using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Rigidbody)) ]

public class Module : MonoBehaviour {
	Renderer[] renders;
	Snap[] snaps;

	GameObject snapTo;
	GameObject snapFrom;

	Rigidbody body;
	MeshCollider mesh;
	Collider genericCollider;

	public bool isSolid;
	public bool isSnapped;

	public Transform sceneParent;

	public bool floating;
	public bool startFrozen;
	public bool unSnappable;
	public GameObject hand; // which hand is holding us if any

	float hilightTimer = 0;
	bool isHilighted = false;

	Material startMaterial;
	Material hilightMaterial;
	Texture startTexture;

	// Use this for initialization
	void Start () {
		sceneParent = transform.parent;

		snaps = GetComponentsInChildren<Snap> ();
		//SpawnVisibleSnaps ();

		renders = GetRenderers ();
		body = GetComponent<Rigidbody> ();
		mesh = GetComponent<MeshCollider> ();
		genericCollider = GetComponent<Collider>();

		SetConvex (true);
		SetKinematic (startFrozen);
		startMaterial = renders [0].material;
		hilightMaterial = (Material) Resources.Load ("Materials/hilight_module");
		startTexture = startMaterial.GetTexture ("_Texture");
	}

	void Update () {
		if (isHilighted) {
			hilightTimer += 8 * Time.deltaTime;
			hilightTimer = Mathf.Min (1, hilightTimer);
		} else {
			hilightTimer -= 8 * Time.deltaTime;
			hilightTimer = Mathf.Max (0, hilightTimer);
		}

		foreach (Renderer r in renders) {
			if (r != null) {
				if (hilightTimer > 0) {
					if (r.material != hilightMaterial) {
						r.material = hilightMaterial;
						r.material.SetTexture ("_Texture", startTexture);
					}
					r.material.SetFloat ("_Crossfade", hilightTimer);
				} else {
					if (r.material != startMaterial) {
						r.material = startMaterial;
					}
					r.material.SetFloat ("_Crossfade", hilightTimer);
				}
			} else {
				print ("shit we lost one");
			}
		}
	}

	void SpawnVisibleSnaps () {
		GameObject visualSnap = (GameObject) Resources.Load ("Prefab/Snap/VisualSnap");
		foreach (Snap snap in snaps) {
			GameObject temp = (GameObject)Instantiate (visualSnap, snap.transform.position, Quaternion.identity);
			temp.transform.rotation = snap.transform.rotation;
			temp.transform.parent = snap.transform;
		}
	}

	Renderer[] GetRenderers() {
		Renderer[] kids = GetComponentsInChildren<Renderer> ();
		if (floating) {
			return new Renderer[1] { GetComponent<Renderer>() };
		}
		return kids;
	}

	public int NumberOfConnections () {
		int connections = 0;
		if (floating || snaps == null)
			return 0;
		for (int i = 0; i < snaps.Length; i++) {
			if (snaps [i].snapFriend != null) {
				connections++;
			}
		}
		return connections;
	}

	public void Hilight () {
		isHilighted = true;
	}
		
	public void UnHilight () {
		isHilighted = false;
	}

	public void PickUp (GameObject newHand) {
		Sound.instance.PlayUnSnap (transform.position);
		SetKinematic (true);
		SetConvex (false);
		startFrozen = false;
		isHilighted = false;
		if (isSnapped) {
			ModuleCircle.piecesLeft += 1;
			Menu.instance.SceneChanged ();
		}
		hand = newHand;
		for (int i = 0; i < snaps.Length; i++) {
			if (snaps [i].snapFriend != null) {
				snaps [i].snapFriend.GetComponent<Snap> ().SetFriend(null);
				snaps [i].SetFriend(null);
			}
		}
	}

	public void SetConvex (bool value) {
		if (floating) {
			SetKinematic (true);
			return;
		}
		if (mesh != null) {
			mesh.convex = value;
		}
	}

	public void SetKinematic (bool value) {
		if (floating) {
			body.isKinematic = true;
		} else {
			body.isKinematic = value;
		}
	}

	public void Release (Vector3 delta) {
		SetConvex (true);
		SetKinematic (false);
		
		body.velocity = delta * 100;
	}

	// TrySnap() --- every frame that we're being held by a hand, shoot rays to all the other snaps
	public GameObject[] TrySnap () {
		List<GameObject[]> pairs = new List<GameObject[]> ();

		if (!floating) {
			for (int i = 0; i < snaps.Length; i++) {
				RaycastHit[] hits = RaycastHoles (snaps [i]);
				for (int k = 0; k < hits.Length && k < 1; k++) {
					pairs.Add (new GameObject[] { snaps [i].gameObject, hits [k].collider.gameObject });		
				}
			}
		}

		if (pairs.Count == 0) {
			// else fail out, no snappies today
			snapTo = null;
			snapFrom = null;
			return null;
		}

		pairs.Sort (delegate(GameObject[] x, GameObject[] y) {
			if (Vector3.Dot (x [0].transform.forward, x [1].transform.forward) < Vector3.Dot (y [0].transform.forward, y [1].transform.forward)) {
				return -1;
			} else {
				return 1;
			}
		});

		snapFrom = pairs [0] [0];
		snapTo = pairs [0] [1];
		return pairs [0];
	}

	// ReleaseSnap() --- we've been released, snap to a snap if we found one last frame
	public void ReleaseSnap (Transform newParent) {
		// newparent is Modules gameobject for organization
		unSnappable = false;
		sceneParent = newParent;
		if (floating) {
			gameObject.transform.parent = null;
		} else {
			gameObject.transform.parent = sceneParent;
		}
		isSnapped = false;

		if (snapTo != null) {
			// turn off physics cuz we're snappin
			SetConvex (false);
			SetKinematic (true);

			Sound.instance.PlaySnap (snapTo.transform.position);

			// shake hands
			snapFrom.GetComponent<Snap> ().SetFriend(snapTo);
			snapTo.GetComponent<Snap> ().SetFriend(snapFrom);

			// make an empty game object called o to wrap our module
			GameObject o = (GameObject) new GameObject ();

			// move and rotate o to match the peg we're snapping from
			o.transform.position = snapFrom.transform.position;
			o.transform.rotation = snapFrom.transform.rotation;

			//parent the module inside our game object
			gameObject.transform.parent = o.transform;

			// move our wrapper to the target location
			o.transform.position = snapTo.transform.position;

			// YEAH BOYOOOYYY
			Quaternion tilt = Quaternion.FromToRotation (o.transform.forward, -snapTo.transform.forward);
			o.transform.rotation = tilt * o.transform.rotation;

			// deparent evertyhing
			gameObject.transform.parent = sceneParent;
			Destroy (o);
			ModuleCircle.piecesLeft -= 1;
			isSnapped = true;
			Menu.instance.SceneChanged ();
			
		}

	}

	RaycastHit[] RaycastHoles (Snap snap) {
		GameObject obj = snap.gameObject;
		Ray r = new Ray (obj.transform.position + obj.transform.forward * -0.02f, obj.transform.forward);

		RaycastHit[] hits = Physics.SphereCastAll (r, 0.0625f, 0.25f, LayerMask.GetMask ("Snap"));
		List<RaycastHit> list = new List<RaycastHit> (hits);

		for (int i = list.Count - 1; i >= 0; i--) {
			Snap temp = list [i].collider.gameObject.GetComponent<Snap> ();
			Module parentModule = temp.GetComponentInParent <Module> ();

			if (list [i].collider.transform.parent == transform) {
				// dont snap to any snap on the same object
				list.RemoveAt (i);
			} else if (temp.snapFriend != null) {
				// dont snap to a snap that's already been snapped to snappy snap snap
				list.RemoveAt (i);
			} else if (parentModule != null && parentModule.unSnappable) {
				// shiiiiit
				list.RemoveAt(i);
			}
		}

		// sort for nearest connection set
		list.Sort (delegate (RaycastHit a, RaycastHit b) {
			float v1 = (obj.transform.position -a.collider.gameObject.transform.position).magnitude;
			float v2 = (obj.transform.position -b.collider.gameObject.transform.position).magnitude;

			if(v1 < v2) {
				return -1;
			} else {
				return 1;
			}
		});
			
		return list.ToArray();
	}
}

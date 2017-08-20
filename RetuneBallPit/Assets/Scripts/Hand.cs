using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {
	public GameObject controller;

	SteamVR_TrackedObject obj;
	SteamVR_TrackedObject.EIndex index;

	GameObject heldObject;
	GameObject hoverObject;

	public GameObject recyclerGameObject;

	public GameObject cameraRig;

	private GameObject possibleSnap;
	public GameObject cursor;
	public GameObject fromCursor;
	public GameObject cursorPointer;

	public GameObject ModulesParent;

	private Vector3 handDelta;
	private Vector3 handLastPos;

	private GameObject selector;
	public bool rightHanded;

	float pulseTimer;
	GameObject touchSphere;


	// Use this for initialization
	void Start () {
		obj = controller.GetComponent<SteamVR_TrackedObject> ();
		index = SteamVR_TrackedObject.EIndex.None;
	}
	
	// Update is called once per frame
	void Update () {
		handDelta = obj.transform.position - handLastPos;

		if (!FoundController ())
			return;



		// add Selector
		if (selector == null) {
			selector = (GameObject) Instantiate ((GameObject) Resources.Load ("Prefab/Player/hand_prefab"), Vector3.one, Quaternion.identity); 
			foreach (Transform t in selector.transform) {
				if (t.name == "Sphere") {
					touchSphere = t.gameObject;
				}
			}
			// scale cuz unity sphere is huuuuge
		//	selector.transform.localScale = Vector3.one * 0.3f;

			selector.transform.parent = obj.transform;
			selector.transform.localEulerAngles = Vector3.zero;
			selector.transform.localPosition = Vector3.zero;

			if (rightHanded) {
				selector.transform.Rotate (0, 0, 180);
			}

			foreach (Transform child in obj.transform) {
				if (child.name == "Model") {
					child.gameObject.SetActive (false);
				}
			}
		}

		if (heldObject != null) {
			touchSphere.SetActive (false);
			if (possibleSnap != null) {
				pulseTimer -= Time.deltaTime;
				if (pulseTimer < 0) {
					pulseTimer = 0.02f;
					SteamVR_Controller.Input ((int)index).TriggerHapticPulse(500);
				}

			}

			if (heldObject.GetComponent<Module> ().hand == gameObject) {
				//heldObject.SendMessage ("Hilight");
			
				// move object to hand
				//heldObject.transform.position = controller.transform.position;
				// check if they let go this frame

				if (SteamVR_Controller.Input ((int)index).GetPressDown (SteamVR_Controller.ButtonMask.Touchpad)) {
					heldObject.SendMessage ("PickRandomTexture", SendMessageOptions.DontRequireReceiver);
				}

				Module m = heldObject.GetComponent<Module> ();
				GameObject[] snapMatrix = m.TrySnap ();
				possibleSnap = null;
				GameObject possibleSourceSnap = null;
				if (snapMatrix != null) {
					possibleSnap = snapMatrix [0];
					possibleSourceSnap = snapMatrix [1];
				}

				if (possibleSnap != null) {
					cursorPointer.transform.position = possibleSnap.transform.position;
					cursorPointer.transform.LookAt (possibleSourceSnap.transform.position);
					cursorPointer.transform.localScale = new Vector3(1, 1, (possibleSnap.transform.position - possibleSourceSnap.transform.position).magnitude);
//					MoveCursor (cursor, possibleSnap);
//					MoveCursor (fromCursor, possibleSourceSnap);
				} else {
					cursorPointer.transform.position = Vector3.up * 100;
					cursor.transform.position = Vector3.up * 100;
					fromCursor.transform.position = Vector3.up * 100;
				}

				if (SteamVR_Controller.Input ((int)index).GetPressUp (SteamVR_Controller.ButtonMask.Trigger)) {
					heldObject.transform.parent = null;
					cursor.transform.position = new Vector3 (0, 100, 0);
					fromCursor.transform.position = new Vector3 (0, 100, 0);
					heldObject.SendMessage ("UnHilight");
					heldObject.SendMessage ("Release", handDelta);
					heldObject.SendMessage ("ReleaseSnap", ModulesParent.transform);
					heldObject = null;
					possibleSnap = null;
					cursorPointer.transform.position = Vector3.up * 100;
				}
			} else {
				heldObject = null;
				cursor.transform.position = new Vector3 (0, 100, 0);
				fromCursor.transform.position = new Vector3 (0, 100, 0);
			}
		} else {

			Debug.DrawRay(controller.transform.position, controller.transform.forward * 3f, Color.red, 0.02f);
			touchSphere.SetActive (true);
			// hilight
			GameObject[] hits = ShootRay ();
			if (hits.Length > 0) {
				if (hoverObject != hits [0] || hoverObject == null) {
					SteamVR_Controller.Input ((int)index).TriggerHapticPulse(3000);
				}

				if (hoverObject != null) {
					hoverObject.SendMessage ("UnHilight");
				}
				hoverObject = hits [0];
				touchSphere.SetActive (false);
				hoverObject.SendMessage ("Hilight");

			} else {
				if (hoverObject != null) {
					hoverObject.SendMessage ("UnHilight");
					hoverObject = null;
					SteamVR_Controller.Input ((int)index).TriggerHapticPulse(3000);
				}
			}
				
			if (SteamVR_Controller.Input ((int)index).GetPressDown (SteamVR_Controller.ButtonMask.Trigger)) {
				if (hoverObject != null) {
					heldObject = hoverObject;
					
					heldObject.GetComponent<Module> ().unSnappable = true;

					heldObject.transform.parent = controller.transform;
					hoverObject.SendMessage ("PickUp", gameObject);
					hoverObject = null;
				}
			}
		}
		handLastPos = obj.transform.position;
	}

	void MoveCursor(GameObject curse, GameObject target) {
		curse.transform.position = target.transform.position;
		curse.transform.rotation = target.transform.rotation;
		curse.transform.position += 0.01f * cursor.transform.forward;
		curse.transform.Rotate (180, 0, 0);
		curse.transform.localScale = new Vector3 (0.25f, 0.25f, 0.025f);
	}

	GameObject[] ShootRay () {
		Ray r = new Ray (controller.transform.position, controller.transform.forward);

		RaycastHit[] hits = Physics.SphereCastAll (r, 0.15f, 0.2f, LayerMask.GetMask ("Module"));

		GameObject[] objects = new GameObject[hits.Length];
		for (int i = 0; i < objects.Length; i++) {
			objects [i] = hits [i].collider.gameObject;
		}

		List<GameObject> tempList = new List<GameObject> (objects);
		for (int i = tempList.Count - 1; i >= 0; i--) {
			if (tempList [i].GetComponent<Module> ().NumberOfConnections () > 1) {
				tempList.RemoveAt (i);
			}
		}

		return tempList.ToArray();
	}


	bool FoundController () {
		if (index == SteamVR_TrackedObject.EIndex.None) {
			index = obj.index;
			return false;
		} else {
			return true;
		}
	}

}

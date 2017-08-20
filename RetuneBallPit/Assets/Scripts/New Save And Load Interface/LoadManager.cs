using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadManager : MonoBehaviour {
	string[] slots;
	// Use this for initialization
	void Start () {
		slots = new string[] {"a", "b", "c", "d", "e", "f", "g", "h", "i"};
		for(int i =0 ; i < slots.Length; i++){
			GameObject o = LoadScene (slots [i]);
			if (o == null) {
				o = (GameObject)Instantiate (Resources.Load ("Prefab/emptySaveNode"), Vector3.zero, Quaternion.identity);
			}
			o.transform.position += Vector3.left * .5f * (i % 3);
			o.transform.position += Vector3.forward * .5f * (i / 3);
			o.transform.position += Vector3.up;
			o.transform.localScale *= 0.1f;
			SaveSlot slot = o.AddComponent<SaveSlot> ();
			slot.name = slots [i];
			slot.waitToScale = i * 0.15f;
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	GameObject LoadScene (string loadMe) {
		GameObject root = new GameObject ();
		root.name = "sculpture " + loadMe;
		print ("Loading file: " + loadMe);
		SavedModule[] loadedModules = SaveLoad.Load (loadMe);
		if (loadedModules != null) {
			//	ClearScene ();
			for (int i = 0; i < loadedModules.Length; i++) {
				SavedModule current = loadedModules [i];

				GameObject g = Resources.Load<GameObject> ("Prefab/" + current.name);
				GameObject guy = (GameObject)Instantiate (g, Vector3.one, Quaternion.identity);
				Destroy (guy.GetComponent<Module> ());
				Destroy (guy.GetComponent<Rigidbody> ());
				Vector3 temp = new Vector3 (current.x, current.y, current.z);
				guy.transform.position = temp;

				guy.transform.parent = root.transform;

				Quaternion quat = new Quaternion (current.rx, current.ry, current.rz, current.rw);
				guy.transform.rotation = quat;

//				Module mod = guy.GetComponent<Module> ();
//				if (mod != null) {
//					guy.GetComponent<Module> ().isSnapped = true;
//					guy.GetComponent<Module> ().startFrozen = true;
//				}
//				// save the ids into each instantiated snap so we can hook them up later
//				Snap[] localSnaps = guy.GetComponentsInChildren<Snap> ();
//				for (int k = 0; k < localSnaps.Length; k++) {
//					//print (current.snaps [k].id + " / " + current.snaps [k].friendid);
//					localSnaps [k].id = current.snaps [k].id;
//					localSnaps [k].friendid = current.snaps [k].friendid;
//				}
			}

			// this is gonna chug
			//allSnaps = GetComponentsInChildren<Snap> ();

			return root;
		} else {
			return null;
		}

	}
}

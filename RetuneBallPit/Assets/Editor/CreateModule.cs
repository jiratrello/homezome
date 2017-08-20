using UnityEngine;
using UnityEditor;
using System.Collections;


[CustomEditor( typeof(Module) )]
public class CreateModule : Editor {
	void OnSceneGUI () {
		Handles.BeginGUI ();
		if (GUI.Button (new Rect (10, 10, 100, 30), "Make Module")) {
			GameObject root = Selection.activeGameObject;
		
			if (root.GetComponent<Module> () == null) {
				root.gameObject.AddComponent<Module> ();
			}
			root.AddComponent<MeshCollider> ();
			root.GetComponent<MeshCollider> ().convex = true;

			root.layer = LayerMask.NameToLayer ("Module");

			for (int i = 0; i < root.transform.childCount; i++) {
				GameObject child = root.transform.GetChild (i).gameObject;
				if (child.name.ToLower ().Contains ("snap")) {
					if (child.GetComponent<Snap> () == null) {
						child.AddComponent<Snap> ();
						child.layer = LayerMask.NameToLayer ("Snap");

						SphereCollider sphere = child.GetComponent<SphereCollider> ();
						sphere.radius = 0.0625f;
						sphere.isTrigger = true;
					}
				}
			}

		}
		Handles.EndGUI();
	}

}

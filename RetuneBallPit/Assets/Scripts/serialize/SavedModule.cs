using UnityEngine;
using System.Collections;

[System.Serializable]
public class SavedModule {
	public string name;

	public float x,y,z;
	public float rx, ry, rz, rw;

	public SavedSnap[] snaps;

	public SavedModule (GameObject g) {
		name = g.name.Replace ("(Clone)", "");
		x = g.transform.position.x;
		y = g.transform.position.y;
		z = g.transform.position.z;

		rx = g.transform.rotation.x;
		ry = g.transform.rotation.y;
		rz = g.transform.rotation.z;
		rw = g.transform.rotation.w;

		// serialize all the snaps
		Snap[] objSnaps = g.GetComponentsInChildren<Snap> ();
		snaps = new SavedSnap[objSnaps.Length];
		for (int i = 0; i < objSnaps.Length; i++) {
			snaps [i] = new SavedSnap (objSnaps [i]);
		}
	}
}

[System.Serializable]
public class SavedSnap {
	public double id, friendid;
	public SavedSnap (Snap s) {
		id = s.id;
		friendid = s.friendid;
	}
}
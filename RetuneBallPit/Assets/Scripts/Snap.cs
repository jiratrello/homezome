using UnityEngine;
using System.Collections;

[RequireComponent (typeof (SphereCollider))]
public class Snap : MonoBehaviour {
	public GameObject snapFriend;
	public double id;
	public double friendid;

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.blue;
		Gizmos.DrawLine (transform.position - transform.forward * 0.05f, transform.position + transform.forward * 1f);
	}

	void Start () {
		if (id == 0) {
			id = Random.value;
		}
	}

	public void SetFriend (GameObject newFriend) {
		snapFriend = newFriend;
		if (snapFriend != null) {
			friendid = snapFriend.GetComponent<Snap> ().id;
		
		} else {
			friendid = 0;
		}
	}
}

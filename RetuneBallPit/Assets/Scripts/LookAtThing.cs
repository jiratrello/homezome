using UnityEngine;
using System.Collections;

public class LookAtThing : MonoBehaviour {
	public GameObject target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {

			Quaternion tilt = Quaternion.FromToRotation (transform.forward, -target.transform.forward);
			transform.rotation = tilt * transform.rotation;
			//transform.rotation = Quaternion.LookRotation (target.transform.position - transform.position, target.transform.forward);
		}
		//transform.LookAt (target.transform.position, Vector3.up);
	}
}

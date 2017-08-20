using UnityEngine;
using System.Collections;

public class FrapsCamera : MonoBehaviour {

	public GameObject targetEye;

	Vector3 lookTrail;
	// Use this for initialization
	void Start () {
		lookTrail = targetEye.transform.forward * 5 + targetEye.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += (targetEye.transform.position - transform.position) * Time.deltaTime * 20;
		lookTrail += (targetEye.transform.forward * 5 + targetEye.transform.position - lookTrail) * 5 * Time.deltaTime;
		transform.LookAt (lookTrail);
	//	transform.rotation = targetEye.transform.rotation;
	}
}

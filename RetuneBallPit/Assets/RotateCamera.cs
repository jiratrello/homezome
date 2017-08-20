using UnityEngine;
using System.Collections;

public class RotateCamera : MonoBehaviour {
	// Use this for initialization
	Camera cam;
	public GameObject cameraObj;
	float fovDelta = 60;
	float fov = 60;
	Vector3 oldMousePos;
	Vector3 deltaPos;
	float rotateSpeed = 15;
	void Start () {
		
		cam = GetComponentInChildren<Camera> ();
		oldMousePos = Input.mousePosition;
		deltaPos = new Vector3 ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Tab)) {
			cam.gameObject.SetActive (!cam.gameObject.activeInHierarchy);
		}
		float zoomchange = Input.GetAxis ("Mouse ScrollWheel");
		fovDelta -= zoomchange * 20f;
		fovDelta = Mathf.Clamp(fovDelta, 2, 100);
		fov += (fovDelta - fov) * 3 * Time.deltaTime;


		transform.Rotate (0, rotateSpeed* Time.deltaTime, 0);

		Vector3 change = new Vector3 ();
		if (Input.GetMouseButton (1)) {
			 change = Input.mousePosition - oldMousePos;
			if (Input.GetKeyDown ("q")) {
				if (rotateSpeed == 30) {
					rotateSpeed = 0;	
				} else {
					rotateSpeed = 30;
				}
			}
		}

		deltaPos += (change - deltaPos) * 2.3f * Time.deltaTime;
		cameraObj.transform.Rotate (0, deltaPos.x * 0.1f, 0);
		if (cam.gameObject.activeInHierarchy) {
			cam.fieldOfView = fov;
			cam.gameObject.transform.Rotate (deltaPos.y * -0.1f, 0, 0);
		}
		oldMousePos = Input.mousePosition;
	}
}

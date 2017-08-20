using UnityEngine;
using System.Collections;

public class SyncPortalCamera : MonoBehaviour {
	public GameObject axisA, cameraA, axisB, renderCamera;
	public Texture renderTarget;

	Camera VRCam;
	Camera renderCam;

	// Use this for initialization
	void Start () {
		//cameraA.GetComponent<Camera>().main
		renderCamera.GetComponent<Camera>().targetTexture = (RenderTexture) renderTarget;
		 renderCam = renderCamera.GetComponent<Camera> ();
		 VRCam = cameraA.GetComponent<Camera> ();
		renderTarget.width = VRCam.pixelWidth;// Screen.width;
		renderTarget.height =  VRCam.pixelHeight;
	}
	
	// Update is called once per frame
	void OnPreRender () {

		renderCam.fieldOfView = VRCam.fieldOfView;
		renderCam.nearClipPlane = VRCam.nearClipPlane;
		renderCam.farClipPlane = VRCam.farClipPlane;
		Vector3 offset = (cameraA.transform.position - axisA.transform.position);
		renderCamera.transform.position = axisB.transform.position + offset;
		renderCamera.transform.rotation = cameraA.transform.rotation;
	}
}
using UnityEngine;
using System.Collections;

public class GenerateSnaps : MonoBehaviour {
	public GameObject snap;
	int width = 2;
	int depth = 2;

	float scale = 1.0f;
	// Use this for initialization
	void Start () {

		for (int i = 0; i < width; i++) {
			for (int k = 0; k < depth; k++) {
				Vector3 v = new Vector3 (i * scale - (scale * width * 0.25f) , 0, k * scale - (scale * depth * 0.25f));
				GameObject o = (GameObject) Instantiate (snap, v, Quaternion.identity);
				o.transform.parent = transform;
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

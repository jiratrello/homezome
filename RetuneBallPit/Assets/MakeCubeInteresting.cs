using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeCubeInteresting : MonoBehaviour {
	float rota;
	Material m;
	// Use this for initialization
	void Start () {
		m = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (13 * Time.deltaTime, 34 * Time.deltaTime ,0 );
		transform.localScale = Vector3.one + Vector3.up * Mathf.Sin (rota * 0.3f) * 0.7f;
		rota += Time.deltaTime;
		m.color = new Color (SinRemap(rota), SinRemap(rota * 1.2f),SinRemap(rota * 1.1f + 0.3f));
	}

	float SinRemap (float input) {
		return (Mathf.Sin(input) + 1) * 0.5f;
	}
}

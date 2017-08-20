using UnityEngine;
using System.Collections;

public class ColorSkybox : MonoBehaviour {
	private float x,y;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		x += (transform.position.x - x) * 0.4f * Time.deltaTime;
		y += (transform.position.y - y) * 0.4f * Time.deltaTime;
		Shader.SetGlobalFloat("_playerX", x);
		Shader.SetGlobalFloat("_playerY", y);
	}
}

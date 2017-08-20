using UnityEngine;
using System.Collections;

public class AnimateTexture : MonoBehaviour {
	Renderer r;
	float timer;
	int index;
	// Use this for initialization
	void Start () {
		r = GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {

		timer += Time.deltaTime;
		if (timer > 0.1f) {
			timer -= 0.1f;
			index++;
			if (index > 3) {
				index = 0;
			}
		}
		r.material.mainTextureOffset = new Vector2 (index * 0.25f, 0);
	}
}

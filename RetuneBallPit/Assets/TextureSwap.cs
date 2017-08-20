using UnityEngine;
using System.Collections;

public class TextureSwap : MonoBehaviour {
	public Texture[] textures;
	int index;
	// Use this for initialization
	void Start () {
		index = Random.Range (0, 4);
		PickRandomTexture ();

	}

	public void PickRandomTexture () {
		if (textures.Length > 0) {
			index++;
			if (index >= textures.Length) {
				index = 0;
			}
			Material m = GetComponent<Renderer> ().material;
			m.SetTexture ("_Texture", textures [index]);
		}
	}
}

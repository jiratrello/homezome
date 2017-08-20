using UnityEngine;
using System.Collections;

public class StrobeColor : MonoBehaviour {
	public Color a,b;
	bool toggle;
	Material m;
	float timer;

	// Use this for initialization
	void Start () {
		m = GetComponent<Renderer> ().material;
	}
	
	// Update is called once per frame
	void Update () {
		timer -= Time.deltaTime;
		if (timer < 0) {
			toggle = !toggle;
			timer = 0.07f;
		}
		if (toggle) {
			m.SetColor ("_Color", a);
		} else {
			m.SetColor ("_Color", b);
		}

	}
}

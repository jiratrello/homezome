using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSlot  : MonoBehaviour {
	public string name;
	public float waitToScale;
	Vector3 originScale;
	Vector3 originPosition;

	// Use this for initialization
	void Start () {
		originPosition = transform.position;
		transform.position += Vector3.down * 2;
		originScale = transform.localScale;
		transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	void Update () {
		if (waitToScale > 0) {
			waitToScale -= Time.deltaTime;
		} else {
			transform.position += (originPosition - transform.position) * Time.deltaTime * 5f;
			transform.localScale += (originScale - transform.localScale) * Time.deltaTime * 2f;
		}
	}
}

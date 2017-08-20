using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Recycler : MonoBehaviour {
	public GameObject moduleParent;
	public GameObject[] modules;
	public GameObject[] pipes;
	List<GameObject> bag;

	public int buffer;
	float spawnTimer;

	// Use this for initialization
	void Start () {
		buffer = 0;
		ResetBag ();
	}
	
	// Update is called once per frame
	void Update () {
		spawnTimer -= Time.deltaTime;
		if (spawnTimer < 0) {
			spawnTimer = 0.3f;
			if (buffer > 0) {
				if (Random.value > 0.3) {
					SpawnPipe ();
				} else {
					SpawnOther ();
				}
				buffer--;
			}
		}
	}

	void SpawnPipe () {
		GameObject o = (GameObject)Instantiate (pipes [Random.Range (0, pipes.Length)], new Vector3 (0, 4, 0), Quaternion.identity);
		o.transform.parent = moduleParent.transform;
		o.GetComponent<Module> ().sceneParent = moduleParent.transform;
	}

	void SpawnOther () {
		int pick = Random.Range (0, bag.Count);
		GameObject o = (GameObject) Instantiate (bag[pick], new Vector3 (0, 4, 0), Quaternion.identity);
		o.transform.parent = moduleParent.transform;
		o.GetComponent<Module> ().sceneParent = moduleParent.transform;
		bag.RemoveAt (pick);
		if (bag.Count == 0) {
			ResetBag ();
		}
	}

	void OnTriggerEnter (Collider c) {
		if (c.gameObject.layer == 8) {
			Destroy (c.gameObject);
			//buffer++;
		}
	}

	void ResetBag () {
		bag = new List<GameObject> (modules);
	}
}

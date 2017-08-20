using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pallete : MonoBehaviour {
	public GameObject[] modules;

	public GameObject[] pipes;
	public GameObject[] blocks;
	public GameObject[] architecture;

	public GameObject[] lastCategory;
	public GameObject lastModule;

	public GameObject child1, child2, child3;
	private GameObject c1,c2,c3;
	// Use this for initialization

	void Start () {
		lastCategory = pipes;
		lastModule = pipes[0];
		RepopulatePallete ();
	}
	
	// Update is called once per frame
	void Update () {
		if (child1.transform.childCount == 0) {
			lastModule = c1;
			lastCategory = WhichList (c1);
			RepopulatePallete ();
		} else if ( child2.transform.childCount == 0) {
			lastModule = c2;
			lastCategory = WhichList (c2);
			RepopulatePallete ();
		} else if ( child3.transform.childCount == 0) {
			lastModule = c3;
			lastCategory = WhichList (c3);
			RepopulatePallete ();
		}	



		child1.transform.Rotate (60 * Time.deltaTime, 23 * Time.deltaTime, 0);
		child2.transform.Rotate (-40 * Time.deltaTime, 23 * Time.deltaTime, 8 * Time.deltaTime);
		child3.transform.Rotate (10 * Time.deltaTime, -23 * Time.deltaTime, 39 * Time.deltaTime);

	}

	void RepopulatePallete () {
		// child1 repeat last module

		foreach (Transform child in child1.transform) {
			Destroy (child.gameObject);
		}
		GameObject newDupe = (GameObject) Instantiate (lastModule, child1.transform.position, Quaternion.identity);
		newDupe.transform.parent = child1.transform;
		Module m1 = newDupe.GetComponent<Module> ();
		m1.startFrozen = true;
		m1.unSnappable = true;
		c1 = lastModule;

		// child 2 repeat same category

		foreach (Transform child in child2.transform) {
			Destroy (child.gameObject);
		}
		GameObject newtemp = GetRandom (lastCategory);
		GameObject newModule = (GameObject) Instantiate (newtemp, child2.transform.position, Quaternion.identity);
		newModule.transform.parent = child2.transform;
		Module m2 = newModule.GetComponent<Module> ();
		m2.startFrozen = true;
		m2.unSnappable = true;
		c2 = newtemp;

		foreach (Transform child in child3.transform) {
			Destroy (child.gameObject);
		}
		GameObject randomTemp = GetUniqueModule ();
		GameObject newRandom = (GameObject) Instantiate (randomTemp, child3.transform.position, Quaternion.identity);
		newRandom.transform.parent = child3.transform;
		Module m3 = newRandom.GetComponent<Module> ();
		m3.startFrozen = true;
		m3.unSnappable = true;
		c3 = randomTemp;
	}

	GameObject GetRandom (GameObject[] list) {
		return list [Random.Range (0, list.Length)];
	}

	GameObject[] WhichList (GameObject g) {
		foreach (GameObject test in pipes) {
			if (test == g)
				return pipes;
		}
		foreach (GameObject test in blocks) {
			if (test == g)
				return blocks;
		}
		foreach (GameObject test in architecture) {
			if (test == g)
				return architecture;
		}
		return pipes;
	}

	GameObject GetUniqueModule () {
		List<GameObject> bucket = new List<GameObject> ();
		if (lastCategory != pipes) {
			AddToList (bucket, pipes);
		}
		if (lastCategory != blocks) {
			AddToList (bucket, blocks);
		}
		if (lastCategory != architecture) {
			AddToList (bucket, architecture);
		}
		return (bucket [Random.Range (0, bucket.Count)]);
	}

	void AddToList(List<GameObject> list, GameObject[] oldlist) {
		foreach (GameObject o in oldlist) {
			list.Add (o);
		}
	}


}

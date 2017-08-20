using UnityEngine;
using System.Collections;

public class ModuleCircle : MonoBehaviour {
	public GameObject[] modules;
	public static int piecesLeft;
	public GameObject ring;
	// Use this for initialization
	void Start () {
		SpawnInCircle ();
		piecesLeft = 15;
	}
	
	// Update is cmodulesed once per frame
	void Update () {
		piecesLeft = 15;
		if (piecesLeft <= 0) {
			ring.SetActive (false);
		} else {
			if (!ring.activeInHierarchy) {
				ring.SetActive (true);
			}
		}
	}



	void SpawnInCircle () {
		float radius = 5;
		for (int i = 0; i < modules.Length; i++) {
			float angle = 1f / (float) modules.Length;
			Vector3 location = new Vector3 (Mathf.Cos((i * angle) * Mathf.PI * 2) * radius, 0,  Mathf.Sin((i * angle) * Mathf.PI * 2) * radius);
			GameObject o = new GameObject ();
			o.transform.position = location;
			o.transform.parent = ring.transform;
			o.AddComponent<ModuleSpawner>();
			o.GetComponent<ModuleSpawner>().toSpawn = modules [i];
			o.GetComponent<ModuleSpawner> ().index = i * 1.4f;
		}
	}
}

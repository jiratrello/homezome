using UnityEngine;
using System.Collections;

public class ModuleSpawner : MonoBehaviour {
	public GameObject toSpawn;
	float dir;

	public float index;
	Vector3 start;
	float rota;
	// Use this for initialization
	void Start () {
		dir = Random.Range (-2, 2);
		start = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		rota += Time.deltaTime * 1.5f;
		transform.position = start + Vector3.up * Mathf.Cos (rota + index ) * .2f;

		if (transform.childCount == 0) {
			GameObject o = (GameObject)Instantiate (toSpawn, Vector3.zero, Quaternion.identity);
			o.transform.parent = transform;
			o.transform.localPosition = Vector3.zero;
			o.GetComponent<Module> ().startFrozen = true;
			o.GetComponent<Module> ().unSnappable = true;
		}

		transform.Rotate (23 * Time.deltaTime, 0, 19 * dir* Time.deltaTime);
	}
}

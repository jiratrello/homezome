using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {
	public static Sound instance;

	public AudioClip[] snaps;
	public AudioClip[] unsnaps;

	float possibleTimer = 0;
	Vector3 moveTo;
	AudioSource source;

	void Start () {
		instance = this;
		source = GetComponent<AudioSource> ();
	}
	// Use this for initialization
	public void PlaySnap (Vector3 pos) {
		GameObject temp = (GameObject) Instantiate(Resources.Load("Prefab/Player/sound_null"), pos, Quaternion.identity);
		temp.GetComponent<PlayAndDie>().clip =  snaps [Random.Range (0, snaps.Length)];
	}

	public void PlayUnSnap (Vector3 pos) {
		GameObject temp = (GameObject) Instantiate(Resources.Load("Prefab/Player/sound_null"), pos, Quaternion.identity);
		temp.GetComponent<PlayAndDie>().clip =  unsnaps [Random.Range (0, snaps.Length)];
	}

	void Update () {
		transform.position += (moveTo - transform.position) * 3 * Time.deltaTime;

		if (possibleTimer > 0) {
			possibleTimer -= Time.deltaTime;
			source.volume += (1 - source.volume) * 10 * Time.deltaTime;
		} else {
			source.volume += (0 - source.volume) * 10 * Time.deltaTime;
		}
	}

	public void SnapPossible (Vector3 pos) {
		possibleTimer = 0.2f;
		moveTo = pos;
	}
}

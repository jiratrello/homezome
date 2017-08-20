using UnityEngine;
using System.Collections;

public class PlayAndDie : MonoBehaviour {
	AudioSource source;
	public AudioClip clip;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource> ();
		source.clip = clip;
		source.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!source.isPlaying) {
			Destroy (gameObject);
		}
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, 1f);
	}
}

using UnityEngine;
using System.Collections;

public class CubeMeter : MonoBehaviour {
	int localCount;
	// Use this for initialization
	void Start () {
		for (int y = 0; y < 5; y++) {
		for (int x = 0; x < 4; x++) {
				Vector3 pos = new Vector3 (-0.09f + x * 0.06f, 0, -0.429f - y * 0.06f);
				GameObject temp = (GameObject)Instantiate (Resources.Load ("Prefab/Player/CubePiece"), Vector3.zero, Quaternion.identity);
				temp.transform.parent = transform;
				temp.transform.localRotation = Quaternion.identity;
				temp.transform.localPosition = pos;
				temp.SetActive (false);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (localCount != ModuleCircle.piecesLeft) {
			localCount = ModuleCircle.piecesLeft;
			for (int i = 0; i < transform.childCount; i++) {
				bool isOn = i < localCount;
				transform.GetChild (i).gameObject.SetActive (isOn);
			}
		}
	}
}

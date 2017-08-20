using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public class Menu : MonoBehaviour {
	public static Menu instance;

	Snap[] allSnaps;
	bool hideMenu = true;

	string textBoxName = "default";
	string directoryName = "cool boys folder";
	string fileName = "";
	public GUIStyle  fieldStyle, buttonStyle, labelStyle;

	bool autoSave = false;
	bool timelapse = false;

	int counter;
	float timelapseTimer = 0;

	bool lateConnectSnaps;

	void Start () {
		instance = this;
	//	SetupDirectory ();
	}

	void SetupDirectory () {
		if (PlayerPrefs.HasKey ("directory")) {
			directoryName = PlayerPrefs.GetString ("directory");
			SaveLoad.SetDirectory (directoryName);
		} else {
			string path = Directory.GetParent(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)).FullName;
			path = Directory.GetParent (path).ToString () + "\\Documents\\homezome saves";
			Directory.CreateDirectory(path);
			directoryName = path;
			SaveLoad.SetDirectory (path);
			PlayerPrefs.SetString ("directory", directoryName);
		}
	}

	void OnGUI () {
		if (Input.GetKeyDown ("`")) {
			RunTimeLapse ();
		}

		if (true)
			return;
		GUILayout.BeginArea (new Rect(15, Screen.height - 345, 150, 330));

//		// get directory name from GUI
//		string tempDirectory = directoryName;
//		directoryName = GUILayout.TextField (directoryName, fieldStyle);
//		// if it changed, resave it
//		if (tempDirectory != directoryName) {
//			SaveLoad.SetDirectory (directoryName);
//			PlayerPrefs.SetString ("directory", directoryName);
//		}

		string status = "";
		if (autoSave) {
			status = fileName + "_" + (counter).ToString ("000") + " AUTOSAVING";
		} else if (timelapse) {
			status = "TIMELAPSE " + counter.ToString("000");
		} else {
			status = ":) " + fileName + "_" + counter.ToString("000");
		}
	
		GUILayout.Button (status, labelStyle);

		textBoxName = GUILayout.TextArea(textBoxName, fieldStyle);
		if (autoSave) {
			if (GUILayout.Button("Stop", buttonStyle)) {
				autoSave = false;
			}
		} else {
			string continueText = "New autoSave";
			if (fileName == textBoxName) {
				continueText = "Continue autosave";
			}
			if (GUILayout.Button (continueText, buttonStyle)) {
				if (fileName != textBoxName) {
					counter = 0;
				}
				autoSave = true;
				fileName = textBoxName;
			}

		}
			

		if (!autoSave) {
			if(GUILayout.Button( "Load", buttonStyle)){
				autoSave = false;
				ClearScene ();
				string latest = SaveLoad.GetNewestFile (textBoxName);
				if (latest != "") {
					string tempname = (latest.Split ('_') [1].Split('.')[0]);
					int.TryParse(tempname, out counter);
					fileName = textBoxName;
					LoadScene (fileName + "_" + counter.ToString ("000"));
					ConnectSnaps ();
				}
			}

			if (!timelapse) {
				if (GUILayout.Button ("Load Timelapse", buttonStyle)) {
					fileName = textBoxName;
					RunTimeLapse ();
				}
			} else if (GUILayout.Button ("Stop Timelapse", buttonStyle)) {
				timelapse = false;
			}
		}
	
		if (GUILayout.Button ("Clear", buttonStyle	)) {
			autoSave = false;
			ClearScene ();

			// add our root snap back in
			GameObject root = (GameObject)Instantiate (Resources.Load ("Prefab/rootNode"), Vector3.zero, Quaternion.identity);
			root.transform.parent = transform;
		}

		GUILayout.Label(ModuleCircle.piecesLeft + " pieces", labelStyle);
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("0", buttonStyle)) {
			ModuleCircle.piecesLeft = 0;
		}
		if (GUILayout.Button ("Set 15", buttonStyle)) {
			ModuleCircle.piecesLeft = 15;
		}
	
		GUILayout.EndHorizontal ();

		GUILayout.EndArea ();
	}

	void Update () {
		if (Input.GetButtonDown ("Jump")) {
			hideMenu = !hideMenu;
		}
		if (lateConnectSnaps) {
			ConnectSnaps ();
		}
		if (timelapse) {
			timelapseTimer += Time.deltaTime;
			if (timelapseTimer > 0.1f) {
				timelapseTimer = 0;
				LoadTimeLapse ();
			}
		}
	}

	void RunTimeLapse () {

		timelapse = true;
		counter = 0;
	}

	public void SceneChanged () {
		if (autoSave) {
			SaveModules ();
			counter++;
		}
	}

	void ClearScene () {
		for (int i = transform.childCount - 1; i >= 0; i--) {
			Destroy(transform.GetChild(i).gameObject);
		}
	}

	void SaveModules () {
		List<SavedModule> toSave = new List<SavedModule> ();
		for (int i = 0; i < transform.childCount; i++) {
			GameObject child = transform.GetChild (i).gameObject;
			SavedModule mod = new SavedModule (child);
			if (child.name.Contains("rootNode") || child.GetComponent<Module> ().isSnapped) {
				toSave.Add (mod);
			}
		}

		SaveLoad.Save(toSave.ToArray(), GetNumberName());
	}

	string GetNumberName () {
		return fileName + "_" + counter.ToString ("000");
	}
		
	bool LoadScene (string loadMe) {
		print ("Loading file: " + loadMe);
		SavedModule[] loadedModules = SaveLoad.Load (loadMe);
		if (loadedModules != null) {
			ClearScene ();
			for (int i = 0; i < loadedModules.Length; i++) {
				SavedModule current = loadedModules [i];

				GameObject g = Resources.Load<GameObject> ("Prefab/" + current.name);
				GameObject guy = (GameObject)Instantiate (g, Vector3.one, Quaternion.identity);
				Vector3 temp = new Vector3 (current.x, current.y, current.z);
				guy.transform.position = temp;

				guy.transform.parent = transform;

				Quaternion quat = new Quaternion (current.rx, current.ry, current.rz, current.rw);
				guy.transform.rotation = quat;

				Module mod = guy.GetComponent<Module> ();
				if (mod != null) {
					guy.GetComponent<Module> ().isSnapped = true;
					guy.GetComponent<Module> ().startFrozen = true;
				}
				// save the ids into each instantiated snap so we can hook them up later
				Snap[] localSnaps = guy.GetComponentsInChildren<Snap> ();
				for (int k = 0; k < localSnaps.Length; k++) {
					//print (current.snaps [k].id + " / " + current.snaps [k].friendid);
					localSnaps [k].id = current.snaps [k].id;
					localSnaps [k].friendid = current.snaps [k].friendid;
				}
			}

			// this is gonna chug
			allSnaps = GetComponentsInChildren<Snap> ();


			return true;
		} else {
			return false;
		}
	}

	void ConnectSnaps () {
		lateConnectSnaps = false;
		if (allSnaps == null)
			return;
		print ("Connecting " + allSnaps.Length + " snaps");
		for (int i = 0; i < allSnaps.Length; i++) {
			if (allSnaps [i].friendid != 0) {
				Snap friend = FindSnapInScene (allSnaps [i].friendid);
				if (friend == null) {
					continue;
				}
				allSnaps [i].SetFriend (friend.gameObject);
				friend.SetFriend (allSnaps [i].gameObject);
			}
		}
	}

	void LoadTimeLapse () {
		if (LoadScene (GetNumberName ())) {
			counter++;
		} else {
			lateConnectSnaps = true;
			timelapse = false;
		}
	}

	Snap FindSnapInScene (double id) {
		for (int i = 0; i < allSnaps.Length; i++) {
			if (allSnaps [i].id == id) {
				return allSnaps [i];
			}
		}
		return null;
	}
}
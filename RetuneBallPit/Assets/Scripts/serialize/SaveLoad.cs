using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;

public static class SaveLoad {
	private static string root = "C:\\Users\\troyduguid\\Desktop\\zome_files\\";

	//it's static so we can call it from anywherex
	public static void Save(SavedModule[] list, string filename) {
	//	string path = EditorUtility.SaveFilePanel( "Save Zome", root, "myzome.zome", "zome" );
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (root+filename + ".zome");
		bf.Serialize(file, list);
		file.Close();
	}    

	public static SavedModule[] Load(string filename) {
		if(File.Exists(root+filename + ".zome")) {
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(root+filename + ".zome", FileMode.Open);
			SavedModule[] loadedSavedModule = (SavedModule[]) bf.Deserialize(file);
			file.Close();
			return loadedSavedModule;
		}
		else {
			Debug.Log("File doesn't exist!");
			return null;
		}

	}

	public static void SetDirectory (string newName) {
		if (newName.Last () != '\\') {
			newName = newName + "\\";
		}
		root = newName;	
	}

	public static string GetDirectory () {
		return root;
	}

	public static string GetNewestFile (string filename){
		List<FileInfo> files =  new DirectoryInfo(root).GetFiles(filename+"*").OrderByDescending(f => f.LastWriteTime).ToList();
		if (files.Count == 0) {
			return "";
		}
		return files [0].Name;
	}


}
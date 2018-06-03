using UnityEngine;
using System.Collections;
using Classes.SaveLoadSystem;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Classes.SaveLoadSystem
{
	public class SaveSystem
	{
		[System.Serializable]
		public struct SavePocket
		{
			public ObjectData[] toSaveData;
		}

		public static string PocketPath (string name)
		{
			return Application.persistentDataPath + "/Saved/" + name + "/Data.pct";
		}

		public static SavePocket LoadPocketFile (string name) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (PocketPath (name), FileMode.Open);
			return (SavePocket)bf.Deserialize (file);
		}
		public static void SavePocketFile (SavePocket toSave, string name) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (PocketPath (name));
			bf.Serialize (file, toSave);
		}
	}
}
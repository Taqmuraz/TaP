using UnityEngine;
using System.Collections;
using Classes.SaveLoadSystem;
using Classes.Math;

namespace Classes.SaveLoadSystem
{
	[System.Serializable]
	public class ObjectData
	{
		public string keyName { get; set; }
		public SVector position { get; set; }
		public SVector euler { get; set; }

		public ObjectData (string _key_name, SVector _position, SVector _euler) {
			keyName = _key_name;
			position = _position;
			euler = _euler;
		}
		public ObjectData () {
		}
	}
}
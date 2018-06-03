using UnityEngine;
using System.Collections;
using Classes.SaveLoadSystem;
using Classes.Math;

namespace Classes.SaveLoadSystem
{
	[System.Serializable]
	public class AliveData : ObjectData
	{
		public string characterName { get; private set; }
		public float health { get; private set; }

		public AliveData (string _key_name, int _health, string _character_name, SVector _position, SVector _euler) {
			keyName = _key_name;
			health = _health;
			characterName = _character_name;
			position = _position;
			euler = _euler;
		}

		public void ApplyDamage (float damage) {
			health -= damage;
			Heal (0);
		}
		public void Heal (float points) {
			health += points;
			health = Mathf.Clamp (health, 0, 100);
		}
	}
}
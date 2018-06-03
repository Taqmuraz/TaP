using UnityEngine;
using System.Collections;
using Behaviours.Alives;
using Behaviours;
using Classes.Engine;
using Classes.Math;
using Classes.SaveLoadSystem;

namespace Delegates
{
	public class DelegatesData
	{
		public delegate void ToDo ();
		public delegate bool AliveBool (Alive alive);
		public delegate float AliveFloat (Alive alive);
		public delegate void AliveDo (Alive alive);
	}
}

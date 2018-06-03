using System;
using UnityEngine;
using Behaviours.Alives;
using Behaviours;

namespace Statuses
{
	public class AliveStatus
	{
		public Alive alive { get; private set; }

		public AliveStatus (Alive _alive)
		{
			alive = _alive;
		}

		public Vector3 localVelocity
		{
			get {
				return alive.trans.InverseTransformDirection (alive.body.velocity);
			}
		}
	}
}


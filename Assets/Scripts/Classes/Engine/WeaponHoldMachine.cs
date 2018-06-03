using UnityEngine;
using System.Collections;
using Behaviours.Alives;
using Behaviours;

namespace Classes.Engine
{
	public class WeaponHoldMachine
	{
		public WeaponHold weaponHold { get; private set; }
		public Character character { get; private set; }

		public WeaponHoldMachine (Character _character) {
			character = _character;
			weaponHold = character.GetComponentInChildren<WeaponHold> ();
		}

		public Vector3 position
		{
			get {
				Vector3 fwd = character.lookDirection;
				Vector3 right = new Vector3 (fwd.z, fwd.y, -fwd.x);
				return character.head.position + right / 4 - character.trans.up / 4 + fwd / 6;
			}
		}

		public void WeaponIK () {
			if (weaponHold) {
				weaponHold.trans.position = position;
				weaponHold.trans.rotation = Quaternion.LookRotation (character.lookDirection);
				character.anims.SetIKPositionWeight (AvatarIKGoal.LeftHand, 1);
				character.anims.SetIKPositionWeight (AvatarIKGoal.RightHand, 1);
				character.anims.SetIKPosition (AvatarIKGoal.LeftHand, weaponHold.leftHandPoint.position);
				character.anims.SetIKPosition (AvatarIKGoal.RightHand, weaponHold.rightHandPoint.position);
			}
		}
	}
}
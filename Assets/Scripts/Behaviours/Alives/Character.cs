using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Behaviours;
using Behaviours.Alives;
using Classes.Engine;

namespace Behaviours.Alives
{
	public class Character : Alive
	{
		public Vector3 lookPoint { get; private set; }
		public Vector3 lookDirection
		{
			get {
				return (lookPoint - head.position).normalized;
			}
		}
		public Transform head { get; private set; }
		public bool crouching { get; set; }
		public WeaponHoldMachine weaponHoldMachine { get; private set; }

		public override void Start ()
		{
			base.Start ();
			head = anims.GetBoneTransform (HumanBodyBones.Head);
			weaponHoldMachine = new WeaponHoldMachine (this);
		}

		public override void LookAt (Vector3 at)
		{
			Vector3 dir = (at - head.position);
			dir = new Vector3 (dir.x, 0, dir.z);
			float f = aliveStatus.localVelocity.magnitude;
			if (f > 0.5f || Vector3.Angle(dir, trans.forward) > 75) {
				base.LookAt (at);
			}
			lookPoint = at;
		}

		private void OnAnimatorIK () {
			anims.SetLookAtWeight (1, 1, 1, 1);
			anims.SetLookAtPosition (lookPoint);
			weaponHoldMachine.WeaponIK ();
			LegsAirIK ();
		}
		private float legs_air_ik = 0;
		private float legs_air_ik_v = 0;
		private void LegIK (AvatarIKGoal leg) {
			anims.SetIKPositionWeight (leg, legs_air_ik);
			anims.SetIKRotationWeight (leg, legs_air_ik);
			float a = leg == AvatarIKGoal.LeftFoot ? -0.25f : 0.25f;
			float b = leg == AvatarIKGoal.LeftFoot ? 0.25f : -0.25f;
			Vector3 v = velocity.magnitude > 1 ? velocity.normalized : velocity;
			v.y = Mathf.Clamp (v.y, -0.5f, 0.5f);
			Vector3 dop = trans.right * a + trans.forward * b;
			Vector3 up = trans.up * 0.25f;
			anims.SetIKPosition (leg, trans.position + v * legs_air_ik_v + dop + up);
			anims.SetIKRotation (leg, trans.rotation);
		}

		private void LegsAirIK () {
			if (!onGround) {
				LegIK (AvatarIKGoal.LeftFoot);
				LegIK (AvatarIKGoal.RightFoot);
			}
			legs_air_ik = Mathf.Lerp (legs_air_ik, onGround ? 0 : 1, Time.fixedDeltaTime * 8);
			legs_air_ik_v = Mathf.Lerp (legs_air_ik_v, onGround ? -2 : 1, Time.fixedDeltaTime * 2);
		}
	}
}
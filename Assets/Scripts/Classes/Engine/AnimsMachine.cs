using UnityEngine;
using Delegates;
using Behaviours;
using Behaviours.Alives;

namespace Classes.Engine
{
	public class AnimsMachine
	{
		public interface AMProperty
		{
		}
		public struct AMBool : AMProperty
		{
			public string name;
			public DelegatesData.AliveBool property;

			public AMBool (string _name, DelegatesData.AliveBool _prop) {
				name = _name;
				property = _prop;
			}
		}
		public struct AMFloat : AMProperty
		{
			public string name;
			public DelegatesData.AliveFloat property;

			public AMFloat (string _name, DelegatesData.AliveFloat _prop) {
				name = _name;
				property = _prop;
			}
		}

		public AMProperty[] properties { get; private set;}
		public Alive alive { get; private set; }

		public AnimsMachine (Alive _alive, params AMProperty[] _props)
		{
			alive = _alive;
			properties = _props;
		}

		public static AnimsMachine Humanic (Alive human) {
			AMProperty walkL = new AnimsMachine.AMFloat ("MoveL", (Alive alive) => alive.aliveStatus.localVelocity.x / Alive.humanMoveSpeed);
			AMProperty walkF = new AnimsMachine.AMFloat ("MoveF", (Alive alive) => alive.aliveStatus.localVelocity.z / Alive.humanMoveSpeed);
			AMProperty crouch = new AnimsMachine.AMFloat ("Crouch", (Alive alive) => Mathf.Lerp(human.anims.GetFloat("Crouch"), ((Character)alive).crouching ? 1 : 0, Time.fixedDeltaTime * 4));
			AMProperty onGround = new AnimsMachine.AMBool ("OnGround", (Alive alive) => alive.onGround);
			return new AnimsMachine (human, walkF, walkL, crouch, onGround);
		}

		public void AnimsUpdate ()
		{
			foreach (var item in properties)
			{
				SetWithParam (item);
			}
		}
		private void SetWithParam (AMProperty param) {
			if (param is AMBool)
			{
				AMBool b = ((AMBool)param);
				alive.anims.SetBool (b.name, b.property (alive));
			}
			if (param is AMFloat)
			{
				AMFloat f = ((AMFloat)param);
				alive.anims.SetFloat (f.name, f.property (alive));
			}
		}
	}
}


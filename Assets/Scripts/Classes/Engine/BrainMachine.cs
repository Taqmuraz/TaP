using UnityEngine;
using Delegates;
using Behaviours;
using InputData;
using Behaviours.Alives;

namespace Classes.Engine
{
	public class BrainMachine
	{

		public struct BrainBlock
		{
			public DelegatesData.AliveBool property;
			public DelegatesData.AliveDo action;

			public BrainBlock (DelegatesData.AliveBool _prop, DelegatesData.AliveDo _action) {
				property = _prop;
				action = _action;
			}
		}

		public Alive alive { get; private set; }
		public BrainBlock[] blocks { get; private set; }

		public BrainMachine (Alive _alive, params BrainBlock[] _blocks)
		{
			alive = _alive;
			blocks = _blocks;
		}

		public static BrainMachine Humanic_AI (Alive alive) {
			return new BrainMachine (alive);
		}
		public static BrainMachine Humanic_Player (Alive alive) {
			BrainBlock move = new BrainBlock ((Alive al) => InputDatabase.inputMove.magnitude > 0, (Alive al) => al.MoveAt(InputDatabase.inputMove));
			BrainBlock look = new BrainBlock ((Alive al) => true, (Alive al) => ((Character)al).LookAt(CameraController.lookPoint));
			BrainBlock crouch = new BrainBlock ((Alive al) => true, (Alive al) => ((Character)al).crouching = InputDatabase.inputCrouch);
			BrainBlock jump = new BrainBlock ((Alive al) => InputDatabase.inputJump, (Alive al) => ((Character)al).Jump ());
			return new BrainMachine (alive, move, look, crouch, jump);
		}

		public void BrainUpdate () {
			foreach (var item in blocks) {
				if (item.property(alive)) {
					item.action (alive);
				}
			}
		}
	}
}


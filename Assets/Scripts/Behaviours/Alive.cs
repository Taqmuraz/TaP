using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Classes;
using Classes.SaveLoadSystem;
using Classes.Engine;
using Statuses;
using Enums;
using System.Linq;

namespace Behaviours
{
	public class Alive : MonoBehaviour
	{
		public AliveData data { get; set; }
		public Rigidbody body { get; private set; }
		public CapsuleCollider coll { get; private set; }
		public Transform trans { get; private set; }
		public Animator anims { get; private set; }
		public AnimsMachine animsMachine { get; private set; }
		public AliveStatus aliveStatus { get; private set; }
		public BrainMachine brainMachine { get; private set; }
		[SerializeField]
		private AliveType aliveType;

		public const float humanMoveSpeed = 3f;
		public const float headOffset = 1.6f;
		public const float jumpForceOnKG = 300;

		public static Alive player { get; private set; }

		private bool on_Ground_get;
		public bool onGround
		{
			get {
				return on_Ground_get || movingUpstairs;
			}
			private set {
				if (on_Ground_get != value && value && !movingUpstairs) {
					Stun (0.5f);
					anims.Play ("ToGround");
				}
				on_Ground_get = value;
			}
		}

		public bool isMoving
		{
			get {
				return velocity.magnitude > 0.2f;
			}
		}

		public Vector3 velocity {
			get
			{
				return body.velocity;
			} 
			private set
			{
				body.velocity = new Vector3 (value.x, body.velocity.y, value.z);
			}
		}

		public bool stunned
		{
			get {
				return IsInvoking ("Stun_End");
			}
		}

		public bool movingUpstairs
		{
			get {
				return IsInvoking ("MoveUpstairs_End");
			}
		}

		public virtual void LookAt (Vector3 at) {
			Vector3 dir = (at - trans.position).normalized;
			dir = new Vector3 (dir.x, 0, dir.z);
			dir = dir.magnitude > 0 ? dir : trans.forward;
			Quaternion r = Quaternion.LookRotation (dir);
			trans.rotation = Quaternion.Slerp (trans.rotation, r, Time.fixedDeltaTime * 5);
		}

		public virtual void Start () {
			body = GetComponent<Rigidbody> ();
			coll = GetComponent<CapsuleCollider> ();
			trans = transform;
			anims = GetComponent<Animator> ();
			animsMachine = AnimsMachine.Humanic (this);
			aliveStatus = new AliveStatus (this);
			switch (aliveType) {
			case AliveType.Human_AI:
				brainMachine = BrainMachine.Humanic_AI (this);
				break;
			case AliveType.Human_Player:
				brainMachine = BrainMachine.Humanic_Player (this);
				player = this;
				break;
			}
		}
		private void OnGroundCheck () {
			onGround = Physics.OverlapSphere (trans.position, 0.25f).FirstOrDefault ((Collider c) => !c.isTrigger && c != coll);
		}
		private void Stun (float time) {
			Invoke ("Stun_End", time);
		}
		private void Stun_End () {
			return;
		}
		private void FixedUpdate () {
			OnGroundCheck ();
			brainMachine.BrainUpdate ();
			animsMachine.AnimsUpdate ();
		}
		public void MoveAt (Vector3 direction) {
			if (onGround && !stunned) {
				direction = new Vector3 (direction.x, 0, direction.z);
				direction = direction.magnitude > 1 ? direction.normalized : direction;
				MoveUpstairs (direction);
				velocity = direction * humanMoveSpeed;
			} else {
				if (movingUpstairs) {
					MoveUpstairs (direction);
				}
			}
		}
		private Vector3 move_upstairs_point;
		private void MoveUpstairs (Vector3 dir) {
			if (!movingUpstairs) {
				Ray check_forward = new Ray (trans.position + Vector3.up * 0.1f, dir);
				RaycastHit hit;
				move_upstairs_point = trans.position;
				if (Physics.Raycast (check_forward, out hit, coll.radius / 2)) {
					Ray check_stair = new Ray (hit.point + Vector3.up * 0.2f + dir * 0.05f, Vector3.down);
					if (Physics.Raycast (check_stair, out hit, 0.2f)) {
						if (Vector3.Angle (trans.up, hit.normal) < 5) {
							move_upstairs_point = hit.point;
						}
					}
				}
				Invoke ("MoveUpstairs_End", 0.2f);
			} else {
				trans.position = Vector3.Slerp (trans.position, move_upstairs_point, Time.fixedDeltaTime * 4);
			}
		}
		private void MoveUpstairs_End () {
			return;
		}
		private Vector3 pre_jump_velocity;
		public void Jump () {
			if (onGround && !stunned) {
				Stun (0.5f);
				pre_jump_velocity = velocity;
				velocity = Vector3.zero;
				anims.Play ("Jump");
				Invoke ("Jump_End", 0.25f);
			}
		}
		private void Jump_End () {
			body.AddForce ((trans.up + pre_jump_velocity.normalized).normalized * jumpForceOnKG * body.mass);
		}
	}
}

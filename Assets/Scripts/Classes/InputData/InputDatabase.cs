using UnityEngine;
using Behaviours;

namespace InputData
{
	public class InputDatabase
	{
		public static Vector3 inputMove
		{
			get {
				Vector3 v = CameraController.camTrans ? CameraController.FromCameraAxes(new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) : Vector3.zero;
				return v;
			}
		}
		public static Vector3 inputCamera
		{
			get {
				return new Vector3 (-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"));
			}
		}
		public static bool inputCrouch
		{
			get {
				return Input.GetKey (KeyCode.LeftControl);
			}
		}
		private static bool jump_get;
		public static bool inputJump
		{
			get {
				bool b = jump_get;
				jump_get = jump_get ? false : Input.GetKeyDown (KeyCode.Space);
				return b;
			}
		}
	}
}


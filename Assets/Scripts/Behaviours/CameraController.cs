using UnityEngine;
using System.Collections;
using InputData;

namespace Behaviours
{
	public class CameraController : MonoBehaviour
	{
		public static Camera cameraMain { get; private set; }
		public static Transform camTrans { get; private set; }
		public static CameraController camControl { get; private set; }

		public static Vector3 lookPoint
		{
			get {
				Vector3 v = camTrans ? camTrans.position + camTrans.forward * 1000 : Vector3.zero;
				return v;
			}
		}

		private Vector3 cameraEuler;

		private void Awake () {
			cameraMain = GetComponent<Camera> ();
			camTrans = transform;
			cameraEuler = camTrans.eulerAngles;
			camControl = this;
		}
		public void OnPreRender () {
			CameraMotor ();
		}
		private void CameraMotor () {
			cameraEuler += InputDatabase.inputCamera * 5;
			cameraEuler.x = Mathf.Clamp (cameraEuler.x, -89f, 89f);
			camTrans.rotation = Quaternion.Slerp (camTrans.rotation, Quaternion.Euler (cameraEuler), Time.fixedDeltaTime * 5);
			if (Alive.player) {
				float dist = 4f;
				RaycastHit hit;
				if (Physics.Raycast(camTrans.position, -camTrans.forward, out hit)) {
					dist = hit.distance < dist ? hit.distance : dist;
				}
				Vector3 d = -camTrans.forward * dist;
				Vector3 p = Alive.player.trans.position + Alive.player.trans.up * Alive.headOffset + d;
				camTrans.position = Vector3.Slerp (camTrans.position, p, Time.fixedDeltaTime * 8);
			}
		}
		public static Vector3 FromCameraAxes (Vector3 origin) {
			Vector3 a = camTrans.TransformDirection (origin);
			a = new Vector3 (a.x, 0, a.z).normalized * a.magnitude;
			return a;
		}
	}
}

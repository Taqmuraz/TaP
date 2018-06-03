using UnityEngine;
using System.Collections;

namespace Behaviours
{
	public class WeaponHold : MonoBehaviour
	{
		[SerializeField]
		private Transform leftHandGet;
		public Transform leftHandPoint
		{
			get {
				return leftHandGet;
			}
		}
		[SerializeField]
		private Transform rightHandGet;
		public Transform rightHandPoint
		{
			get {
				return rightHandGet;
			}
		}
		public Transform trans { get; private set; }

		private void Start () {
			trans = transform;
		}
	}
}


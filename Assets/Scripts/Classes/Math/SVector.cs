using UnityEngine;

namespace Classes.Math
{
	public struct SVector
	{
		public float x;
		public float y;
		public float z;

		public SVector (float _x, float _y, float _z) {
			x = _x;
			y = _y;
			z = _z;
		}

		public static implicit operator SVector (Vector3 v) {
			return new SVector (v.x, v.y, v.z);
		}
		public static implicit operator SVector (Vector2 v) {
			return new SVector (v.x, v.y, 0);
		}
		public static implicit operator Vector3 (SVector v) {
			return new Vector3 (v.x, v.y, v.z);
		}
		public static implicit operator Vector2 (SVector v) {
			return new Vector3 (v.x, v.y, 0);
		}
		public static SVector operator + (SVector a, SVector b) {
			return new SVector (a.x + b.x, a.y + b.y, a.z + b.z);
		}
		public static SVector operator - (SVector a, SVector b) {
			return new SVector (a.x - b.x, a.y - b.y, a.z - b.z);
		}
		public static SVector operator * (SVector a, float b) {
			return new SVector (a.x * b, a.y * b, a.z * b);
		}
		public static SVector operator / (SVector a, float b) {
			return new SVector (a.x / b, a.y / b, a.z / b);
		}

		public float magnitude
		{
			get {
				return Mathf.Sqrt (x * x + y * y + z * z);
			}
		}
		public SVector normallized
		{
			get {
				return this / magnitude;
			}
		}
	}
}


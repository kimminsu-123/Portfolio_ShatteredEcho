using UnityEngine;

namespace ShEcho.Utils
{
	public class GroundStatus
	{
		public bool IsGrounded = false;
		public GameObject Ground = null;
		public Vector3 Normal = Vector3.zero;
		public Vector3 Point = Vector3.zero;

		public void CopyFrom(GroundStatus gs)
		{
			IsGrounded = gs.IsGrounded;
			Ground = gs.Ground;
			Normal = gs.Normal;
			Point = gs.Point;
		}
	}
}
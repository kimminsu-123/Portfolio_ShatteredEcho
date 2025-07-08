using UnityEngine;

namespace ShEcho.Utils
{
	public class Global
	{
		public class PlayerAnimation
		{
			public static readonly int HashMagnitude = Animator.StringToHash("Magnitude");
			public static readonly int HashIsGround = Animator.StringToHash("IsGround");
			public static readonly int HashJump = Animator.StringToHash("Jump");
		}
	}
	
	public class GroundStatus
	{
		public enum Status
		{
			Ungrounded,
			Flatted,
			Sloped
		}
		
		public Status CurrentStatus = Status.Ungrounded;
		public GameObject Ground = null;
		public Vector3 Normal = Vector3.zero;
		public Vector3 Point = Vector3.zero;

		public void CopyFrom(GroundStatus gs)
		{
			CurrentStatus = gs.CurrentStatus;
			Ground = gs.Ground;
			Normal = gs.Normal;
			Point = gs.Point;
		}

		public void Reset()
		{
			CurrentStatus = Status.Ungrounded;
			Ground = null;
			Normal = Vector3.zero;
			Point = Vector3.zero;
		}
	}
}
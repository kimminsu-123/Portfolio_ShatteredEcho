using UnityEngine;

namespace ShEcho.Utils
{
	public static class VectorExtensions
	{
		public static bool Approximately(this Vector2 lhs, Vector2 rhs)
		{
			bool ret = true;
			
			ret &= Mathf.Approximately(lhs.x, rhs.x);
			ret &= Mathf.Approximately(lhs.x, rhs.x);
			
			return ret;
		}
		
		public static bool Approximately(this Vector3 lhs, Vector3 rhs)
		{
			bool ret = true;
			
			ret &= Mathf.Approximately(lhs.x, rhs.x);
			ret &= Mathf.Approximately(lhs.x, rhs.x);
			ret &= Mathf.Approximately(lhs.x, rhs.x);
			
			return ret;
		}
	}
}
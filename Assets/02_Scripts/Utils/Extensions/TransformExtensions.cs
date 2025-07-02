using UnityEngine;

namespace ShEcho.Utils
{
	public static class TransformExtensions
	{
		public static RectTransform GetCanvasRectTransform(this Transform transform)
		{
			var parentTransform = transform.parent;
			return parentTransform != null ? parentTransform.GetComponentInParent<RectTransform>() : null;
		}
	}
}
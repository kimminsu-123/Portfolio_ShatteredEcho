using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace ShEcho.Utils.InputSystem.BindingComposite
{
	[DisplayStringFormat("{firstTouch}+{secondTouch}")]
	public class PinchComposite : InputBindingComposite<float>
	{
		private struct TouchStateComparer : IComparer<TouchState>
		{
			public int Compare(TouchState x, TouchState y) => 1;
		}

		[InputControl(layout = "Vector2")]
		public int FirstTouch;
		[InputControl(layout = "Vector2")]
		public int SecondTouch;

		public float Negative = -1f;
		public float Positive = 1f;

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init() { }
		
		public override float ReadValue(ref InputBindingCompositeContext context)
		{
			TouchState touch0 = context.ReadValue<TouchState, TouchStateComparer>(FirstTouch);
			TouchState touch1 = context.ReadValue<TouchState, TouchStateComparer>(SecondTouch);

			if (touch0.phase != TouchPhase.Moved || touch1.phase != TouchPhase.Moved)
				return 0f;

			float startDistance = math.distance(touch0.startPosition, touch1.startPosition);
			float distance = math.distance(touch0.position, touch1.position);

			float unscaledValue = startDistance / distance - 1f; 
			
			return unscaledValue * (unscaledValue < 0 ? Negative : Positive);
		}

		public override float EvaluateMagnitude(ref InputBindingCompositeContext context) => 1f;
		
		static PinchComposite() => UnityEngine.InputSystem.InputSystem.RegisterBindingComposite<PinchComposite>();
	}
}
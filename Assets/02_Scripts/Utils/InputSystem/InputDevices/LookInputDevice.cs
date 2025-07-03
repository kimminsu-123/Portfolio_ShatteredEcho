#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.Layouts;

namespace ShEcho.Utils.InputSystem.InputDevices
{
	#if UNITY_EDITOR
	[InitializeOnLoad]
	#endif
	[InputControlLayout(displayName = "LookInput", stateType = typeof(LookInputState))]
	public class LookInputDevice : InputDevice
	{
		static LookInputDevice() => UnityEngine.InputSystem.InputSystem.RegisterLayout<LookInputDevice>(name: "LookInputDevice");

		[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
		private static void Init() { }

		[InputControl(name = "Delta")] public Vector2Control Control { get; private set; }
		
		protected override void FinishSetup()
		{
			base.FinishSetup();
			Control = GetChildControl<Vector2Control>("Delta");
		}
	}
}
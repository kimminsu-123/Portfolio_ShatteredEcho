using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Utilities;

namespace ShEcho.Utils.InputSystem
{
	[StructLayout(LayoutKind.Explicit, Size = 8)]
	public struct LookInputState : IInputStateTypeInfo
	{
		public FourCC format => new('L', 'O', 'O', 'K');

		[InputControl(name = "Delta", layout = "Vector2")]
		[FieldOffset(0)]
		public Vector2 control;
	}
}
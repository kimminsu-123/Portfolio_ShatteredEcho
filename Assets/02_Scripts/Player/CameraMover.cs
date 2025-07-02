using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShEcho.Player
{
	[RequireComponent(typeof(CinemachineCamera), typeof(CinemachineOrbitalFollow))]
	public class CameraMover : MonoBehaviour
	{
		public InputActionReference moveInputAction;

		public float horizontalSpeed = 6f;
		public float verticalSpeed = 4f;

		private CinemachineOrbitalFollow _orbitalFollow;

		private void Awake()
		{
			_orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
		}
		
		private void LateUpdate()
		{
			UpdateMove();
		}

		private void UpdateMove()
		{
			Vector2 look = moveInputAction.action.ReadValue<Vector2>();
			InputAxis horizontalAxis = _orbitalFollow.HorizontalAxis;
			InputAxis verticalAxis = _orbitalFollow.VerticalAxis;
			
			_orbitalFollow.HorizontalAxis.Value += look.x * horizontalSpeed;
			_orbitalFollow.VerticalAxis.Value += look.y * verticalSpeed;
			
			_orbitalFollow.HorizontalAxis.Value = horizontalAxis.ClampValue(_orbitalFollow.HorizontalAxis.Value);
			_orbitalFollow.VerticalAxis.Value = verticalAxis.ClampValue(_orbitalFollow.VerticalAxis.Value);	
		}
	}
}
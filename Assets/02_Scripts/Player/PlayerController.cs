using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Logger = ShEcho.Utils.Logger;

namespace ShEcho.Player
{
	[RequireComponent(typeof(CapsuleCollider), typeof(PlayerMotor))]
	public class PlayerController : MonoBehaviour
	{
		[Header("입력 액션 들")]
		public InputActionReference moveInputAction;
		public InputActionReference jumpInputAction;

		private PlayerMotor _motor;
		private Camera _mainCam;

		private void Awake()
		{
			_motor = GetComponent<PlayerMotor>();
		}

		private void Start()
		{
			_mainCam = Camera.main;
		}

		private void Update()
		{
			Vector2 input = Vector3.ClampMagnitude(moveInputAction.action.ReadValue<Vector2>(), 1f);
			
			CalculateCameraDirection(input, out Vector3 direction);
			
			if (jumpInputAction.action.WasPerformedThisFrame())
			{
				_motor.Jump();
			}

			_motor.CurrentDirection = direction;
			_motor.Rotate();
			_motor.Move();
		}
	
		private void CalculateCameraDirection(Vector3 input, out Vector3 direction)
		{
			Vector3 camForward = _mainCam.transform.forward;
			camForward.y = 0f;
			camForward.Normalize();
				
			Vector3 camRight = _mainCam.transform.right;
			camRight.y = 0;
			camRight.Normalize();
				
			direction = (camForward * input.y + camRight * input.x).normalized * input.magnitude;
		}
	}
}
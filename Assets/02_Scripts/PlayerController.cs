using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShEcho.Player
{
	public class PlayerController : MonoBehaviour
	{
		[Header("입력 액션 들")]
		public InputActionReference moveInputAction;
		public InputActionReference sprintInputAction;
		
		[Header("이동 관련 속성들")]
		public float walkSpeed = 2f;
		public float sprintSpeed = 5f;
		public float rotateSpeed = 10f;

		private Camera _mainCam;

		private bool _isSprint;

		private void Start()
		{
			_mainCam = Camera.main;
			
			sprintInputAction.action.started += _ => _isSprint = true;
			sprintInputAction.action.canceled += _ => _isSprint = false;
		}

		public void Update()
		{
			Vector2 input = moveInputAction.action.ReadValue<Vector2>();

			if (input.sqrMagnitude > 0f)
			{
				CalculateCameraDirection(input, out Vector3 direction);
				Rotate(direction);
				Move(direction);	
			}
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

		private void Move(Vector3 direction)
		{
			float moveSpeed = _isSprint ? sprintSpeed : walkSpeed;
			transform.position += direction * (moveSpeed * Time.deltaTime);
		}

		private void Rotate(Vector3 direction)
		{
			float rotateStep = rotateSpeed * Time.deltaTime;
			Vector3 rotateTowards = Vector3.RotateTowards(transform.forward, direction, rotateStep, 0f);
			transform.rotation = Quaternion.LookRotation(rotateTowards);
		}
	}
}
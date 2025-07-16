using ShEcho.Controller;
using ShEcho.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ShEcho.Player
{
	[RequireComponent(typeof(CapsuleCollider), typeof(PlayerMotor), typeof(SwapTimeController))]
	[RequireComponent(typeof(InteractController))]
	public class PlayerController : MonoBehaviour
	{
		[Header("입력 액션 들")]
		public InputActionReference moveInputAction;
		public InputActionReference jumpInputAction;
		public InputActionReference swapTimeInputAction;
		public InputActionReference interactInputAction;

		[Header("애니메이션")] 
		public Animator modelAnimator;
		
		private PlayerMotor _motor;
		private SwapTimeController _swapTimeController;
		private InteractController _interactController;
		private Camera _mainCam;

		private void Awake()
		{
			_motor = GetComponent<PlayerMotor>();
			_swapTimeController = GetComponent<SwapTimeController>();
			_interactController = GetComponent<InteractController>();
		}

		private void Start()
		{
			_mainCam = Camera.main;
		}

		private void Update()
		{
			Vector2 input = Vector3.ClampMagnitude(moveInputAction.action.ReadValue<Vector2>(), 1f);

			UpdateDirection(input);
			UpdateAnimation(input);

			if (swapTimeInputAction.action.WasPerformedThisFrame())
			{
				_swapTimeController.SwapTime();
			}

			if (interactInputAction.action.WasPerformedThisFrame())
			{
				_interactController.Interact();
			}
		}

		private void UpdateDirection(Vector2 input)
		{
			CalculateCameraDirection(input, out Vector3 direction);
			
			_motor.CurrentDirection = direction;
		}

		private void UpdateAnimation(Vector2 input)
		{
			modelAnimator.SetFloat(Global.PlayerAnimation.HashMagnitude, input.magnitude);

			GroundStatus.Status status = _motor.GroundChecker.CurrentGroundStatus.CurrentStatus;
			modelAnimator.SetBool(Global.PlayerAnimation.HashIsGround, status != GroundStatus.Status.Ungrounded);
		}
		
		private void FixedUpdate()
		{
			_motor.Rotate();
			_motor.Move();
			
			if (jumpInputAction.action.WasPerformedThisFrame())
			{
				bool success = _motor.Jump();
				if (success)
				{
					modelAnimator.CrossFade(Global.PlayerAnimation.HashJump, 0f);
				}
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

		public void Teleport(Vector3 position, Quaternion rotation)
		{
			_motor.SetPosition(position);
			_motor.SetRotation(rotation);
		}
	}
}
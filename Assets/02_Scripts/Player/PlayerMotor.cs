using System;
using NaughtyAttributes;
using ShEcho.Utils;
using ShEcho.Utils.Entities;
using UnityEngine;
using Logger = ShEcho.Utils.Logger;

namespace ShEcho.Player
{
	[RequireComponent(typeof(GroundChecker), typeof(CapsuleCollider), typeof(Rigidbody))]
	public class PlayerMotor : MonoBehaviour
	{
		[Header("이동 관련 세팅")]
		[MinMaxSlider(1f, 50f)] public Vector2 minMaxMoveSpeed = new(4f, 7f);
		public float jumpHeight = 1.5f;

		[Header("경사로/계단 관련 세팅")]
		[Range(0f, 80f)] public float maxSlopeAngle = 60f;
		[Range(0f, 10f)] public float minStepHeight = 0.3f;

		public Vector3 CurrentDirection { get; set; }

		public GroundChecker GroundChecker { get; private set; }
		public Rigidbody CachedRigidbody { get; private set; }

		private void Awake()
		{
			GroundChecker = GetComponent<GroundChecker>();
			CachedRigidbody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			GroundChecker.CheckGround();
		}

		private bool CheckSlope()
		{
			if (!GroundChecker.CurrentGroundStatus.IsGrounded) return false;
			
			GroundStatus curGroundStatus = GroundChecker.CurrentGroundStatus;
			Vector3 groundNormal = curGroundStatus.Normal;
			
			float dot = Vector3.Dot(transform.up, groundNormal);
			float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

			return !Mathf.Approximately(angle, 0f) && angle <= maxSlopeAngle;
		}

		public void ForceUnGround()
		{
			CachedRigidbody.position += Vector3.up * 0.1f;
		}
		
		public void Move()
		{
			// 앞으로 Ray를 쏴서 다음지점의각도를 미리 구해야할 듯
			
			float moveSpeed = Mathf.Lerp(minMaxMoveSpeed.x, minMaxMoveSpeed.y, CurrentDirection.magnitude);

			Vector3 velocity = CurrentDirection * moveSpeed;
			bool isSlope = CheckSlope();
			if (isSlope)
			{
				Vector3 projection = Vector3.ProjectOnPlane(CurrentDirection, GroundChecker.CurrentGroundStatus.Normal).normalized;
				velocity = projection * velocity.magnitude;
				velocity += Vector3.down * Mathf.Abs(CachedRigidbody.linearVelocity.y);
			}
			else
			{
				velocity.y = CachedRigidbody.linearVelocity.y;
			}

			CachedRigidbody.linearVelocity = velocity;
		}

		public void Rotate()
		{
			if (CurrentDirection != Vector3.zero)
			{
				CachedRigidbody.rotation = Quaternion.LookRotation(CurrentDirection);
			}
		}

		public void Jump()
		{
			if (GroundChecker.CurrentGroundStatus.IsGrounded)
			{
				ForceUnGround();
				
				float jumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(Physics.gravity.y) * jumpHeight);
				Vector3 velocity = CachedRigidbody.linearVelocity;
				velocity.y = jumpVelocity;
				CachedRigidbody.linearVelocity = velocity;
			}
		}
	}
}
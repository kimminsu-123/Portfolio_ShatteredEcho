using System;
using NaughtyAttributes;
using ShEcho.Utils;
using ShEcho.Utils.Entities;
using UnityEngine;

namespace ShEcho.Player
{
	[RequireComponent(typeof(GroundChecker), typeof(CapsuleCollider), typeof(Rigidbody))]
	public class PlayerMotor : MonoBehaviour
	{
		[Header("이동 관련 세팅")]
		[MinMaxSlider(1f, 50f)] public Vector2 minMaxMoveSpeed = new(4f, 7f);
		public float jumpHeight = 1.5f;

		public Vector3 CurrentDirection { get; set; }

		public GroundChecker GroundChecker { get; private set; }
		public StairChecker StairChecker { get; private set; }
		public Rigidbody CachedRigidbody { get; private set; }

		private void Awake()
		{
			GroundChecker = GetComponent<GroundChecker>();
			StairChecker = GetComponent<StairChecker>();
			CachedRigidbody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			GroundChecker.CheckGround();
		}

		private void MoveOnSlope(float moveSpeed)
		{
			Vector3 velocity = CurrentDirection * moveSpeed;
			Vector3 projection = Vector3.ProjectOnPlane(CurrentDirection, GroundChecker.CurrentGroundStatus.Normal).normalized;

			CachedRigidbody.useGravity = false;
			CachedRigidbody.linearVelocity = projection * velocity.magnitude;
		}

		private void MoveOnFlatGround(float moveSpeed)
		{
			Vector3 velocity = CurrentDirection * moveSpeed;
			velocity.y = CachedRigidbody.linearVelocity.y;

			CachedRigidbody.useGravity = true;
			CachedRigidbody.linearVelocity = velocity;
		}

		public void ForceUnGround()
		{
			CachedRigidbody.position += Vector3.up * GroundChecker.distance;
		}
		
		public void Move()
		{
			float moveSpeed = Mathf.Lerp(minMaxMoveSpeed.x, minMaxMoveSpeed.y, CurrentDirection.magnitude);

			bool isStair = StairChecker.CheckStair();
			if (isStair && CurrentDirection.sqrMagnitude > 0f)
			{
				CachedRigidbody.position += Vector3.up * StairChecker.stepHeight;
			}
			else
			{
				GroundStatus.Status status = GroundChecker.CurrentGroundStatus.CurrentStatus;
				switch (status)
				{
					case GroundStatus.Status.Ungrounded:
					case GroundStatus.Status.Flatted:
						MoveOnFlatGround(moveSpeed);
						break;
					case GroundStatus.Status.Sloped:
						MoveOnSlope(moveSpeed);
						break;
				}	
			}
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
			if (GroundChecker.CurrentGroundStatus.CurrentStatus != GroundStatus.Status.Ungrounded)
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
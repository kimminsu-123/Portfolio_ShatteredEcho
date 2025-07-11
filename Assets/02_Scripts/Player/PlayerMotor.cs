using System;
using NaughtyAttributes;
using ShEcho.Utils;
using ShEcho.Utils.Entities;
using UnityEngine;

namespace ShEcho.Player
{
	[RequireComponent(typeof(GroundChecker), typeof(Rigidbody))]
	public class PlayerMotor : MonoBehaviour
	{
		[Header("이동 관련 세팅")]
		[MinMaxSlider(1f, 50f)] public Vector2 minMaxMoveSpeed = new(4f, 7f);
		public float jumpForce = 1.5f;

		public Vector3 CurrentDirection { get; set; }

		public GroundChecker GroundChecker { get; private set; }
		public Rigidbody CachedRigidbody { get; private set; }

		private float _curMoveSpeed;

		private void Awake()
		{
			GroundChecker = GetComponent<GroundChecker>();
			CachedRigidbody = GetComponent<Rigidbody>();
		}

		private void Start()
		{
			CachedRigidbody.freezeRotation = true;
		}

		private void Update()
		{
			_curMoveSpeed = Mathf.Lerp(minMaxMoveSpeed.x, minMaxMoveSpeed.y, CurrentDirection.magnitude);
		}

		private void FixedUpdate()
		{
			GroundChecker.CheckGround();
		}

		private void MoveOnSlope()
		{
			Vector3 projection = Vector3.ProjectOnPlane(CurrentDirection, GroundChecker.CurrentGroundStatus.Normal).normalized;

			CachedRigidbody.useGravity = false;
			CachedRigidbody.linearVelocity = projection * _curMoveSpeed;
		}

		private void MoveOnFlatGround()
		{
			CachedRigidbody.useGravity = true;

			Vector3 velocity = CurrentDirection * _curMoveSpeed;
			velocity.y = CachedRigidbody.linearVelocity.y;
			CachedRigidbody.linearVelocity = velocity;
		}

		public void Move()
		{
			GroundStatus.Status status = GroundChecker.CurrentGroundStatus.CurrentStatus;
			switch (status)
			{
				case GroundStatus.Status.Ungrounded:
				case GroundStatus.Status.Flatted:
					MoveOnFlatGround();
					break;
				case GroundStatus.Status.Sloped:
					MoveOnSlope();
					break;
			}	
		}
		
		public void Rotate()
		{
			if (CurrentDirection != Vector3.zero)
			{
				CachedRigidbody.rotation = Quaternion.LookRotation(CurrentDirection);
			}
		}

		public bool Jump()
		{
			if (GroundChecker.CurrentGroundStatus.CurrentStatus != GroundStatus.Status.Ungrounded)
			{
				GroundChecker.ForceUnGround();
				
				Vector3 velocity = CachedRigidbody.linearVelocity;
				velocity.y = 0f;
				CachedRigidbody.linearVelocity = velocity;

				CachedRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

				return true;
			}

			return false;
		}

		public void SetPosition(Vector3 position)
		{
			CachedRigidbody.position = position;
		}

		public void SetRotation(Quaternion rotation)
		{
			CachedRigidbody.rotation = rotation;
		}
	}
}
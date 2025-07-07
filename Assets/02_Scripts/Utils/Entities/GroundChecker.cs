using System;
using System.Linq;
using UnityEngine;

namespace ShEcho.Utils.Entities
{
	public class GroundChecker : MonoBehaviour
	{
		public bool selfCall;
		[Range(0f, 80f)] public float maxSlopeAngle = 60f;
		public float distance = 0.2f;
		public Vector3 offset = new(0f, 0.01f, 0f);
		public LayerMask groundLayer;

		public GroundStatus CurrentGroundStatus { get; private set; }
		public GroundStatus PreviousGroundStatus { get; private set; }
		private RaycastHit[] _hits;
		
		private void Start()
		{
			CurrentGroundStatus = new GroundStatus();
			PreviousGroundStatus = new GroundStatus();
			_hits = new RaycastHit[1];
		}

		private void FixedUpdate()
		{
			if (selfCall)
			{
				CheckGround();
			}
		}

		public void CheckGround()
		{
			UpdateGroundStatus();
			
			PreviousGroundStatus.CopyFrom(CurrentGroundStatus);
		}
		
		private bool CheckSlope()
		{
			float angle = Vector3.Angle(Vector3.up, CurrentGroundStatus.Normal);
			return angle != 0f && angle <= maxSlopeAngle;
		}
		
		private void UpdateGroundStatus()
		{
			Ray ray = new Ray(transform.position + offset, Vector3.down);

			int count = Physics.RaycastNonAlloc(ray, _hits, distance, groundLayer);
			if (count > 0)
			{
				RaycastHit hit = _hits[0];

				CurrentGroundStatus.Ground = hit.collider.gameObject;
				CurrentGroundStatus.Normal = hit.normal;
				CurrentGroundStatus.Point = hit.point;
				CurrentGroundStatus.CurrentStatus = CheckSlope() ? GroundStatus.Status.Sloped : GroundStatus.Status.Flatted;

				Logger.DrawLine(hit.transform.position, hit.transform.position + hit.normal * 10f, Color.yellow);
			}
			else
			{
				CurrentGroundStatus.CurrentStatus = GroundStatus.Status.Ungrounded;
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position + offset, transform.position + offset + Vector3.down * distance);
		}
	}
}
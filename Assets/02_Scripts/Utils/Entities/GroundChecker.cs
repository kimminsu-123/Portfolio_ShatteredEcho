using System.Linq;
using UnityEngine;

namespace ShEcho.Utils.Entities
{
	public class GroundChecker : MonoBehaviour
	{
		public float radius = 0.2f;
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

		public void CheckGround()
		{
			UpdateGroundStatus();
			
			PreviousGroundStatus.CopyFrom(CurrentGroundStatus);
		}

		private void UpdateGroundStatus()
		{
			Ray ray = new Ray(transform.position + offset, Vector3.down);

			int count = Physics.SphereCastNonAlloc(ray, radius, _hits, groundLayer);
			if (count > 0)
			{
				RaycastHit hit = _hits[0];
				
				CurrentGroundStatus.IsGrounded = true;
				CurrentGroundStatus.Ground = hit.collider.gameObject;
				CurrentGroundStatus.Normal = hit.normal;
				CurrentGroundStatus.Point = hit.point;
			}
			else
			{
				CurrentGroundStatus.IsGrounded = false;
				CurrentGroundStatus.Ground = null;
				CurrentGroundStatus.Normal = Vector3.zero;
				CurrentGroundStatus.Point = Vector3.zero;
			}
		}

		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.red;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawWireSphere(offset, radius);
		}
	}
}
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace ShEcho.Utils.Entities
{
	public class StairChecker : MonoBehaviour
	{
		public float stepHeight = 0.3f;
		public float distance = 0.2f;
		public float offset = -0.1f;
		public LayerMask stairLayer;

		private Vector3 BackOffset => Vector3.back * offset; 
			
		private RaycastHit[] _hits;
		private Collider[] _scannedColliders;

		private void Start()
		{
			_hits = new RaycastHit[1];
			_scannedColliders = new Collider[2];
		}

		public bool CheckStair()
		{
			// 이거 조건 검사 더 해야할 듯
			for (int i = 0; i < _scannedColliders.Length; i++)
			{
				_scannedColliders[i] = null;
			}
			
			bool ret = Scan(transform.position + BackOffset, ref _scannedColliders[0]);
			if (ret)
			{
				Scan(transform.position + BackOffset + Vector3.up * stepHeight, ref _scannedColliders[1]);
				
				ret &= _scannedColliders[0] != _scannedColliders[1];
			}
			return ret;
		}

		private bool Scan(Vector3 origin, ref Collider col)
		{
			Ray ray = new Ray(origin, transform.forward);
			
			int count = Physics.RaycastNonAlloc(ray, _hits, distance, stairLayer);
			if (count > 0)
			{
				col = _hits[0].collider;
				return true;
			}
			
			return false;
		}
		
		private void OnDrawGizmosSelected()
		{
			Gizmos.color = Color.yellow;
			Gizmos.matrix = transform.localToWorldMatrix;
			Gizmos.DrawLine(BackOffset, BackOffset + (Vector3.forward * distance));
			Gizmos.DrawLine(Vector3.up * stepHeight + BackOffset, (BackOffset + Vector3.up * stepHeight) + Vector3.forward * distance);
		}
	}
}
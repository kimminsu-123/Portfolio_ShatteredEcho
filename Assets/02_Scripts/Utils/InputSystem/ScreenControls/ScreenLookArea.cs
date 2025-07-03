using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace ShEcho.Utils.InputSystem.ScreenControls
{
	[AddComponentMenu("Input/On-Screen Look Area")]
	public class ScreenLookArea : OnScreenControl, IPointerDownHandler, IDragHandler, IPointerUpHandler
	{
		[InputControl(layout = "Vector2")] [SerializeField]
		private string controlInputPath;

		protected override string controlPathInternal
		{
			get => controlInputPath;
			set => controlInputPath = value;
		}

		private List<RaycastResult> _raycastResults;
		private Vector2 _lastMousePosition;

		private void Start()
		{
			_raycastResults = new List<RaycastResult>(1);
		}

		public void OnPointerDown(PointerEventData eventData)
		{
			_lastMousePosition = eventData.position;
		}

		public void OnDrag(PointerEventData eventData)
		{
			Vector2 delta = Vector2.zero;
			
			if (IsTopMostUI(eventData))
			{
				delta = eventData.position - _lastMousePosition;
			}
			SendValueToControl(delta);
			
			_lastMousePosition = eventData.position;
		}
		
		public void OnPointerUp(PointerEventData eventData)
		{
			SendValueToControl(Vector2.zero);
		}
		
		private bool IsTopMostUI(PointerEventData eventData)
		{
			EventSystem.current.RaycastAll(eventData, _raycastResults);

			bool ret = false;
			if (_raycastResults.Count > 0)
			{
				ret = _raycastResults[0].gameObject == gameObject;
			}
			return ret;
		}
	}
}
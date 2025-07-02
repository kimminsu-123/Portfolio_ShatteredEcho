using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace ShEcho.Utils.InputSystem.ScreenControls
{
	[AddComponentMenu("Input/On-Screen Look Area")]
	public class ScreenLookArea : OnScreenControl, IBeginDragHandler, IDragHandler, IEndDragHandler
	{
		[InputControl(layout = "Vector2")] [SerializeField]
		private string controlInputPath;

		protected override string controlPathInternal
		{
			get => controlInputPath;
			set => controlInputPath = value;
		}

		private RectTransform _cachedRectTr;
		private List<RaycastResult> _raycastResults;
		private Vector2 _delta;

		private void Start()
		{
			_cachedRectTr = transform as RectTransform;
			_raycastResults = new List<RaycastResult>(1);
		}

		public void OnBeginDrag(PointerEventData eventData)
		{
			_delta = Vector2.zero;
		}

		private void Update()
		{
			SendValueToControl(_delta);
			
			_delta = Vector2.zero;
		}

		public void OnDrag(PointerEventData eventData)
		{
			bool isOver = RectTransformUtility.RectangleContainsScreenPoint(_cachedRectTr, eventData.position);
			if (isOver && IsTopMostUI(eventData))
			{
				_delta = eventData.delta;
			}
			else
			{
				_delta = Vector2.zero;
			}
		}
		
		public void OnEndDrag(PointerEventData eventData)
		{
			_delta = Vector2.zero;
			
			SendValueToControl(Vector2.zero);
		}
		
		private bool IsTopMostUI(PointerEventData eventData)
		{
			EventSystem.current.RaycastAll(eventData, _raycastResults);

			return _raycastResults[0].gameObject == gameObject;
		}
	}
}
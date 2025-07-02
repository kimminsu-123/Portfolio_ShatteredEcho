using System;
using ShEcho.Core;
using UnityEngine;

namespace ShEcho.UIs
{
	public abstract class UIBase : MonoBehaviour
	{
		protected abstract Type Type { get; }
		
		private void Awake()
		{
			UIManager.Instance.Register(Type, this);
			
			OnAwake();
		}

		private void OnDestroy()
		{
			try
			{
				UIManager.Instance.Unregister(Type);
			}
			catch (Exception)
			{
				// ignore
			}
			
			OnDestroying();
		}

		protected virtual void OnAwake() { }
		protected virtual void OnDestroying() { }
	}
}
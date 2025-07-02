using System;
using System.Collections.Generic;
using ShEcho.UIs;
using ShEcho.Utils;

namespace ShEcho.Core
{
	public class UIManager : SingletonMonoBehaviour<UIManager>
	{
		private readonly Dictionary<Type, UIBase> _uis = new();
		
		public T Get<T>() where T : UIBase
		{
			Type type = typeof(T);

			if (_uis.TryGetValue(type, out UIBase ui))
			{
				return ui as T;
			}

			return null;
		}

		public void Register(Type type, UIBase ui)
		{
			if (!_uis.TryAdd(type, ui))
			{
				Logger.LogWarning("UIManager", $"이미 등록된 UI 입니다 {type}");
			}
		}

		public void Unregister(Type type)
		{
			if (!_uis.ContainsKey(type))
			{
				Logger.LogWarning("UIManager", $"등록되지 않은 UI 입니다 {type}");
				return;
			}

			_uis.Remove(type);
		}
	}
}
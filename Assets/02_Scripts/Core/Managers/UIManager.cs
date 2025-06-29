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
	}
}
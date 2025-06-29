using System;
using ShEcho.UIs.Player;
using ShEcho.Utils;
using UnityEngine;

namespace ShEcho.Core
{
	public class InputManager : SingletonMonoBehaviour<InputManager>
	{
		private void Awake()
		{
#if UNITY_ANDROID || UNITY_IOS
			UIManager.Instance.Get<MobileInputUI>().gameObject.SetActive(true);
#else
			UIManager.Instance.Get<MobileInputUI>().gameObject.SetActive(false);
#endif
		}
	}
}
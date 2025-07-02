using System;
using ShEcho.UIs.Player;
using ShEcho.Utils;
using Unity.Cinemachine;
using UnityEngine;

namespace ShEcho.Core
{
	public class InputManager : SingletonMonoBehaviour<InputManager>
	{
		protected override void OnAwake()
		{
/*#if UNITY_ANDROID || UNITY_IOS
			UIManager.Instance.Get<MobileInputUI>().gameObject.SetActive(true);
#else
			UIManager.Instance.Get<MobileInputUI>().gameObject.SetActive(false);
#endif*/
		}
	}
}
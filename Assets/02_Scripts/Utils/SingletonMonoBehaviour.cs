using System;
using UnityEngine;

namespace ShEcho.Utils
{
	public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
	{
		public static T Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance = FindFirstObjectByType<T>(FindObjectsInactive.Include);
				}

				if (_instance == null)
				{
					GameObject go = new GameObject(nameof(T));

					_instance = go.AddComponent<T>();
				}
				
				return _instance;
			}
		}

		private static T _instance;

		private void OnDestroy()
		{
			int otherHash = _instance.GetHashCode();
			int myHash = GetHashCode();

			if (otherHash == myHash)
			{
				_instance = null;
			}
		}
	}
}
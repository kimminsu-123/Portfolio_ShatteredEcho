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
				if (_instance != null)
				{
					return _instance;
				}

				_instance = FindFirstObjectByType<T>();
				if (_instance != null)
				{
					return _instance;
				}

				GameObject findGo = GameObject.Find(typeof(T).Name);
				if (findGo != null)
				{
					_instance = findGo.AddComponent<T>();
				}
				else
				{
					_instance = CreateSingleton();
				}

				return _instance;
			}
		}		
		
		private static T _instance;

		private static T CreateSingleton()
		{
			string goName = typeof(T).Name;
			GameObject singleton = new GameObject(goName);
			T component = singleton.AddComponent<T>();

			return component;
		}

		private void Awake()
		{
			OnAwake();
		}

		protected virtual void OnDestroy()
		{
			if (_instance != null)
			{
				if (_instance.GetHashCode().Equals(GetHashCode()))
				{
					_instance = null;
				}
			}

			OnDestroying();
		}

		protected virtual void OnAwake() { }
		protected virtual void OnDestroying() { }
	}
}
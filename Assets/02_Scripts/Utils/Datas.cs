using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace ShEcho.Utils
{
	public class Global
	{
		public static class PlayerAnimation
		{
			public static readonly int HashMagnitude = Animator.StringToHash("Magnitude");
			public static readonly int HashIsGround = Animator.StringToHash("IsGround");
			public static readonly int HashJump = Animator.StringToHash("Jump");
		}

		public static class ShaderProperty
		{
			public static readonly int Intensity = Shader.PropertyToID("_Intensity");
		}

		public const string SPAWN_POINT = "SpawnPoint";

		public const int ANDROID_FRAMERATE = 30;
	}
	
	public class GroundStatus
	{
		public enum Status
		{
			Ungrounded,
			Flatted,
			Sloped
		}
		
		public Status CurrentStatus = Status.Ungrounded;
		public GameObject Ground = null;
		public Vector3 Normal = Vector3.zero;
		public Vector3 Point = Vector3.zero;

		public void CopyFrom(GroundStatus gs)
		{
			CurrentStatus = gs.CurrentStatus;
			Ground = gs.Ground;
			Normal = gs.Normal;
			Point = gs.Point;
		}

		public void Reset()
		{
			CurrentStatus = Status.Ungrounded;
			Ground = null;
			Normal = Vector3.zero;
			Point = Vector3.zero;
		}
	}

	[Serializable]
	public class SceneProfile
	{
		private enum Status
		{
			None,
			Unloading,
			Loading,
		}
		
		public SceneReference sceneRef;
		public SceneType type;
		public bool isActive;
		public AsyncOperation Operation;

		private Status _currentStatus;

		public bool IsDone
		{
			get
			{
				if (Operation == null) return true;

				return Operation.isDone && Operation.progress >= 0.9f;
			}
		}

		public float Progress
		{
			get
			{
				if (Operation == null) return 1f;

				return Operation.progress;
			}
		}

		public void Load()
		{
			Operation = SceneManager.LoadSceneAsync(sceneRef.BuildIndex, LoadSceneMode.Additive);
			_currentStatus = Status.Loading;
		}

		public void Unload()
		{
			Operation = SceneManager.UnloadSceneAsync(sceneRef.BuildIndex);
			_currentStatus = Status.Unloading;
		}

		public void Complete()
		{
			if (_currentStatus == Status.Loading && isActive)
			{
				SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneRef.BuildIndex));
			}
			_currentStatus = Status.None;
		}
		
		public void Reset()
		{
			Operation = null;
		}
	}
}
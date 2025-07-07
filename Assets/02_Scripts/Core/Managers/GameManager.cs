using ShEcho.Utils;
using UnityEngine;
using Application = UnityEngine.Application;

namespace ShEcho.Core
{
	public class GameManager : SingletonMonoBehaviour<GameManager>
	{
		private void Start()
		{
			QualitySettings.vSyncCount = 0;
			Application.targetFrameRate = 30;
		}
	}
}
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ShEcho.UIs.Player
{
	public class MobileInputUI : UIBase
	{
		protected override Type Type => typeof(MobileInputUI);

		[SerializeField] private Image swapTimeCooldownImg;
		[SerializeField] private Image interactCooldownImg;

		public void SetSwapTimeCooldownValue(float value)
		{
			swapTimeCooldownImg.fillAmount = Mathf.Clamp(value, 0f, 1f);
		}

		public void SetInteractCooldownValue(float value)
		{
			interactCooldownImg.fillAmount = Mathf.Clamp(value, 0f, 1f);
		}
	}
}
using System;
using DG.Tweening;
using NaughtyAttributes;
using ShEcho.Core;
using ShEcho.UIs.Player;
using ShEcho.Utils;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using EventType = ShEcho.Utils.EventType;

namespace ShEcho.Controller
{
    public class SwapTimeController : MonoBehaviour
    {
        public ScriptableRendererFeature swapTimeRendererFeature;
        public Material swapTimeMaterial;
        [HorizontalLine(color:EColor.Gray)]
        public AnimationCurve swapTimeEffectCurve;
        public float swapTimeEffectDuration;
        public float swapTimeCooldown = 1f;

        [ShowNonSerializedField] private TimeType _currentTimeType = TimeType.Present;
        private Tweener _swapTimeTweener;
        private CooldownTimer _swapTimeCooldownTimer;
        
        private void Awake()
        {
            _swapTimeCooldownTimer = new CooldownTimer(swapTimeCooldown);
            
            EventBus<EventType>.OnEvent += OnSceneEvent;
        }

        private void OnSceneEvent(EventType evt)
        {
            switch (evt)
            {
                case EventType.BeginSceneGroupLoad:
                    _currentTimeType = TimeType.Present;
                    break;
                case EventType.EndSceneGroupLoad:
                    EventBus<TimeType>.Notify(_currentTimeType);
                    break;
            }
        }

        private void Update()
        {
            _swapTimeCooldownTimer.Tick(Time.deltaTime);
            
            UIManager.Instance.Get<MobileInputUI>().SetSwapTimeCooldownValue(_swapTimeCooldownTimer.CurrentTime / _swapTimeCooldownTimer.Time);
        }

        public void SwapTime()
        {
            if (!_swapTimeCooldownTimer.IsRunning)
            {
                DoEffect();

                _currentTimeType = (TimeType)(((int) _currentTimeType + 1) % ((int) TimeType.Present + 1));
                
                EventBus<TimeType>.Notify(_currentTimeType);
			
                _swapTimeCooldownTimer.SetTime(swapTimeCooldown);
                _swapTimeCooldownTimer.Start();
            }
        }

        private void DoEffect()
        {
            if (_swapTimeTweener != null)
            {
                _swapTimeTweener.Kill();
                _swapTimeTweener = null;
            }
			
            swapTimeRendererFeature.SetActive(true);
            _swapTimeTweener = DOTween
                .To(() => 0f, x => swapTimeMaterial.SetFloat(Global.ShaderProperty.Intensity, x), 1f, swapTimeEffectDuration * 0.5f)
                .SetLoops(2, LoopType.Yoyo)
                .SetEase(swapTimeEffectCurve)
                .SetAutoKill(true)
                .OnComplete(() =>
                {
                    _swapTimeTweener = null;
                    swapTimeRendererFeature.SetActive(false);
                });
        }
        
        private void OnDisable()
        {
            swapTimeRendererFeature.SetActive(false);
            _swapTimeTweener?.Kill();
            _swapTimeTweener = null;
            
            _swapTimeCooldownTimer?.Stop();
            _swapTimeCooldownTimer?.Reset();
        }
    }
}
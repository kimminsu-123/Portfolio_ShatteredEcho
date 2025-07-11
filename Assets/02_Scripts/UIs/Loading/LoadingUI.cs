using System;
using ShEcho.SO.EventChannels;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Logger = ShEcho.Utils.Logger;

namespace ShEcho.UIs.Loading
{
    public class LoadingUI : UIBase
    {
        protected override Type Type => typeof(LoadingUI);

        [SerializeField] private StringEventChannel waitMsgEventChannel;
        [SerializeField] private FloatEventChannel progressEventChannel;
        [SerializeField] private BoolEventChannel uiShowEventChannel;

        [SerializeField] private TMP_Text msgText;
        [SerializeField] private Slider progressSlider;

        protected override void OnAwake()
        {
            waitMsgEventChannel.Register(SetText);
            progressEventChannel.Register(SetProgress);
            uiShowEventChannel.Register(Visibility);   
            
            Visibility(false);
        }

        public void SetText(string txt)
        {
            msgText.text = txt;            
        }

        public void SetProgress(float progress)
        {
            progressSlider.value = progress;
        }

        public void Visibility(bool enable)
        {
            gameObject.SetActive(enable);
        }

        protected override void OnDestroying()
        {
            waitMsgEventChannel.Unregister(SetText);
            progressEventChannel.Unregister(SetProgress);
            uiShowEventChannel.Unregister(Visibility);
        }
    }
}
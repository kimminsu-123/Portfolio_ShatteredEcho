using System;
using System.Linq;
using ShEcho.SO;
using ShEcho.SO.EventChannels;
using ShEcho.Utils;
using TMPro;
using UnityEngine;
using EventType = ShEcho.Utils.EventType;

namespace ShEcho.Core
{
    public class SceneManager : SingletonMonoBehaviour<SceneManager>
    {
        public float delay = 0.3f;
        public SceneGroupSO initializeSceneGroup;
        
        [SerializeField] private StringEventChannel waitMsgEventChannel;
        [SerializeField] private FloatEventChannel progressEventChannel;
        [SerializeField] private BoolEventChannel uiShowEventChannel;
        
        private SceneGroupSO _currentGroup;

        private void Start()
        {
            Load(initializeSceneGroup);
        }

        public async Awaitable Load(SceneGroupSO group)
        {
            if (group == _currentGroup)
            {
                return;
            }
            
            if (group.SceneCount <= 0)
            {
                return;
            }

            uiShowEventChannel.Notify(true);
            EventBus<EventType>.Notify(EventType.BeginSceneGroupLoad);

            await UnloadCurrentSceneGroup();
            await WaitDoneLoadAll();
            await LoadSceneGroup(group);
            await WaitDoneLoadAll();

            await Awaitable.WaitForSecondsAsync(delay);
            
            uiShowEventChannel.Notify(false);
            EventBus<EventType>.Notify(EventType.EndSceneGroupLoad);
        }

        private async Awaitable UnloadCurrentSceneGroup()
        {
            if (_currentGroup == null)
            {
                return;
            }
            
            waitMsgEventChannel.Notify("씬을 언로드 합니다.");
            progressEventChannel.Notify(0f);

            foreach (SceneProfile profile in _currentGroup.profiles)
            {
                if (profile.type == SceneType.System) continue;
                
                profile.Reset();
                profile.Unload();
            }
        }

        private async Awaitable LoadSceneGroup(SceneGroupSO group)
        {
            waitMsgEventChannel.Notify("씬을 로드 합니다.");
            progressEventChannel.Notify(0f);

            foreach (SceneProfile profile in group.profiles)
            {
                profile.Reset();
                profile.Load();
            }

            _currentGroup = group;
        }

        private async Awaitable WaitDoneLoadAll()
        {
            if (_currentGroup == null)
            {
                return;
            }
            
            while (!_currentGroup.IsAllDone)
            {
                progressEventChannel.Notify(_currentGroup.Progress);

                await Awaitable.NextFrameAsync();
            }
            
            foreach (SceneProfile profile in _currentGroup.profiles)
            {
                profile.Complete();
            }
            
            progressEventChannel.Notify(1f);
        }
    }
}
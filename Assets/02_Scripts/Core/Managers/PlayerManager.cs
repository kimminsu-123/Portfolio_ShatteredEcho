using ShEcho.Player;
using ShEcho.Utils;
using UnityEngine;
using EventType = ShEcho.Utils.EventType;
using Logger = ShEcho.Utils.Logger;

namespace ShEcho.Core
{
    public class PlayerManager : SingletonMonoBehaviour<PlayerManager>
    {
        private Transform _cachedPlayerParent;
        private PlayerController _controller;
        
        protected override void OnAwake()
        {
            EventBus<EventType>.OnEvent += OnSceneLoad;

            _controller = FindFirstObjectByType<PlayerController>(FindObjectsInactive.Include);
            if (_controller != null)
            {
                _cachedPlayerParent = _controller.transform.parent;
            }
        }

        private void OnSceneLoad(EventType type)
        {
            if (type == EventType.BeginSceneGroupLoad)
            {
                _controller.gameObject.SetActive(false);
                MovePlayer(_cachedPlayerParent == null ? transform : _cachedPlayerParent);
            }   
            else if (type == EventType.EndSceneGroupLoad)
            {
                GameObject spawnPoint = GameObject.FindWithTag(Global.SPAWN_POINT);
                if (spawnPoint == null)
                {
                    Logger.LogError("PlayerManager", "스폰 포인트가 존재하지 않습니다.");
                    return;
                }

                MovePlayer(spawnPoint.transform);
                _controller.gameObject.SetActive(true);
            }
        }

        private void MovePlayer(Transform parent)
        {
            _controller.Teleport(parent.position, parent.rotation);
            _controller.transform.parent = parent;
        }

        protected override void OnDestroying()
        {
            EventBus<EventType>.OnEvent += OnSceneLoad;
        }
    }
}
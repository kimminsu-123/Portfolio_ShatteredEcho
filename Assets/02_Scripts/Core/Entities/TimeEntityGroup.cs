using ShEcho.Utils;
using UnityEngine;

namespace ShEcho.Core
{
    public class TimeEntityGroup : MonoBehaviour
    {
        public bool useFog;
        public TimeType enableTimeType;

        private ITimeEntity[] _timeEntities;
        
        private void Awake()
        {
            EventBus<TimeType>.OnEvent += OnChangeTimeType;
            
            _timeEntities = GetComponentsInChildren<ITimeEntity>();

            Initialize();
        }

        private void OnChangeTimeType(TimeType type)
        {
            gameObject.SetActive(type == enableTimeType);

            if (gameObject.activeSelf)
            {
                RenderSettings.fog = useFog;
            }
        }

        private void OnDestroy()
        {
            EventBus<TimeType>.OnEvent -= OnChangeTimeType;
        }

        private void Initialize()
        {
            for (int i = 0; _timeEntities != null && i < _timeEntities.Length; i++)
            {
                _timeEntities[i].Initialize();
            }
        }

        private void OnEnable()
        {
            for (int i = 0; _timeEntities != null && i < _timeEntities.Length; i++)
            {
                _timeEntities[i].OnEnabled();
            }
        }

        private void OnDisable()
        {
            for (int i = 0; _timeEntities != null && i < _timeEntities.Length; i++)
            {
                _timeEntities[i].OnDisabled();
            }
        }

        private void Update()
        {
            for (int i = 0; _timeEntities != null && i < _timeEntities.Length; i++)
            {
                _timeEntities[i].OnUpdate();
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; _timeEntities != null && i < _timeEntities.Length; i++)
            {
                _timeEntities[i].OnFixedUpdate();
            }
        }
    }
}
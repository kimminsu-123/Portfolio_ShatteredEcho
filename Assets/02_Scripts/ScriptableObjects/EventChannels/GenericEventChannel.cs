using ShEcho.Utils;
using UnityEngine;
using UnityEngine.Events;

namespace ShEcho.SO.EventChannels
{
    public abstract class GenericEventChannel<T> : BaseSO
    {
        private UnityAction<T> _callback;

        public void Register(UnityAction<T> evt)
        {
            _callback += evt;
        }

        public void Unregister(UnityAction<T> evt)
        {
            _callback -= evt;
        }
        
        public void Notify(T evt)
        {
            _callback?.Invoke(evt);
        }
    }
}
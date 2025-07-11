using ShEcho.Utils;
using UnityEngine;
using UnityEngine.Events;
using Logger = ShEcho.Utils.Logger;

namespace ShEcho.SO.EventChannels
{
    [CreateAssetMenu(fileName = "GenericEventChannel", menuName = "SO/EventChannel/Generic", order = 5)]
    public class GenericEventChannel : BaseSO
    {
        private UnityAction _callback;

        public void Register(UnityAction evt)
        {
            _callback += evt;
        }

        public void Unregister(UnityAction evt)
        {
            _callback -= evt;
        }
        
        public void Notify()
        {
            _callback?.Invoke();
        }
    }
    
    public abstract class GenericEventChannel<T> : BaseSO
    {
        private UnityAction<T> _callback;

        public void Register(UnityAction<T> evt)
        {
            Logger.Log($"{name}", "Register", Color.orange);
            
            _callback += evt;
        }

        public void Unregister(UnityAction<T> evt)
        {
            Logger.Log($"{name}", "Unregister", Color.orange);
            
            _callback -= evt;
        }
        
        public void Notify(T evt)
        {
            Logger.Log($"{name}", "Notify", Color.orange);
            
            _callback?.Invoke(evt);
        }
    }
}
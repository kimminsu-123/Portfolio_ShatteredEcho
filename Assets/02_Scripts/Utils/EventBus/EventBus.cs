using JetBrains.Annotations;

namespace ShEcho.Utils
{
    public class EventBus<T>
    {
        public delegate void Event(T evt);

        public static event Event OnEvent;

        public static void Notify(T evt) => OnEvent?.Invoke(evt);
    }
}
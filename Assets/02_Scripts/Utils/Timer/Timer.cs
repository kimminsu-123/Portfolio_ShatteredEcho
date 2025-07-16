using NUnit.Framework;

namespace ShEcho.Utils
{
    public abstract class Timer<T>
    {
        public delegate void TimerEvent();

        public T Time { get; private set; }
        public T CurrentTime { get; protected set; }
        public bool IsRunning { get; protected set; }

        public TimerEvent OnStart;
        public TimerEvent OnComplete;
        public TimerEvent OnStop;
        public TimerEvent OnTick;

        protected Timer(T initTime)
        {
            Time = initTime;
            IsRunning = false;
        }

        public void SetTime(T time)
        {
            Time = time;
            CurrentTime = Time;
        }

        public void Start()
        {
            if (!IsRunning)
            {
                OnStart?.Invoke();

                IsRunning = true;

                CurrentTime = Time;
            }
        }

        public void Stop()
        {
            if (IsRunning)
            {
                OnStop?.Invoke();

                IsRunning = false;   
            }
        }

        public abstract void Reset();
        public abstract void Tick(T delta);
    }
}
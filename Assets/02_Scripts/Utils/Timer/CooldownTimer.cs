namespace ShEcho.Utils
{
    public class CooldownTimer : Timer<float>
    {
        public CooldownTimer(float initTime) : base(initTime)
        {
        }

        public override void Reset()
        {
            CurrentTime = 0f;
        }

        public override void Tick(float delta)
        {
            if (!IsRunning) return;
            
            CurrentTime -= delta;
            if (CurrentTime <= 0f)
            {
                CurrentTime = 0f;
                OnComplete?.Invoke();
                IsRunning = false;
            }
            OnTick?.Invoke();
        }
    }
}
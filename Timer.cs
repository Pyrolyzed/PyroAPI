using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI
{
    public class Timer : MonoBehaviour
    {
        public float Duration { get; set; }
        
        public float RemainingSeconds { get; private set; }
        
        public bool DestroyOnFinish { get; set; }
        
        public Action OnTimerEnd { get; set; }

        public Action OnTimerStart { get; set; }

        public Action<float> OnTimerTick { get; set; }

        public bool Active { get; set; }
        
        public Timer Setup(float duration, Action onTimerEnd, bool destroyFinish = false)
        {
            Duration = duration;
            RemainingSeconds = duration;
            OnTimerEnd = onTimerEnd;
            DestroyOnFinish = destroyFinish;
            return this;
        }

        public Timer Setup(float duration, Action onTimerStart, Action onTimerEnd, bool destroyFinish = false)
        {
            Duration = duration;
            RemainingSeconds = duration;
            OnTimerStart = onTimerStart;
            OnTimerEnd = onTimerEnd;
            DestroyOnFinish = destroyFinish;
            return this;
        }
        
        public Timer Setup(float duration, Action onTimerStart, Action<float> onTimerTick, bool destroyFinish = false)
        {
            Duration = duration;
            RemainingSeconds = duration;
            OnTimerStart = onTimerStart;
            OnTimerTick = onTimerTick;
            DestroyOnFinish = destroyFinish;
            return this;
        }
        public Timer Setup(float duration, Action onTimerStart, Action onTimerEnd, Action<float> onTimerTick, bool destroyFinish = false)
        {
            Duration = duration;
            RemainingSeconds = duration;
            OnTimerStart = onTimerStart;
            OnTimerEnd = onTimerEnd;
            DestroyOnFinish = destroyFinish;
            OnTimerTick = onTimerTick;
            return this;
        }

        public void StartTimer()
        {
            RemainingSeconds = Duration;
            Active = true;
            OnTimerStart?.Invoke();
        }

        public void EndTimer()
        {
            OnTimerEnd?.Invoke();
            Active = false;
            if (DestroyOnFinish)
                Destroy(this);
        }

        private void TickTimer()
        {
            RemainingSeconds -= Time.deltaTime;
            OnTimerTick?.Invoke(RemainingSeconds);
            if (RemainingSeconds <= 0)
                EndTimer();
        }

        private void Update()
        {
            if (Active)
                TickTimer();
        }
    }
}
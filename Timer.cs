using System;
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
        
        public bool Active { get; private set; }
        
        public void Setup(float duration, Action onTimerEnd, bool destroyFinish = false)
        {
            Duration = duration;
            RemainingSeconds = duration;
            OnTimerEnd = onTimerEnd;
            DestroyOnFinish = destroyFinish;
        }

        public void Setup(float duration, Action onTimerStart, Action onTimerEnd, bool destroyFinish = false)
        {
            Duration = duration;
            RemainingSeconds = duration;
            OnTimerStart = onTimerStart;
            OnTimerEnd = onTimerEnd;
            DestroyOnFinish = destroyFinish;
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
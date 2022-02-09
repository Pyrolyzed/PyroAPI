using System;
using UnityEngine;

namespace PyroAPI
{
    public class Cooldown : MonoBehaviour
    {
        // How long the cooldown lasts in seconds
        public float CooldownLength { get; set; }

        // Should we destroy the cooldown component once the cooldown finishes? FALSE by default.
        public bool DestroyOnFinish { get; set; }

        // How many seconds are remaining on the cooldown, private setter.
        public float SecondsRemaining { get; private set; }

        // The action the cooldown performs when it ends
        public Action OnCooldownEnd { get; set; }

        // The action the cooldown performs when it starts
        public Action OnCooldownStart { get; set; }

        // Is the cooldown currently running?
        public bool OnCooldown { get; set; }

        public void Setup(float length, Action onEnd, bool destroyFinish = false)
        {
            CooldownLength = length;
            OnCooldownEnd = onEnd;
            DestroyOnFinish = destroyFinish;
        }

        public void Setup(float length, Action onEnd, Action onStart, bool destroyFinish = false)
        {
            CooldownLength = length;
            OnCooldownEnd = onEnd;
            OnCooldownStart = onStart;
            DestroyOnFinish = destroyFinish;
        }

        public void StartCooldown()
        {
            OnCooldown = true;
            OnCooldownStart?.Invoke();
            SecondsRemaining = CooldownLength;
        }

        public void EndCooldown()
        {
            OnCooldown = false;
            OnCooldownEnd?.Invoke();
            if (DestroyOnFinish)
                Destroy(this);
        }

        private void TickCooldown()
        {
            SecondsRemaining -= Time.deltaTime;
            if (SecondsRemaining <= 0)
                EndCooldown();
        }

        private void Update()
        {
            if (OnCooldown)
                TickCooldown();
        }
    }
}
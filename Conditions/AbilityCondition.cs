using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI.Conditions
{
    public class AbilityCondition : MonoBehaviour
    {
        public bool Allowed { get; set; } = true;

        public Func<RagdollHand, Handle, bool> TryCondition { get; set; }

        public AbilityCondition Setup(Func<RagdollHand, Handle, bool> condition)
        {
            TryCondition = condition;
            return this;
        }

        public AbilityCondition Setup(bool allowed)
        {
            Allowed = allowed;
            TryCondition = (hand, handle) => allowed;
            return this;
        }
    }
}
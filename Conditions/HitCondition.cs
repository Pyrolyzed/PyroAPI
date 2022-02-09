using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI.Conditions
{
    public class HitCondition : MonoBehaviour
    {
        public bool Allowed { get; set; } = true;

        public Func<Creature, CollisionInstance, bool> TryCondition { get; set; }

        public HitCondition Setup(Func<Creature, CollisionInstance, bool> condition)
        {
            TryCondition = condition;
            return this;
        }

        public HitCondition Setup(bool allowed)
        {
            Allowed = allowed;
            TryCondition = (creature, instance) => allowed;
            return this;
        }
    }
}
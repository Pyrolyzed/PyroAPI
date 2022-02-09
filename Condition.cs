using System;
using UnityEngine;

namespace PyroAPI
{
    public class Condition : MonoBehaviour
    {
        // Are we allowed at all to do this?
        public bool Allowed { get; set; } = true;

        // The condition to satisfy
        public Func<bool> TryCondition { get; set; }

        public Condition Setup(Func<bool> condition)
        {
            TryCondition = condition;
            return this;
        }

        public Condition Setup(bool allowed)
        {
            Allowed = allowed;
            TryCondition = () => Allowed;
            return this;
        }
    }
}
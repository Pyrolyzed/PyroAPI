using System;
using UnityEngine;

namespace PyroAPI.Conditions
{
    public class Condition<A> : MonoBehaviour
    {

        public Func<A, bool> TryCondition { get; set; }

        public bool Allowed { get; set; } = true;

        public Condition<A> Setup(Func<A, bool> condition, bool allowed = true)
        {
            TryCondition = condition;
            Allowed = allowed;
            return this;
        }
    }
    public class Condition<A,B> : MonoBehaviour
    {
        public Func<A, B, bool> TryCondition { get; set; }

        public bool Allowed { get; set; } = true;

        public Condition<A,B> Setup(Func<A, B, bool> condition, bool allowed = true)
        {
            TryCondition = condition;
            Allowed = allowed;
            return this;
        }

    }

    public class Condition<A, B, C> : MonoBehaviour
    {
        public Func<A, B, C, bool> TryCondition { get; set; }
        public bool Allowed { get; set; } = true;

        public Condition<A, B, C> Setup(Func<A, B, C, bool> condition, bool allowed = true)
        {
            TryCondition = condition;
            Allowed = allowed;
            return this;
        }
    }
}
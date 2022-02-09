using System;
using UnityEngine;

namespace PyroAPI
{
    public class Condition : MonoBehaviour
    {
        // Are we allowed at all to do this?
        public bool Allowed { get; set; } = true;

        // The condition to satisfy
        public Func<bool> AllowCondition { get; set; }

        public void Setup(Func<bool> condition)
        {
            AllowCondition = condition;
        }

        public void Setup(bool allowed)
        {
            Allowed = allowed;
            AllowCondition = () => Allowed;
        }
    }
}
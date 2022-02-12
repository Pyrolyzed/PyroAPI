using PyroAPI.Conditions;
using System;
using UnityEngine;

namespace PyroAPI.Abilities
{
    public class Ability<A> : MonoBehaviour
    {
        public Action<A> OnAbility { get; set; }
        
        public Condition<A> Condition { get; set; }
    }

    public class Ability<A, B> : MonoBehaviour
    {
        public Action<A, B> OnAbility { get; set; }
        
        public Condition<A, B> Condition { get; set; }
    }

    public class Ability<A, B, C> : MonoBehaviour
    {
        public Action<A, B, C> OnAbility { get; set; }
        
        public Condition<A, B, C> Condition { get; set; }
        
    }
}
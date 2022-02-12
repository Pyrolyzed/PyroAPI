using PyroAPI.Conditions;
using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI.Abilities
{
    public class HitAbility : Ability<Creature, CollisionInstance>
    {
        public Item Item { get; set; }

        /*
         * Create an ALWAYS ACTIVE hit ability
         */
        public void Setup(Action<Creature, CollisionInstance> onHit)
        {
            Setup(onHit, (creature, instance) => true);
        }

        /*
         * Create a hit ability that must satisfy the Condition
         */
        public void Setup(Action<Creature, CollisionInstance> onHit,
            Func<Creature, CollisionInstance, bool> condition)
        {
            if (!gameObject.GetComponent<Item>())
                throw new Exception("No Item on HitAbility gameObject");
            Item = gameObject.GetComponent<Item>();
            OnAbility = onHit;

            Condition = gameObject.AddComponent<Condition<Creature, CollisionInstance>>().Setup(condition);

            CollisionHandler.CollisionEvent onCollision = instance =>
            {
                if (instance.damageStruct.hitRagdollPart?.ragdoll?.creature is Creature creature &&
                    (bool) Item.mainHandler?.creature?.isPlayer &&
                    Condition.TryCondition(creature, instance) &&
                    Condition.Allowed)
                    OnAbility?.Invoke(creature, instance);
            };
            Item.collisionHandlers.ForEach(i => i.OnCollisionStartEvent += onCollision);

        }
    }
}
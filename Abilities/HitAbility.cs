using PyroAPI.Conditions;
using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI.Abilities
{
    public class HitAbility : MonoBehaviour
    {
        public Action<Creature, CollisionInstance> OnHit { get; set; }
        public Item Item { get; set; }
        public HitCondition Condition { get; set; }

        /*
         * Create an ALWAYS ACTIVE hit ability
         */
        public HitAbility Setup(Action<Creature, CollisionInstance> onHit)
        {
            Setup(onHit, (creature, instance) => true);
            return this;
        }

        /*
         * Create a hit ability that must satisfy the Condition
         */
        public HitAbility Setup(Action<Creature, CollisionInstance> onHit,
            Func<Creature, CollisionInstance, bool> condition)
        {
            if (!gameObject.GetComponent<Item>())
                throw new Exception("No Item on HitAbility gameObject");
            Item = gameObject.GetComponent<Item>();
            OnHit = onHit;

            Condition = gameObject.AddComponent<HitCondition>().Setup(condition);

            CollisionHandler.CollisionEvent onCollision = instance =>
            {
                if (instance.damageStruct.hitRagdollPart?.ragdoll?.creature is Creature creature &&
                    Item.handlers.TrueForAll(hand => hand.creature.isPlayer) &&
                    Condition.TryCondition(creature, instance) &&
                    Condition.Allowed)
                    OnHit?.Invoke(creature, instance);
            };
            Item.collisionHandlers.ForEach(i => i.OnCollisionStartEvent += onCollision);

            return this;
        }
    }
}
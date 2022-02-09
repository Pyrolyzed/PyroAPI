using PyroAPI.Conditions;
using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI.Abilities
{
    public class HitAbility : MonoBehaviour
    {
        public Action<Creature, CollisionInstance> OnHit;
        public Item item;
        public HitCondition Condition { get; set; }

        /*
         * Create an ALWAYS ACTIVE hit ability
         */
        public HitAbility Setup(Item _item, Action<Creature, CollisionInstance> onHit)
        {
            Setup(_item, onHit, (creature, instance) => true);
            return this;
        }

        /*
         * Create a hit ability that must satisfy the Condition
         */
        public HitAbility Setup(Item _item, Action<Creature, CollisionInstance> onHit, Func<Creature, CollisionInstance, bool> condition)
        {
            item = _item;
            OnHit = onHit;

            Condition = gameObject.AddComponent<HitCondition>().Setup(condition);

            CollisionHandler.CollisionEvent onCollision = instance =>
            {
                if (instance.damageStruct.hitRagdollPart?.ragdoll?.creature is Creature creature &&
                    item.handlers.TrueForAll(hand => hand.creature.isPlayer) && Condition.TryCondition(creature, instance) &&
                    Condition.Allowed)
                    OnHit?.Invoke(creature, instance);
            };
            item.collisionHandlers.ForEach(i => i.OnCollisionStartEvent += onCollision);

            return this;
        }
    }
}
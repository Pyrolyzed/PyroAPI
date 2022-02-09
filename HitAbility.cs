using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI
{
    public class HitAbility : MonoBehaviour
    {
        public Action<Creature, CollisionInstance> OnHit;
        public Item item;
        public Condition Condition { get; set; }

        /*
         * Create an ALWAYS ACTIVE hit ability
         */
        public HitAbility Setup(Item _item, Action<Creature, CollisionInstance> onHit)
        {
            Setup(_item, onHit, () => true);
            return this;
        }

        /*
         * Create a hit ability that must satisfy the Condition
         */
        public HitAbility Setup(Item _item, Action<Creature, CollisionInstance> onHit, Func<bool> condition)
        {
            item = _item;
            OnHit = onHit;

            Condition = gameObject.AddComponent<Condition>().Setup(condition);

            CollisionHandler.CollisionEvent onCollision = instance =>
            {
                if (instance.damageStruct.hitRagdollPart?.ragdoll?.creature is Creature creature &&
                    item.handlers.TrueForAll(hand => hand.creature.isPlayer) && Condition.TryCondition() &&
                    Condition.Allowed)
                    OnHit?.Invoke(creature, instance);
            };
            item.collisionHandlers.ForEach(i => i.OnCollisionStartEvent += onCollision);

            return this;
        }
    }
}
using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI
{
    public class HitAbility : MonoBehaviour
    {
        public Action<Creature, CollisionInstance> OnHit;
        public Item item;

        public void Setup(Item _item, Action<Creature, CollisionInstance> onHit)
        {
            item = _item;
            OnHit = onHit;
            CollisionHandler.CollisionEvent onCollision = instance =>
            {
                if (instance.damageStruct.hitRagdollPart?.ragdoll?.creature is Creature creature &&
                    item.handlers.TrueForAll(hand => hand.creature.isPlayer))
                    OnHit?.Invoke(creature, instance);
            };
            item.collisionHandlers.ForEach(i => i.OnCollisionStartEvent += onCollision);
        }
    }
}
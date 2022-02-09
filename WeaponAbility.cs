using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI
{
    public class WeaponAbility : MonoBehaviour
    {
        public Action<RagdollHand, Handle> Ability { get; set; }
        public Item Item { get; set; }

        public Interactable.Action Bind { get; set; }

        public Condition Condition { get; set; }


        protected void Setup(Item item, Interactable.Action bind, Action<RagdollHand, Handle> ability, Func<bool> condition)
        {
            Ability = ability;
            Bind = bind;
            Item = item;

            Condition = gameObject.AddComponent<Condition>().Setup(Condition);
            
            item.OnHeldActionEvent += ItemHeldActionEvent;
        }

        private void ItemHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            if (ragdollHand.creature.isPlayer && action == Bind && handle.item.itemId == Item.itemId &&
                Condition.TryCondition() && Condition.Allowed)
                Ability?.Invoke(ragdollHand, handle);
        }
    }
}
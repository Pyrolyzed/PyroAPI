using PyroAPI.Conditions;
using System;
using ThunderRoad;
using UnityEngine;

namespace PyroAPI.Abilities
{
    public class WeaponAbility : MonoBehaviour
    {
        public Action<RagdollHand, Handle> Ability { get; set; }
        public Item Item { get; set; }

        public Interactable.Action Bind { get; set; }

        public AbilityCondition Condition { get; set; }


        public WeaponAbility Setup(Interactable.Action bind, Action<RagdollHand, Handle> ability,
            Func<RagdollHand, Handle, bool> condition)
        {
            Ability = ability;
            Bind = bind;
            if (!gameObject.GetComponent<Item>())
                throw new ApplicationException("WeaponAbility not attached to Item!");
            Item = gameObject.GetComponent<Item>();
            Condition = gameObject.AddComponent<AbilityCondition>().Setup(condition);

            Item.OnHeldActionEvent += ItemHeldActionEvent;

            return this;
        }

        public WeaponAbility Setup(Interactable.Action bind, Action<RagdollHand, Handle> ability)
        {
            Setup(bind, ability, (hand, handle) => true);
            return this;
        }

        private void ItemHeldActionEvent(RagdollHand hand, Handle handle, Interactable.Action action)
        {
            if (hand.creature.isPlayer && action == Bind &&
                Condition.TryCondition(hand, handle) && Condition.Allowed)
                Ability?.Invoke(hand, handle);
        }
    }
}
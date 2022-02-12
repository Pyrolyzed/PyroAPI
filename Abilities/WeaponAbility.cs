using PyroAPI.Conditions;
using System;
using ThunderRoad;

namespace PyroAPI.Abilities
{
    public class WeaponAbility : Ability<RagdollHand, Handle>
    {
        public Item Item { get; set; }

        public Interactable.Action Bind { get; set; }

        public void Setup(Interactable.Action bind, Action<RagdollHand, Handle> ability,
            Func<RagdollHand, Handle, bool> condition)
        {
            OnAbility = ability;
            Bind = bind;
            if (!gameObject.GetComponent<Item>())
                throw new ApplicationException("WeaponAbility not attached to Item!");
            Item = gameObject.GetComponent<Item>();
            Condition = gameObject.AddComponent<Condition<RagdollHand, Handle>>().Setup(condition);

            Item.OnHeldActionEvent += ItemHeldActionEvent;
        }

        
        
        public void Setup(Interactable.Action bind, Action<RagdollHand, Handle> ability)
        {
            Setup(bind, ability, (hand, handle) => true);
        }

        private void ItemHeldActionEvent(RagdollHand hand, Handle handle, Interactable.Action action)
        {
            if (hand.creature.isPlayer && action == Bind &&
                Condition.TryCondition(hand, handle) && Condition.Allowed)
                OnAbility?.Invoke(hand, handle);
        }
    }
}
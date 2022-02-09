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

        public AbilityCondition Condition { get; set; }


        protected WeaponAbility Setup(Interactable.Action bind, Action<RagdollHand, Handle> ability, Func<RagdollHand, Handle, bool> condition)
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

        protected WeaponAbility Setup(Interactable.Action bind, Action<RagdollHand, Handle> ability)
        {
            Setup(bind, ability, (hand, handle) => true);
            return this;
        }

        private void ItemHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            if (ragdollHand.creature.isPlayer && action == Bind && handle.item.itemId == Item.itemId &&
                Condition.TryCondition(ragdollHand, handle) && Condition.Allowed)
                Ability?.Invoke(ragdollHand, handle);
        }
    }

    public class ExampleAbility : WeaponAbility
    {
        private Item _item;
        private Interactable.Action _bind;
        
        private void Awake()
        {
            Setup(_bind, Ability, Condition);
        }

        private bool Condition(RagdollHand hand, Handle handle)
        {
            return hand.creature.isPlayer;
        }

        private void Ability(RagdollHand hand, Handle handle)
        {
            hand.creature.Kill();
        }

        public class ExampleItemModule : ItemModule
        {
            public override void OnItemLoaded(Item item)
            {
                item.gameObject.AddComponent<ExampleAbility>();
                base.OnItemLoaded(item);
            }
        }
    }
    
}
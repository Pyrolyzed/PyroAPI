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
        public bool Allowed { get; set; }

        protected void Setup(Item item, Interactable.Action bind, Action<RagdollHand, Handle> ability)
        {
            Ability = ability;
            Bind = bind;
            Item = item;

            item.OnHeldActionEvent += ItemHeldActionEvent;
        }

        private void ItemHeldActionEvent(RagdollHand ragdollHand, Handle handle, Interactable.Action action)
        {
            if (ragdollHand.creature.isPlayer && action == Bind && handle.item.itemId == Item.itemId && Allowed)
                Ability?.Invoke(ragdollHand, handle);
        }
    }
    // Example weapon ability
    public class ExampleWeaponAbility : WeaponAbility
    {
        // The Item that will be provided by the item
        private Item _item;

        private void Awake()
        {
            // Set Item to the Item we are attached to.
            _item = GetComponent<Item>();

            // Call the base classes setup with our settings. Creating the ability
            base.Setup(_item, Interactable.Action.AlternateUseStart,
                (hand, handle) => { Debug.Log("Test Weapon Ability:\n" + hand + "\n" + handle); });

            // Alternate way to setup
            base.Setup(_item, Interactable.Action.AlternateUseStart, OnAbility);
        }

        // Used in our base.Setup() line (line 48)
        private void OnAbility(RagdollHand hand, Handle handle)
        {
            Debug.Log("Test Weapon Ability:\n" + hand + "\n" + handle);
        }
    }
    // Example item module
    public class ExampleItemModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            item.gameObject.AddComponent<ExampleWeaponAbility>();
            base.OnItemLoaded(item);
        }
    }
}
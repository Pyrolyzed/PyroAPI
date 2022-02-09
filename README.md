# PyroAPI
A API that adds some utility functions/classes for Blade &amp; Sorcery modding, will continue to be updated. Feel free to use in any projects!


# Features List:

## Weapon Abilities
This is a class that inherits from `MonoBehaviour`, and can be used to create weapon abilities activated by pressing a button on your controller.
To make one, you inherit from this class, you will still also inherit from `MonoBehaviour`, since the base class does.
You then call `Setup(bind, ability, condition);`
Or, alternatively, call `Setup(bind, ability);` for an ability that you can always use.

Example:
```cs
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
```

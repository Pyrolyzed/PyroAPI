# PyroAPI
A API that adds some utility functions/classes for Blade &amp; Sorcery modding, will continue to be updated. Feel free to use in any projects!


# Features List:

## Weapon Abilities
This is a class that inherits from `MonoBehaviour`, and can be used to create weapon abilities activated by pressing a button on your controller.
To make one, Simply add it as a component onto a GameObject with an Item component, and call the `Setup()` method.

## Hit Abilities
This is another class that inherits from `MonoBehaviour`, and can be used to create abilities that trigger whenever a weapon hits something.
**Note: This class ONLY works when the weapon is held by the player, and the weapon hits a Creature**

To make one, add it as a component onto a GameObject with an Item component, and call the `Setup()` method.


# Example Weapon
```cs
public class AwesomeWeapon : MonoBehaviour
    {
        private HitAbility _hitAbility;
        private WeaponAbility _weaponAbility;

        public void Awake()
        {
            _hitAbility = gameObject.AddComponent<HitAbility>().Setup(OnHit, HitCondition);
            _weaponAbility = gameObject.AddComponent<WeaponAbility>().Setup(Interactable.Action.AlternateUseStart,
                Ability, AbilityCondition);
        }

        private bool AbilityCondition(RagdollHand hand, Handle handle)
        {
            // Can be any statement that returns a bool
            
            return true;
        }

        private void Ability(RagdollHand hand, Handle handle)
        {
            AreaUtils.GetCreaturesInRadius(hand.creature.transform.position, 10f).ForEach(creature => creature.Kill());
        }

        private bool HitCondition(Creature creature, CollisionInstance instance)
        {
            return true;
        }

        private void OnHit(Creature creature, CollisionInstance instance)
        {
            Debug.Log("STRIKE!");
        }
    }

    public class WeaponModule : ItemModule
    {
        public override void OnItemLoaded(Item item)
        {
            item.gameObject.AddComponent<AwesomeWeapon>();
            base.OnItemLoaded(item);
        }
    }
```

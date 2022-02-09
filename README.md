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
public class ExampleAbility: WeaponAbility {
  private void Awake() => Setup(Interactable.Action.AlternateUseStart, Ability, Condition);

  private bool Condition(RagdollHand hand, Handle handle) => hand.creature.isPlayer;

  private void Ability(RagdollHand hand, Handle handle) => hand.creature.Kill();

  public class ExampleItemModule: ItemModule {
    public override void OnItemLoaded(Item item) {
      item.gameObject.AddComponent < ExampleAbility > ();
      base.OnItemLoaded(item);
    }
  }
}
```

## Hit Abilities
This is another class that inherits from `MonoBehaviour`, and can be used to create abilities that trigger whenever a weapon hits something.
**Note: This class ONLY works when the weapon is held by the player, and the weapon hits a Creature**

To make one, inherit from this class, and call `Setup(ability, condition)` inside of `Awake()`, or call `Setup(ability)` for a effect that always happens on hit.

Example:
```cs
public class ExampleHitAbility: HitAbility {
  private void Awake() => Setup(OnHit, Condition);

  private void OnHit(Creature creature, CollisionInstance instance) => instance.damageStruct.damage *= 100;

  private bool Condition(Creature creature, CollisionInstance collisionInstance) => creature.isPlayer;

  public class ExampleHitItemModule: ItemModule {
    public override void OnItemLoaded(Item item) {
      item.gameObject.AddComponent < ExampleHitAbility > ();
      base.OnItemLoaded(item);
    }
  }
}
```

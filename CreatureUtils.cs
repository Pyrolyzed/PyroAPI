using ThunderRoad;

namespace PyroAPI
{
    public class CreatureUtils
    {
        public static void SetGod(Creature creature, bool god = true)
        {
            creature.mana.maxMana = float.PositiveInfinity;
            creature.mana.currentMana = float.PositiveInfinity;
            creature.mana.maxFocus = float.PositiveInfinity;
            creature.mana.currentFocus = float.PositiveInfinity;
            creature.maxHealth = float.PositiveInfinity;
            creature.currentHealth = float.PositiveInfinity;
        }

        public static void SetLimbPower(Creature creature, float power)
        {
            creature.handLeft.bodyDamager.data.addForce = power;
            creature.handRight.bodyDamager.data.addForce = power;
            creature.handLeft.bodyDamager.data.addForceDuration = power;
            creature.handRight.bodyDamager.data.addForceDuration = power;
            creature.footLeft.bodyDamager.data.addForce = power;
            creature.footRight.bodyDamager.data.addForce = power;
            creature.footLeft.bodyDamager.data.addForceDuration = power;
            creature.footRight.bodyDamager.data.addForceDuration = power;
            creature.handLeft.data.damageMultiplier = power;
            creature.handRight.data.damageMultiplier = power;
            creature.footLeft.data.damageMultiplier = power;
            creature.footRight.data.damageMultiplier = power;
        }

        public static void SetMoveSpeed(Creature creature, float speed)
        {
            creature.locomotion.speed = speed;
            creature.locomotion.airSpeed = speed;
            creature.locomotion.runSpeedMultiplier = 1 + speed / 10;
            creature.locomotion.backwardspeedMultiplier = 1 + speed / 10;
        }

        public static void SetJumpForce(Creature creature, float force)
        {
            creature.locomotion.jumpGroundForce = force;
            creature.locomotion.jumpClimbVerticalMultiplier = force / 10;
            creature.locomotion.jumpClimbHorizontalMultiplier = force / 10;
        }
    }
}
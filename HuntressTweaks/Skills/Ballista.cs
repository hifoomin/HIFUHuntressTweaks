using RoR2.Skills;
using UnityEngine.AddressableAssets;

namespace HIFUHuntressTweaks.Skills
{
    public class Ballista : TweakBase
    {
        public static float damage;
        public static float procCoefficient;
        public static float cooldown;
        public static int boltCount;

        public override string Name => ": Special :: Ballista";

        public override string SkillToken => "special_alt1";

        public override string DescText => "<style=cIsUtility>Teleport</style> backwards into the sky. Fire up to <style=cIsDamage>" + boltCount + "</style> energy bolts, dealing <style=cIsDamage>" + boltCount + "x" + d(damage) + " damage</style>.";

        public override void Init()
        {
            damage = ConfigOption(7.5f, "Damage", "Decimal. Vanilla is 9");
            procCoefficient = ConfigOption(1f, "Proc Coefficient", "Vanilla is 1");
            cooldown = ConfigOption(10f, "Cooldown", "Vanilla is 12");
            boltCount = ConfigOption(4, "Bolt Count", "Vanilla is 3");
            base.Init();
        }

        public override void Hooks()
        {
            Changes();
            On.EntityStates.GenericBulletBaseState.OnEnter += GenericBulletBaseState_OnEnter;
        }

        private void GenericBulletBaseState_OnEnter(On.EntityStates.GenericBulletBaseState.orig_OnEnter orig, EntityStates.GenericBulletBaseState self)
        {
            if (self is EntityStates.Huntress.Weapon.FireArrowSnipe)
            {
                self.damageCoefficient = damage;
                self.procCoefficient = procCoefficient;
            }
            orig(self);
        }

        public static void Changes()
        {
            var ballistaAim = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Huntress/AimArrowSnipe.asset").WaitForCompletion();
            var ballistaFire = Addressables.LoadAssetAsync<SkillDef>("RoR2/Base/Huntress/FireArrowSnipe.asset").WaitForCompletion();
            ballistaAim.baseRechargeInterval = cooldown;
            ballistaFire.baseMaxStock = boltCount;
        }
    }
}
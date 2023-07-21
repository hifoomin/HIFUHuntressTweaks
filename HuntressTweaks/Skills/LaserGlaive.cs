using RoR2;
using RoR2.Skills;
using System;

namespace HIFUHuntressTweaks.Skills
{
    public class LaserGlaive : TweakBase
    {
        public static float damage;
        public static float procCoefficient;
        public static float bounceDamage;
        public static int bounceCount;
        public static float cooldown;
        public static float antigrav;
        public static float duration;
        public static bool agile;

        public override string Name => ": Secondary : Laser Glaive";

        public override string SkillToken => "secondary";

        public override string DescText => (agile ? "<style=cIsUtility>Agile</style>. " : "") + "Throw a seeking glaive that bounces up to <style=cIsDamage>" + bounceCount + "</style> times for <style=cIsDamage>" + d(damage) + " damage</style>. Damage increases by <style=cIsDamage>" + Math.Round((bounceDamage - 1f) * 100f, 1) + "%</style> per bounce.";

        public override void Init()
        {
            damage = ConfigOption(3.5f, "Damage", "Decimal. Vanilla is 2.5");
            procCoefficient = ConfigOption(1.0f, "Proc Coefficient", "Vanilla is 0.8");
            bounceDamage = ConfigOption(1.025f, "Damage Multiplier Per Bounce", "Decimal. Vanilla is 1.1");
            bounceCount = ConfigOption(6, "Bounces", "Vanilla is 6");
            cooldown = ConfigOption(6f, "Cooldown", "Vanilla is 7");
            antigrav = ConfigOption(40f, "Jump Boost", "Vanilla is 30");
            duration = ConfigOption(0.8f, "Animation Speed", "Vanilla is 1.1");
            agile = ConfigOption(true, "Enable Agile?", "Vanilla is false");
            base.Init();
        }

        public override void Hooks()
        {
            Changes();
            On.EntityStates.Huntress.HuntressWeapon.ThrowGlaive.OnEnter += ThrowGlaive_OnEnter;
        }

        private void ThrowGlaive_OnEnter(On.EntityStates.Huntress.HuntressWeapon.ThrowGlaive.orig_OnEnter orig, EntityStates.Huntress.HuntressWeapon.ThrowGlaive self)
        {
            EntityStates.Huntress.HuntressWeapon.ThrowGlaive.damageCoefficient = damage;
            EntityStates.Huntress.HuntressWeapon.ThrowGlaive.damageCoefficientPerBounce = bounceDamage;
            EntityStates.Huntress.HuntressWeapon.ThrowGlaive.maxBounceCount = bounceCount;
            EntityStates.Huntress.HuntressWeapon.ThrowGlaive.glaiveProcCoefficient = procCoefficient;
            EntityStates.Huntress.HuntressWeapon.ThrowGlaive.antigravityStrength = antigrav / duration;
            EntityStates.Huntress.HuntressWeapon.ThrowGlaive.baseDuration = duration;
            orig(self);
        }

        public static void Changes()
        {
            var glaive = LegacyResourcesAPI.Load<SkillDef>("skilldefs/huntressbody/HuntressBodyGlaive");
            glaive.baseRechargeInterval = cooldown;
            if (agile)
            {
                glaive.cancelSprintingOnActivation = false;
                glaive.keywordTokens = new string[] { "KEYWORD_AGILE" };
            }
        }
    }
}
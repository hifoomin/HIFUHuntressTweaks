using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;

namespace HIFUHuntressTweaks.Skills
{
    public class ArrowRain : TweakBase
    {
        public static float damage;
        public static float procCoefficient;
        public static float cooldown;
        public static float sizeMultiplier;
        public static float heightMultiplier;

        public override string Name => ": Special : Arrow Rain";

        public override string SkillToken => "special";

        public override string DescText => "<style=cIsUtility>Teleport</style> into the sky. Target an area to rain arrows, <style=cIsUtility>slowing</style> all enemies and dealing <style=cIsDamage>" + (damage * 300f) + "% damage per second</style>.";

        public override void Init()
        {
            damage = ConfigOption(1.4f, "Damage", "Decimal. Vanilla is 1.1");
            procCoefficient = ConfigOption(0.7f, "Proc Coefficient", "Vanilla is 0.2");
            cooldown = ConfigOption(9f, "Cooldown", "Vanilla is 12");
            sizeMultiplier = ConfigOption(1.3f, "Hitbox Size Multiplier", "Vanilla is 1");
            heightMultiplier = ConfigOption(3f, "Hitbox Height Multiplier", "Vanilla is 1");
            base.Init();
        }

        public override void Hooks()
        {
            Changes();
            On.EntityStates.Huntress.ArrowRain.OnEnter += ArrowRain_OnEnter;
        }

        private void ArrowRain_OnEnter(On.EntityStates.Huntress.ArrowRain.orig_OnEnter orig, EntityStates.Huntress.ArrowRain self)
        {
            EntityStates.Huntress.ArrowRain.damageCoefficient = damage / 0.5f;
            orig(self);
        }

        public static void Changes()
        {
            var arrowRain = LegacyResourcesAPI.Load<GameObject>("prefabs/projectiles/HuntressArrowRain");
            var projectileDotZone = arrowRain.GetComponent<ProjectileDotZone>();
            projectileDotZone.overlapProcCoefficient = procCoefficient;

            arrowRain.transform.localScale = new Vector3(15f * sizeMultiplier, 15f * heightMultiplier, 15f * sizeMultiplier);
            /*
            var hitbox = arrowRain.GetComponentInChildren<HitBox>();
            hitbox.transform.localScale = new Vector3(hitbox.transform.localScale.x * sizeMultiplier, hitbox.transform.localScale.y * heightMultiplier, hitbox.transform.localScale.z * sizeMultiplier);

            var radiusIndicator = arrowRain.transform.GetChild(0).GetChild(0);
            radiusIndicator.localScale = new Vector3(0.2692265f * sizeMultiplier, 0.2692266f * heightMultiplier, 1.080555f * sizeMultiplier);
            */
            LegacyResourcesAPI.Load<SkillDef>("skilldefs/huntressbody/HuntressBodyArrowRain").baseRechargeInterval = cooldown;
        }
    }
}
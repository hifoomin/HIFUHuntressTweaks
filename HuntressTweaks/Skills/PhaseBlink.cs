using RoR2;
using RoR2.Skills;

namespace HIFUHuntressTweaks.Skills
{
    public class PhaseBlink : TweakBase
    {
        public static float cooldown;
        public static float speedCoefficient;
        public static int charges;
        public static int chargesToRecharge;
        public static bool resetChargeCooldown;

        public override string Name => ": Utility :: Phase Blink";

        public override string SkillToken => "utility_alt1";

        public override string DescText => "<style=cIsUtility>Agile</style>. <style=cIsUtility>Disappear</style> and <style=cIsUtility>teleport</style> a short distance. Can store up to <style=cIsUtility>" + charges + "</style> charges.";

        public override void Init()
        {
            cooldown = ConfigOption(7f, "Cooldown", "Vanilla is 2");
            speedCoefficient = ConfigOption(15f, "Speed Coefficient", "Vanilla is 15");
            charges = ConfigOption(3, "Charge Count", "Vanilla is 3");
            chargesToRecharge = ConfigOption(3, "Charges to Recharge", "Vanilla is 1");
            resetChargeCooldown = ConfigOption(false, "Reset Charge Cooldown on use?", "Vanilla is false");
            base.Init();
        }

        public override void Hooks()
        {
            Changes();
            On.EntityStates.Huntress.BlinkState.OnEnter += BlinkState_OnEnter;
        }

        private void BlinkState_OnEnter(On.EntityStates.Huntress.BlinkState.orig_OnEnter orig, EntityStates.Huntress.BlinkState self)
        {
            if (self is EntityStates.Huntress.MiniBlinkState)
            {
                self.speedCoefficient = speedCoefficient;
            }
            orig(self);
        }

        public static void Changes()
        {
            var huntressMiniBodyBlink = LegacyResourcesAPI.Load<SkillDef>("skilldefs/huntressbody/HuntressBodyMiniBlink");
            huntressMiniBodyBlink.baseRechargeInterval = cooldown;
            huntressMiniBodyBlink.baseMaxStock = charges;
            huntressMiniBodyBlink.rechargeStock = chargesToRecharge;
            huntressMiniBodyBlink.resetCooldownTimerOnUse = resetChargeCooldown;
        }
    }
}
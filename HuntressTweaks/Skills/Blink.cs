using RoR2;
using RoR2.Skills;

namespace HIFUHuntressTweaks.Skills
{
    public class Blink : TweakBase
    {
        public static float cooldown;
        public static float speedCoefficient;

        public override string Name => ": Utility : Blink";

        public override string SkillToken => "utility";

        public override string DescText => "<style=cIsUtility>Agile</style>. <style=cIsUtility>Disappear</style> and <style=cIsUtility>teleport</style> forward.";

        public override void Init()
        {
            cooldown = ConfigOption(6.5f, "Cooldown", "Vanilla is 7");
            speedCoefficient = ConfigOption(15f, "Speed Coefficient", "Vanilla is 14");
            base.Init();
        }

        public override void Hooks()
        {
            Changes();
            On.EntityStates.Huntress.BlinkState.OnEnter += BlinkState_OnEnter;
        }

        private void BlinkState_OnEnter(On.EntityStates.Huntress.BlinkState.orig_OnEnter orig, EntityStates.Huntress.BlinkState self)
        {
            if (self is EntityStates.Huntress.BlinkState)
            {
                self.speedCoefficient = speedCoefficient;
            }
            orig(self);
        }

        public static void Changes()
        {
            var blink = LegacyResourcesAPI.Load<SkillDef>("skilldefs/huntressbody/HuntressBodyBlink");
            blink.baseRechargeInterval = cooldown;
            blink.keywordTokens = new string[] { "KEYWORD_AGILE" };
        }
    }
}
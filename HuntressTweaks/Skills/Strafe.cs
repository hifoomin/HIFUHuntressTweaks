namespace HIFUHuntressTweaks.Skills
{
    public class Strafe : TweakBase
    {
        public static float damage;
        public static float procCoefficient;
        public static float fireRate;

        public override string Name => ": Primary : Strafe";

        public override string SkillToken => "primary";

        public override string DescText => "<style=cIsUtility>Agile</style>. Quickly fire a seeking arrow for <style=cIsDamage>" + d(damage) + " damage</style>.";

        public override void Init()
        {
            damage = ConfigOption(1.8f, "Damage", "Decimal. Vanilla is 1.5");
            procCoefficient = ConfigOption(1.0f, "Proc Coefficient", "Vanilla is 1");
            fireRate = ConfigOption(1f / 3f, "Duration", "Vanilla is 0.5");
            base.Init();
        }

        public override void Hooks()
        {
            On.EntityStates.Huntress.HuntressWeapon.FireSeekingArrow.OnEnter += FireSeekingArrow_OnEnter;
        }

        private void FireSeekingArrow_OnEnter(On.EntityStates.Huntress.HuntressWeapon.FireSeekingArrow.orig_OnEnter orig, EntityStates.Huntress.HuntressWeapon.FireSeekingArrow self)
        {
            if (self is EntityStates.Huntress.HuntressWeapon.FireSeekingArrow)
            {
                self.orbDamageCoefficient = damage;
                self.orbProcCoefficient = procCoefficient;
                self.baseDuration = fireRate;
            }
            orig(self);
        }
    }
}
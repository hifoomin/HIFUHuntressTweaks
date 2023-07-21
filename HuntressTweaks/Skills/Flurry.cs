namespace HIFUHuntressTweaks.Skills
{
    public class Flurry : TweakBase
    {
        public static float damage;
        public static float procCoefficient;
        public static float fireRate;
        public static int minArrows;
        public static int maxArrows;

        public override string Name => ": Primary :: Flurry";

        public override string SkillToken => "primary_alt";

        public override string DescText => "<style=cIsUtility>Agile</style>. Draw back a volley of <style=cIsDamage>" + minArrows + "</style> seeking arrows for <style=cIsDamage>" + minArrows + "x" + d(damage) + " damage</style>. Critical Strikes fire <style=cIsDamage>" + maxArrows + "</style> arrows.";

        public override void Init()
        {
            damage = ConfigOption(1.1f, "Damage", "Decimal. Vanilla is 1");
            procCoefficient = ConfigOption(1.0f, "Proc Coefficient", "Vanilla is 0.7");
            fireRate = ConfigOption(1f, "Fire Rate", "Vanilla is 1.3");
            minArrows = ConfigOption(3, "Minimum Arrows", "Vanilla is 3");
            maxArrows = ConfigOption(6, "Maximum Arrows", "Vanilla is 6");
            base.Init();
        }

        public override void Hooks()
        {
            On.EntityStates.Huntress.HuntressWeapon.FireSeekingArrow.OnEnter += FireSeekingArrow_OnEnter;
            On.EntityStates.Huntress.HuntressWeapon.FireFlurrySeekingArrow.OnEnter += FireFlurrySeekingArrow_OnEnter;
        }

        private void FireFlurrySeekingArrow_OnEnter(On.EntityStates.Huntress.HuntressWeapon.FireFlurrySeekingArrow.orig_OnEnter orig, EntityStates.Huntress.HuntressWeapon.FireFlurrySeekingArrow self)
        {
            EntityStates.Huntress.HuntressWeapon.FireFlurrySeekingArrow.critMaxArrowCount = maxArrows;
            orig(self);
        }

        private void FireSeekingArrow_OnEnter(On.EntityStates.Huntress.HuntressWeapon.FireSeekingArrow.orig_OnEnter orig, EntityStates.Huntress.HuntressWeapon.FireSeekingArrow self)
        {
            if (self is EntityStates.Huntress.HuntressWeapon.FireFlurrySeekingArrow)
            {
                self.orbDamageCoefficient = damage;
                self.orbProcCoefficient = procCoefficient;
                self.maxArrowCount = minArrows;
                self.baseDuration = fireRate;
            }
            orig(self);
        }
    }
}
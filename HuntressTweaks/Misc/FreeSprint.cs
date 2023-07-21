using RoR2;
using UnityEngine;

namespace HIFUHuntressTweaks.Misc
{
    public class FreeSprint : MiscBase
    {
        public override string Name => ":: Misc : 360 Degree Sprint";

        public override void Init()
        {
            base.Init();
        }

        public override void Hooks()
        {
            Changes();
        }

        public static void Changes()
        {
            var huntressBody = LegacyResourcesAPI.Load<GameObject>("prefabs/characterbodies/HuntressBody").GetComponent<CharacterBody>();
            huntressBody.bodyFlags |= CharacterBody.BodyFlags.SprintAnyDirection;
        }
    }
}
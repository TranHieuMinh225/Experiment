using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class ArmsOfSwiftness
        : ExperimentUpgrade
    {
        public override int Path => MIDDLE;
        public override int Tier => 1;
        public override int Cost => 700;

        public override string DisplayName =>
            "Arms of Swiftness";

        public override string Description =>
            "Capable of swinging his arms faster than the speed of sound.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class EagleEyes
        : ExperimentUpgrade
    {
        public override int Path => BOTTOM;
        public override int Tier => 1;
        public override int Cost => 450;

        public override string DisplayName =>
            "Eagle Eyes";

        public override string Description =>
            "The Experiment develops eyes capable of rivaling those of an eagle.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
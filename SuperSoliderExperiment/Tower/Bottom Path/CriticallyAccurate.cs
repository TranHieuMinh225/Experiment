using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class CriticallyAccurate
        : ExperimentUpgrade
    {
        public override int Path => BOTTOM;
        public override int Tier => 4;
        public override int Cost => 28000;

        public override string DisplayName =>
            "Critically Accurate";

        public override string Description =>
            "Attacks so accurate, they always strike a critical spot.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
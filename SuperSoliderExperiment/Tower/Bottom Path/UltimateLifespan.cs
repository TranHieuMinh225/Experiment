using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class UltimateLifespan
        : ExperimentUpgrade
    {
        public override int Path => BOTTOM;
        public override int Tier => 3;
        public override int Cost => 3200;

        public override string DisplayName =>
            "Ultimate Lifespan";

        public override string Description =>
            "Attacks have their lifespan reinforced by the Experiment's power.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
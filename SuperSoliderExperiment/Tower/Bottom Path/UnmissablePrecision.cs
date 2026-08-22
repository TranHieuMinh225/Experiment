using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class UnmissablePrecision
        : ExperimentUpgrade
    {
        public override int Path => BOTTOM;
        public override int Tier => 5;

        public override int Cost => 95000;

        public override string DisplayName =>
            "Unmissable Precision";

        public override string Description =>
            "Precision Unknown.";

        public override void ApplyUpgrade(
            TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(
                towerModel
            );
        }
    }
}
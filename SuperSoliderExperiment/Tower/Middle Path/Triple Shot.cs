using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class TripleShot
        : ExperimentUpgrade
    {
        public override int Path => MIDDLE;
        public override int Tier => 2;
        public override int Cost => 1100;

        public override string DisplayName =>
            "Triple Shot";

        public override string Description =>
            "Optimizes the Monkey genes, allowing him to hold three darts with no struggle.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
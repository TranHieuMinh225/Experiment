using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class ApexPlasmaMaster
        : ExperimentUpgrade
    {
        public override int Path => TOP;
        public override int Tier => 4;
        public override int Cost => 32000;

        public override string DisplayName =>
            "Apex Plasma Master";

        public override string Description =>
            "Power taken from the Paragon, now manifested by the Experiment.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
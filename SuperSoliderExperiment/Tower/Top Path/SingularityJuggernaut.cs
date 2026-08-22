using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class SingularityJuggernaut
        : ExperimentUpgrade
    {
        public override int Path => TOP;
        public override int Tier => 5;

        public override int Cost => 120000;

        public override string DisplayName =>
            "Singularity Juggernaut";

        public override string Description =>
            "Calamity has come through.";

        public override void ApplyUpgrade(
            TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(
                towerModel
            );
        }
    }
}
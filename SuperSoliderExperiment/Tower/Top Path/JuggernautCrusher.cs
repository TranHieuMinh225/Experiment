using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class JuggernautCrusher
        : ExperimentUpgrade
    {
        public override int Path => TOP;
        public override int Tier => 2;
        public override int Cost => 1200;

        public override string DisplayName =>
            "Juggernaut Crusher";

        public override string Description =>
            "Manifests Juggernaut Crushing Balls, capable of destroying any enemy type.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
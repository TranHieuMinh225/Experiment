using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class GodOfTheArmies
        : ExperimentUpgrade
    {
        public override int Path => MIDDLE;

        public override int Tier => 5;

        public override int Cost => 180000;

        public override string DisplayName =>
            "God of the Armies";

        public override string Description =>
            "Wielding the power of Gods.";

        public override void ApplyUpgrade(
            TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
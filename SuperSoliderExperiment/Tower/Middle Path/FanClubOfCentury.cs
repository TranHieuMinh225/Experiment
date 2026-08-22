using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class FanClubOfCentury
        : ExperimentUpgrade
    {
        public override int Path => MIDDLE;
        public override int Tier => 3;
        public override int Cost => 8500;

        public override string DisplayName =>
            "Fan Club of Century";

        public override string Description =>
            "Breaks all limits of a Fan Club, capable of transforming any number of other Experiments.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class FanClubOfTheMillennium
        : ExperimentUpgrade
    {
        public override int Path => MIDDLE;
        public override int Tier => 4;
        public override int Cost => 45000;

        public override string DisplayName =>
            "Fan Club of the Millennium";

        public override string Description =>
            "An infinite stream of power runs through all other Experiments.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
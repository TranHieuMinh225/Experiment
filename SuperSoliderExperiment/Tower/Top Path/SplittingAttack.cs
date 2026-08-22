using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class SplittingAttack
        : ExperimentUpgrade
    {
        public override int Path => TOP;
        public override int Tier => 3;
        public override int Cost => 4800;

        public override string DisplayName =>
            "Splitting Attack";

        public override string Description =>
            "The power of just one ball isn't enough.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
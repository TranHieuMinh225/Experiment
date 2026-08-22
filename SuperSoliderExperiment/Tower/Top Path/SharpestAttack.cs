using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class SharpestAttack
        : ExperimentUpgrade
    {
        public override int Path => TOP;
        public override int Tier => 1;
        public override int Cost => 650;

        public override string DisplayName =>
            "Sharpest Attack";

        public override string Description =>
            "Capable of manifesting Experimental Darts, created to pierce an unreasonable amount of enemies.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
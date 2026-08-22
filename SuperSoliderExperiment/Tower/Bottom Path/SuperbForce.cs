using BTD_Mod_Helper.Api.Towers;
using Il2CppAssets.Scripts.Models.Towers;

namespace SuperSoldierExperiment
{
    public class SuperbForce
        : ExperimentUpgrade 
    {
        public override int Path => BOTTOM;
        public override int Tier => 2;
        public override int Cost => 900;

        public override string DisplayName =>
            "Superb Force";

        public override string Description =>
            "Arm strength capable of throwing projectiles with fifteen times the force.";

        public override void ApplyUpgrade(TowerModel towerModel)
        {
            SuperSoldierBuilder.Rebuild(towerModel);
        }
    }
}
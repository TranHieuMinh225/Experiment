using BTD_Mod_Helper.Api.Towers;

using Il2CppAssets.Scripts.Models.TowerSets;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Unity;

using Il2CppNinjaKiwi.Common.ResourceUtils;

namespace SuperSoldierExperiment
{
    public class SuperSoldierExperimentTower : ModTower
    {
        public override TowerSet TowerSet =>
            TowerSet.Military;

        public override string BaseTower =>
            TowerType.DartMonkey;

        public override int Cost =>
            500;

        public override string DisplayName =>
            "Experiment";

        public override string Description =>
            "Somewhere inside an underground lab, these were created. " +
            "They should be normal, until they aren't. Created to exceed " +
            "all monkey limit, their power wreck the battlefield.";

        // ========================================================
        // PARAGON
        //
        // Start from the full 5-5-5 Experiment.
        // ========================================================

        public override ParagonMode ParagonMode =>
            ParagonMode.Base555;


        public override SpriteReference IconReference =>
            Game.instance.model
                .GetTowerFromId(TowerType.DartMonkey)
                .icon;

        public override SpriteReference PortraitReference =>
            Game.instance.model
                .GetTowerFromId(TowerType.DartMonkey)
                .portrait;


        public override void ModifyBaseTowerModel(
            TowerModel towerModel)
        {
            // Base remains Dart Monkey.
        }
    }
}
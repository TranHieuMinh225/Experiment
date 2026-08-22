using System.Linq;

using BTD_Mod_Helper.Api.Towers;

using Il2CppAssets.Scripts.Models.Towers.Upgrades;
using Il2CppAssets.Scripts.Unity;

using Il2CppNinjaKiwi.Common.ResourceUtils;

namespace SuperSoldierExperiment
{
    public abstract class ExperimentUpgrade
        : ModUpgrade<SuperSoldierExperimentTower>
    {
        // ========================================================
        // ICON
        //
        // Get the REAL vanilla UpgradeModel icon.
        // No more TowerModel.icon.
        // No more MONKE.
        // ========================================================

        public override SpriteReference IconReference =>
            ExperimentUpgradeAssets.GetIcon(
                GetType().Name
            );


        // ========================================================
        // PORTRAIT
        //
        // DON'T override PortraitReference here.
        //
        // Let Mod Helper automatically use:
        //
        // ClassName-Portrait.png
        //
        // Example:
        // JuggernautCrusher-Portrait.png
        //
        // This is substantially safer than trying to steal
        // TowerModel.portrait, which gave us Base Dart Monkey.
        // ========================================================
    }


    public static class ExperimentUpgradeAssets
    {
        public static SpriteReference GetIcon(
            string upgradeClass)
        {
            string vanillaUpgrade =
                upgradeClass switch
                {
                    // =============================================
                    // TOP
                    // =============================================

                    nameof(SharpestAttack) =>
                        "Razor Sharp Shots",

                    nameof(JuggernautCrusher) =>
                        "Juggernaut",

                    nameof(SplittingAttack) =>
                        "Ultra-Juggernaut",

                    nameof(ApexPlasmaMaster) =>
                        "DartMonkey Paragon",

                    // Temporary donor icon.
                    nameof(SingularityJuggernaut) =>
                        "Ultra-Juggernaut",


                    // =============================================
                    // MIDDLE
                    // =============================================

                    nameof(ArmsOfSwiftness) =>
                        "Very Quick Shots",

                    nameof(TripleShot) =>
                        "Triple Shot",

                    nameof(FanClubOfCentury) =>
                        "Super Monkey Fan Club",

                    nameof(FanClubOfTheMillennium) =>
                        "Plasma Monkey Fan Club",

                    nameof(GodOfTheArmies) =>
                        "True Sun God",


                    // =============================================
                    // BOTTOM
                    // =============================================

                    nameof(EagleEyes) =>
                        "Enhanced Eyesight",

                    // Your T2 donor was Quick Shots.
                    nameof(SuperbForce) =>
                        "Quick Shots",

                    nameof(UltimateLifespan) =>
                        "Crossbow",

                    nameof(CriticallyAccurate) =>
                        "Sharp Shooter",

                    nameof(UnmissablePrecision) =>
                        "Crossbow Master",


                    // =============================================
                    // PARAGON TEST
                    // =============================================
                    nameof(TheFinalExperiment) =>
                        "MonkeySub Paragon",

                    // =============================================
                    // FALLBACK
                    // =============================================


                    _ =>
                        ""
                };


            var upgrade =
                Game.instance.model.upgrades
                    .FirstOrDefault(
                        x =>
                            x.name == vanillaUpgrade
                    );


            // If we typo a vanilla upgrade name,
            // fail gracefully instead of exploding.
            if (upgrade != null)
            {
                return upgrade.icon;
            }


            // Emergency fallback.
            return Game.instance.model
                .GetTowerFromId("DartMonkey")
                .icon;
        }
    }
}
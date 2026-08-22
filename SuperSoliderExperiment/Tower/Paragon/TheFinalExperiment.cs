using BTD_Mod_Helper.Api.Towers;
using BTD_Mod_Helper.Extensions;

using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Unity;

namespace SuperSoldierExperiment
{
    public class TheFinalExperiment
        : ModParagonUpgrade<SuperSoldierExperimentTower>
    {
        // ========================================================
        // THE FINAL EXPERIMENT
        // ========================================================

        public override int Cost =>
            540000;

        public override string DisplayName =>
            "The Final Experiment";

        public override string Description =>
            "Manifestation Of The Perfect Experiment, Nothing Shall Stand In His Way";


        // ========================================================
        // KEEP ABILITIES
        // ========================================================

        public override bool RemoveAbilities =>
            false;


        // ========================================================
        // BUILD THE BASE PARAGON
        //
        // Base555 already supplies:
        //
        // Singularity Juggernaut
        // God of the Armies
        // Unmissable Precision
        // ========================================================

        public override void ApplyUpgrade(
            TowerModel towerModel)
        {
            // ====================================================
            // MODEL — M.A.D.
            //
            // NOTE:
            // This is still the part that currently gets overridden
            // visually by the Paragon display system.
            //
            // Mechanics are unaffected.
            // ====================================================

            var mad =
                Game.instance.model
                    .GetTowerFromId("DartlingGunner-050");

            if (mad != null)
            {
                towerModel.display =
                    mad.display;
            }


            towerModel.isParagon = true;


            // ====================================================
            // GLOBAL RANGE
            // ====================================================

            towerModel.range =
                9_999_999f;

            var attack =
                towerModel.GetAttackModel();

            if (attack != null)
            {
                attack.range =
                    9_999_999f;
            }


            // ====================================================
            // SINGULARITY
            //
            // T5:
            // 1000 stacks
            // +1% each
            //
            // PARAGON:
            // 3000 stacks
            // +1.5% each
            // ====================================================

            UpgradeSingularityCombo(
                towerModel
            );


            // ====================================================
            // FINAL DAMAGE
            //
            // Bottom path already gives:
            //
            // Superb Force          x15
            // Critically Accurate    x2
            // Unmissable Precision  x10
            //
            // = x300
            //
            // Paragon adds:
            // x7.5
            //
            // = x2250 before Degree scaling.
            // ====================================================

            ApplyFinalDamageMultiplier(
                towerModel
            );


            // ====================================================
            // FINAL HOMELAND
            //
            // D1:
            // x3
            //
            // Duration:
            // 40 seconds
            //
            // Cooldown:
            // 30 seconds
            //
            // Degree changes multiplier later in OnDegreeSet().
            // ====================================================

            AddFinalHomeland(
                towerModel
            );


            // ====================================================
            // MASS OVERCLOCK
            //
            // Permanent / global equivalent of Ultraboost's
            // active 0.6 rate modifier.
            // ====================================================

            AddMassOverclock(
                towerModel
            );
        }


        // ========================================================
        // ========================================================
        //
        // PARAGON DEGREE SCALING
        //
        // This runs AFTER the actual degree is known.
        //
        // ========================================================
        // ========================================================

        public override void OnDegreeSet(
            Tower tower,
            int degree)
        {
            // ====================================================
            // OFFENSIVE DEGREE MULTIPLIER
            //
            // D1   = x1
            // D20  = x1.5
            // D40  = x2
            // D60  = x3
            // D80  = x4
            // D88  = x5
            // D91  = x6
            // D100 = x10
            //
            // Multiplicative because careful balance has left
            // the laboratory.
            // ====================================================

            float degreeMultiplier =
                GetDegreeMultiplier(degree);


            var towerModel =
                tower.towerModel;

            var weapon =
                towerModel.GetWeapon();


            if (weapon != null)
            {
                // ================================================
                // DAMAGE
                // ================================================

                if (weapon.projectile != null)
                {
                    var damage =
                        weapon.projectile
                            .GetDamageModel();

                    if (damage != null)
                    {
                        damage.damage *=
                            degreeMultiplier;
                    }
                }


                // ================================================
                // ATTACK SPEED
                //
                // Smaller rate = faster.
                //
                // x2 attack speed:
                // rate / 2
                //
                // x10 attack speed:
                // rate / 10
                // ================================================

                weapon.rate /=
                    degreeMultiplier;

                weapon.Rate =
                    weapon.rate;
            }


            // ====================================================
            // HOMELAND DEGREE CHECKPOINTS
            // ====================================================

            float homelandMultiplier =
                GetHomelandMultiplier(degree);


            foreach (
                var ability
                in towerModel.GetBehaviors<AbilityModel>())
            {
                var homeland =
                    ability
                        .GetBehavior<CallToArmsModel>();


                // Only modify OUR Homeland.
                //
                // This prevents accidentally modifying another
                // inherited ability containing CallToArmsModel.
                if (
                    homeland != null &&
                    ability.modelName == "FinalHomeland"
                )
                {
                    homeland.multiplier =
                        homelandMultiplier;

                    homeland.lifespan =
                        40f;

                    homeland.Lifespan =
                        40f;

                    ability.cooldown =
                        30f;

                    break;
                }
            }


            base.OnDegreeSet(
                tower,
                degree
            );
        }


        // ========================================================
        // DEGREE MULTIPLIER
        // ========================================================

        private static float GetDegreeMultiplier(
            int degree)
        {
            if (degree >= 100)
                return 10f;

            if (degree >= 91)
                return 6f;

            if (degree >= 88)
                return 5f;

            if (degree >= 80)
                return 4f;

            if (degree >= 60)
                return 3f;

            if (degree >= 40)
                return 2f;

            if (degree >= 20)
                return 1.5f;

            return 1f;
        }


        // ========================================================
        // HOMELAND CHECKPOINTS
        //
        // EXACT VALUES REQUESTED:
        //
        // D1   x3
        // D20  x3.5
        // D40  x3.75
        // D60  x4
        // D80  x4.25
        // D88  x4.5
        // D91  x4.75
        // D100 x5
        // ========================================================

        private static float GetHomelandMultiplier(
            int degree)
        {
            if (degree >= 100)
                return 5f;

            if (degree >= 91)
                return 4.75f;

            if (degree >= 88)
                return 4.5f;

            if (degree >= 80)
                return 4.25f;

            if (degree >= 60)
                return 4f;

            if (degree >= 40)
                return 3.75f;

            if (degree >= 20)
                return 3.5f;

            return 3f;
        }


        // ========================================================
        // ========================================================
        //
        // SINGULARITY
        //
        // ========================================================
        // ========================================================

        private static void UpgradeSingularityCombo(
            TowerModel towerModel)
        {
            var combo =
                towerModel
                    .GetBehavior<DamageBasedAttackSpeedModel>();

            if (combo == null)
            {
                return;
            }


            combo.damageThreshold =
                1f;


            // +1.5% per stack.
            combo.increasePerThreshold =
                0.015f;


            // 3000 STACKS.
            combo.maxStacks =
                3000;


            // Preserve Skywarden-style combo timeout.
            combo.maxTimeInFramesWithoutDamage =
                120;


            combo.damageCap =
                1f;
        }


        // ========================================================
        // PARAGON DAMAGE
        // ========================================================

        private static void ApplyFinalDamageMultiplier(
            TowerModel towerModel)
        {
            var weapon =
                towerModel.GetWeapon();

            if (
                weapon == null ||
                weapon.projectile == null
            )
            {
                return;
            }


            var damage =
                weapon.projectile
                    .GetDamageModel();

            if (damage != null)
            {
                // Paragon-specific:
                //
                // Another x7.5 on top of all Bottom Path
                // multipliers.
                damage.damage *=
                    7.5f;
            }
        }


        // ========================================================
        // ========================================================
        //
        // FINAL HOMELAND
        //
        // ========================================================
        // ========================================================

        private static void AddFinalHomeland(
            TowerModel towerModel)
        {
            var village =
                Game.instance.model
                    .GetTowerFromId("MonkeyVillage-050");

            if (village == null)
            {
                return;
            }


            AbilityModel? homelandSource =
                null;


            foreach (
                var ability
                in village.GetBehaviors<AbilityModel>())
            {
                if (
                    ability
                        .GetBehavior<CallToArmsModel>()
                    != null
                )
                {
                    homelandSource =
                        ability;

                    break;
                }
            }


            if (homelandSource == null)
            {
                return;
            }


            var finalHomeland =
                homelandSource.Duplicate();


            // Unique identity.
            finalHomeland.modelName =
                "FinalHomeland";

            finalHomeland.displayName =
                "Final Homeland";

            finalHomeland.description =
                "All towers fight beyond their limits.";


            var homeland =
                finalHomeland
                    .GetBehavior<CallToArmsModel>();


            if (homeland != null)
            {
                // D1 baseline.
                homeland.multiplier =
                    3f;


                // Global.
                homeland.useRadius =
                    false;


                // 40-second duration.
                homeland.lifespan =
                    40f;

                homeland.Lifespan =
                    40f;
            }


            // 30-second cooldown.
            finalHomeland.cooldown =
                30f;


            towerModel.AddBehavior(
                finalHomeland
            );
        }


        // ========================================================
        // ========================================================
        //
        // MASS OVERCLOCK
        //
        // ========================================================
        // ========================================================

        private static void AddMassOverclock(
            TowerModel towerModel)
        {
            // God of the Armies should already provide a
            // RateSupportModel through its TSG support package.

            var sourceRateSupport =
                towerModel
                    .GetBehavior<RateSupportModel>();


            if (sourceRateSupport == null)
            {
                return;
            }


            var massOverclock =
                sourceRateSupport.Duplicate();


            // ====================================================
            // ULTRABOOST / OVERCLOCK EQUIVALENT
            //
            // Vanilla active:
            // rateModifier = 0.6
            //
            // Our version:
            // global permanent rate support x0.6.
            // ====================================================

            massOverclock.multiplier =
                0.6f;


            // Separate ID means it can coexist with TSG support.
            massOverclock.mutatorId =
                "TheFinalExperiment:MassOverclock";


            massOverclock.isUnique =
                true;


            massOverclock.isGlobal =
                true;

            massOverclock.isCustomRadius =
                false;

            massOverclock.customRadius =
                0f;


            // Also buff The Final Experiment itself.
            massOverclock.appliesToOwningTower =
                true;


            massOverclock.showBuffIcon =
                true;

            massOverclock.buffLocsName =
                "Mass Overclock";

            massOverclock.buffIconName =
                "EngineerMonkeyOverclock";


            towerModel.AddBehavior(
                massOverclock
            );
        }
    }
}
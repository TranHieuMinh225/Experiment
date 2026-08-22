using BTD_Mod_Helper.Extensions;

using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Filters;

using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

using Il2CppAssets.Scripts.Models.Towers.Mutators;

using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Weapons;

using Il2CppAssets.Scripts.Unity;

namespace SuperSoldierExperiment
{
    public static class SuperSoldierBuilder
    {
        public static void Rebuild(TowerModel towerModel)
        {
            int top = towerModel.tiers[0];
            int middle = towerModel.tiers[1];
            int bottom = towerModel.tiers[2];

            var weapon = towerModel.GetWeapon();
            var attack = towerModel.GetAttackModel();

            // ====================================================
            // CLEAN BASE REFERENCES
            //
            // Every rebuild starts from a CLEAN projectile.
            //
            // Otherwise Superb Force starts discovering
            // compound interest again.
            // ====================================================

            var baseDart =
                Game.instance.model
                    .GetTowerFromId("DartMonkey");

            float baseRate =
                baseDart.GetWeapon().rate;

            float baseRange =
                baseDart.range;


            // ====================================================
            // ====================================================
            //
            // TOP PATH
            //
            // 1 — Sharpest Attack
            // 2 — Juggernaut Crusher
            // 3 — Splitting Attack
            // 4 — Apex Plasma Master
            // 5 — Singularity Juggernaut
            //
            // ====================================================
            // ====================================================


            // ====================================================
            // FINAL PROJECTILE FOUNDATION
            // ====================================================

            if (top >= 4)
            {
                // T4/T5:
                // Steal Apex projectile only.
                //
                // NO Apex display.
                // NO D100 display system.
                // We learned.

                var apex =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-Paragon");

                weapon.projectile =
                    apex.GetWeapon()
                        .projectile
                        .Duplicate();
            }
            else if (top >= 3)
            {
                var ultraJug =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-500");

                weapon.projectile =
                    ultraJug.GetWeapon()
                        .projectile
                        .Duplicate();
            }
            else if (top >= 2)
            {
                var juggernaut =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-400");

                weapon.projectile =
                    juggernaut.GetWeapon()
                        .projectile
                        .Duplicate();
            }
            else
            {
                // VERY IMPORTANT:
                //
                // Even 0-x-x / 1-x-x needs a clean projectile
                // every rebuild.

                weapon.projectile =
                    baseDart.GetWeapon()
                        .projectile
                        .Duplicate();
            }


            // ====================================================
            // TOP T1 — SHARPEST ATTACK
            //
            // Infinite pierce.
            // ====================================================

            if (top >= 1)
            {
                weapon.projectile.pierce = 1_000_000f;
                weapon.projectile.maxPierce = 1_000_000f;
            }


            // ====================================================
            // TOP T5 — SINGULARITY JUGGERNAUT
            // ====================================================

            if (top >= 5)
            {
                ApplySingularityJuggernaut(
                    towerModel,
                    weapon
                );
            }


            // ====================================================
            // ====================================================
            //
            // MIDDLE PATH
            //
            // 1 — Arms of Swiftness
            // 2 — Triple Shot
            // 3 — Fan Club of Century
            // 4 — Fan Club of the Millennium
            // 5 — God of the Armies
            //
            // ====================================================
            // ====================================================


            // ====================================================
            // MIDDLE T1 — ARMS OF SWIFTNESS
            //
            // 10x attack speed.
            // ====================================================

            if (middle >= 1)
            {
                weapon.rate =
                    baseRate * 0.10f;
            }


            // ====================================================
            // MIDDLE T2 — TRIPLE SHOT
            // ====================================================

            if (middle >= 2)
            {
                weapon.emission =
                    new ArcEmissionModel(
                        "TripleShotEmission",
                        3,
                        0,
                        30,
                        null,
                        false,
                        false
                    );
            }


            // ====================================================
            // MIDDLE T4 — FAN CLUB OF THE MILLENNIUM
            //
            // PMFC
            // Unlimited transforms
            // Full-map transform range
            // 20-second cooldown
            // ====================================================

            if (middle >= 4)
            {
                var pmfc =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-050");

                var sourceAbility =
                    pmfc.GetBehavior<AbilityModel>();

                if (sourceAbility != null)
                {
                    // Remove the older Fan Club ability first.
                    var existingAbility =
                        towerModel.GetBehavior<AbilityModel>();

                    if (existingAbility != null)
                    {
                        towerModel.RemoveBehavior(
                            existingAbility
                        );
                    }


                    var ability =
                        sourceAbility.Duplicate();

                    var fanClub =
                        ability.GetBehavior<MonkeyFanClubModel>();

                    if (fanClub != null)
                    {
                        fanClub.towerCount =
                            1_000_000;

                        fanClub.range =
                            1_000_000f;
                    }


                    ability.cooldown = 20f;

                    towerModel.AddBehavior(
                        ability
                    );
                }
            }
            else if (middle >= 3)
            {
                // ================================================
                // MIDDLE T3 — FAN CLUB OF CENTURY
                //
                // Unlimited SMFC.
                // ================================================

                var smfc =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-040");

                var sourceAbility =
                    smfc.GetBehavior<AbilityModel>();

                if (sourceAbility != null)
                {
                    var existingAbility =
                        towerModel.GetBehavior<AbilityModel>();

                    if (existingAbility == null)
                    {
                        var ability =
                            sourceAbility.Duplicate();

                        var fanClub =
                            ability.GetBehavior<MonkeyFanClubModel>();

                        if (fanClub != null)
                        {
                            fanClub.towerCount =
                                1_000_000;
                        }

                        towerModel.AddBehavior(
                            ability
                        );
                    }
                }
            }


            // ====================================================
            // MIDDLE T5 — GOD OF THE ARMIES
            // ====================================================

            if (middle >= 5)
            {
                ApplyGodOfTheArmies(
                    towerModel
                );
            }


            // ====================================================
            // ====================================================
            //
            // BOTTOM PATH
            //
            // 1 — Eagle Eyes
            // 2 — Superb Force
            // 3 — After-images
            // 4 — Critically Accurate
            // 5 — Unmissable Precision
            //
            // ====================================================
            // ====================================================


            // ====================================================
            // BOTTOM T1 — EAGLE EYES
            //
            // Huge range + Camo.
            // ====================================================

            if (bottom >= 1)
            {
                towerModel.range =
                    baseRange * 2.5f;

                attack.range =
                    baseRange * 2.5f;


                var projectile =
                    weapon.projectile;


                // ------------------------------------------------
                // DIRECT PROJECTILE FILTER
                // ------------------------------------------------

                if (projectile.filters != null)
                {
                    foreach (var filter in projectile.filters)
                    {
                        var invisible =
                            filter.TryCast<FilterInvisibleModel>();

                        if (invisible != null)
                        {
                            invisible.isActive = false;
                        }
                    }
                }


                // ------------------------------------------------
                // PROJECTILE FILTER MODEL
                // ------------------------------------------------

                var projectileFilter =
                    projectile
                        .GetBehavior<ProjectileFilterModel>();

                if (
                    projectileFilter != null &&
                    projectileFilter.filters != null
                )
                {
                    foreach (
                        var filter
                        in projectileFilter.filters
                    )
                    {
                        var invisible =
                            filter.TryCast<FilterInvisibleModel>();

                        if (invisible != null)
                        {
                            invisible.isActive = false;
                        }
                    }
                }


                // ------------------------------------------------
                // ATTACK FILTER
                // ------------------------------------------------

                var attackFilter =
                    attack.GetBehavior<AttackFilterModel>();

                if (
                    attackFilter != null &&
                    attackFilter.filters != null
                )
                {
                    foreach (
                        var filter
                        in attackFilter.filters
                    )
                    {
                        var invisible =
                            filter.TryCast<FilterInvisibleModel>();

                        if (invisible != null)
                        {
                            invisible.isActive = false;
                        }
                    }
                }
            }


            // ====================================================
            // BOTTOM T2 — SUPERB FORCE
            //
            // 15x damage.
            // ====================================================

            if (bottom >= 2)
            {
                var damage =
                    weapon.projectile
                        .GetDamageModel();

                if (damage != null)
                {
                    damage.damage *= 15f;
                }
            }


            // ====================================================
            // BOTTOM T3 — AFTER-IMAGES
            //
            // OLD:
            // 15x projectile speed.
            //
            // That absolutely murdered Unmissable Precision.
            //
            // NEW:
            // 15x projectile travel lifetime.
            //
            // Homing projectile gets enough time to actually
            // TURN instead of flying into another country.
            // ====================================================

            if (bottom >= 3)
            {
                var travel =
                    weapon.projectile
                        .GetBehavior<TravelStraitModel>();

                if (travel != null)
                {
                    travel.lifespan *= 15f;
                    travel.Lifespan *= 15f;
                }
            }


            // ====================================================
            // BOTTOM T4 — CRITICALLY ACCURATE
            //
            // Every shot = weak-point hit.
            //
            // Another x2 damage.
            //
            // Superb Force:
            //      x15
            //
            // Critically Accurate:
            //      x2
            //
            // Combined:
            //      x30
            // ====================================================

            if (bottom >= 4)
            {
                var damage =
                    weapon.projectile
                        .GetDamageModel();

                if (damage != null)
                {
                    damage.damage *= 2f;
                }
            }


            // ====================================================
            // BOTTOM T5 — UNMISSABLE PRECISION
            // ====================================================

            if (bottom >= 5)
            {
                ApplyUnmissablePrecision(
                    weapon
                );
            }


            // ====================================================
            // DISPLAY
            // ====================================================

            ApplyDisplay(
                towerModel,
                top,
                middle,
                bottom
            );
        }


        // ========================================================
        // ========================================================
        //
        // TOP T5
        //
        // SINGULARITY JUGGERNAUT
        //
        // ========================================================
        // ========================================================

        private static void ApplySingularityJuggernaut(
            TowerModel towerModel,
            WeaponModel weapon)
        {
            var farwind =
                Game.instance.model
                    .GetTowerFromId("Skywarden-500");

            var stormwrath =
                Game.instance.model
                    .GetTowerFromId("Skywarden-050");

            var wintersMercy =
                Game.instance.model
                    .GetTowerFromId("Skywarden-005");


            // ====================================================
            // STORMWRATH COMBO ATTACK
            //
            // Original:
            // 20 stacks
            //
            // Singularity:
            // 1000 stacks
            // +1% attack speed each stack
            // ====================================================

            var sourceCombo =
                stormwrath
                    .GetBehavior<DamageBasedAttackSpeedModel>();

            if (sourceCombo != null)
            {
                var existingCombo =
                    towerModel
                        .GetBehavior<DamageBasedAttackSpeedModel>();

                if (existingCombo == null)
                {
                    var combo =
                        sourceCombo.Duplicate();

                    combo.damageThreshold = 1f;

                    // +1% per combo stack
                    combo.increasePerThreshold = 0.01f;

                    // YES.
                    combo.maxStacks = 1000;

                    combo.maxTimeInFramesWithoutDamage = 120;

                    combo.damageCap = 1f;

                    towerModel.AddBehavior(
                        combo
                    );
                }
            }


            // ====================================================
            // FARWIND RANGE / DAMAGE SCALING
            // ====================================================

            var sourceRangeDamage =
                farwind
                    .GetBehavior<TowerRangeDamageBuffModel>();

            if (sourceRangeDamage != null)
            {
                var existingRangeDamage =
                    towerModel
                        .GetBehavior<TowerRangeDamageBuffModel>();

                if (existingRangeDamage == null)
                {
                    towerModel.AddBehavior(
                        sourceRangeDamage.Duplicate()
                    );
                }
            }


            // ====================================================
            // FARWIND RANGE SUPPORT
            // ====================================================

            var sourceRangeSupport =
                farwind
                    .GetBehavior<RangeSupportModel>();

            if (sourceRangeSupport != null)
            {
                var existingRangeSupport =
                    towerModel
                        .GetBehavior<RangeSupportModel>();

                if (existingRangeSupport == null)
                {
                    var rangeSupport =
                        sourceRangeSupport.Duplicate();

                    rangeSupport.mutatorId =
                        "SingularityJuggernautRange";

                    towerModel.AddBehavior(
                        rangeSupport
                    );
                }
            }


            // ====================================================
            // STORMWRATH GALVANIZED SYSTEM
            //
            // This is the cursed stun/combo payoff.
            // ====================================================

            var stormProjectile =
                stormwrath
                    .GetWeapon()
                    .projectile;

            var sourceGalvanized =
                stormProjectile
                    .GetBehavior<GalvanizedModel>();

            if (sourceGalvanized != null)
            {
                var existingGalvanized =
                    weapon.projectile
                        .GetBehavior<GalvanizedModel>();

                if (existingGalvanized == null)
                {
                    weapon.projectile.AddBehavior(
                        sourceGalvanized.Duplicate()
                    );
                }
            }


            // ====================================================
            // WINTER'S MERCY DAMAGE SYSTEM
            // ====================================================

            var sourceWinterBuff =
                wintersMercy
                    .GetBehavior<WintersMercyTowerBuffModel>();

            if (sourceWinterBuff != null)
            {
                var existingWinterBuff =
                    towerModel
                        .GetBehavior<WintersMercyTowerBuffModel>();

                if (existingWinterBuff == null)
                {
                    towerModel.AddBehavior(
                        sourceWinterBuff.Duplicate()
                    );
                }
            }


            // ====================================================
            // WINTER'S MERCY FROZEN-POP PIERCE
            // ====================================================

            var sourceFrozenPierce =
                wintersMercy
                    .GetBehavior<StatePoppedBasedPierceModel>();

            if (sourceFrozenPierce != null)
            {
                var existingFrozenPierce =
                    towerModel
                        .GetBehavior<StatePoppedBasedPierceModel>();

                if (existingFrozenPierce == null)
                {
                    towerModel.AddBehavior(
                        sourceFrozenPierce.Duplicate()
                    );
                }
            }
        }


        // ========================================================
        // ========================================================
        //
        // MIDDLE T5
        //
        // GOD OF THE ARMIES
        //
        // ========================================================
        // ========================================================

        private static void ApplyGodOfTheArmies(
            TowerModel towerModel)
        {
            var trueGod =
                Game.instance.model
                    .GetTowerFromId("SuperMonkey-500");


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
            // STEAL TRUE SUN GOD SUPPORT SACRIFICE GROUPS
            //
            // towerSet serializes as 8 for Support.
            //
            // We use ToString() because TowerSet is an enum and
            // comparing directly against integer 8 caused:
            //
            // "Operator != cannot be applied..."
            //
            // This version already compiled for us.
            // ====================================================

            foreach (
                var group
                in trueGod
                    .GetBehaviors<
                        TempleTowerMutatorGroupTierTwoModel
                    >()
            )
            {
                if (group.towerSet.ToString() != "Support")
                {
                    continue;
                }


                if (group.mutators == null)
                {
                    continue;
                }


                foreach (var mutator in group.mutators)
                {
                    var addBehavior =
                        mutator.TryCast<
                            AddBehaviorToTowerMutatorModel
                        >();

                    if (
                        addBehavior == null ||
                        addBehavior.behaviors == null
                    )
                    {
                        continue;
                    }


                    foreach (
                        var sourceBehavior
                        in addBehavior.behaviors
                    )
                    {
                        if (sourceBehavior == null)
                        {
                            continue;
                        }


                        var behavior =
                            sourceBehavior.Duplicate();


                        // -----------------------------------------
                        // ATTACK SPEED
                        // -----------------------------------------

                        var rate =
                            behavior
                                .TryCast<RateSupportModel>();

                        if (rate != null)
                        {
                            rate.isGlobal = true;
                            rate.isCustomRadius = false;
                            rate.customRadius = 0f;
                        }


                        // -----------------------------------------
                        // RANGE
                        // -----------------------------------------

                        var range =
                            behavior
                                .TryCast<RangeSupportModel>();

                        if (range != null)
                        {
                            range.isGlobal = true;
                            range.isCustomRadius = false;
                            range.customRadius = 0f;
                        }


                        // -----------------------------------------
                        // PIERCE
                        // -----------------------------------------

                        var pierce =
                            behavior
                                .TryCast<PierceSupportModel>();

                        if (pierce != null)
                        {
                            pierce.isGlobal = true;
                            pierce.isCustomRadius = false;
                            pierce.customRadius = 0f;
                        }


                        // -----------------------------------------
                        // DAMAGE
                        // -----------------------------------------

                        var damage =
                            behavior
                                .TryCast<DamageSupportModel>();

                        if (damage != null)
                        {
                            damage.isGlobal = true;
                            damage.isCustomRadius = false;
                            damage.customRadius = 0f;
                        }


                        // -----------------------------------------
                        // DISCOUNT
                        // -----------------------------------------

                        var discount =
                            behavior
                                .TryCast<DiscountZoneModel>();

                        if (discount != null)
                        {
                            discount.isGlobal = true;
                            discount.isGlobalRange = true;
                        }


                        towerModel.AddBehavior(
                            behavior
                        );
                    }
                }
            }
        }


        // ========================================================
        // ========================================================
        //
        // BOTTOM T5
        //
        // UNMISSABLE PRECISION
        //
        // ========================================================
        // ========================================================

        private static void ApplyUnmissablePrecision(
            WeaponModel weapon)
        {
            // ====================================================
            // T5 ATTACK SPEED
            //
            // Unmissable Precision doesn't hesitate.
            // 5x attack speed.
            // ====================================================

            weapon.rate *= 0.20f;

            // ====================================================
            // T5 DAMAGE BOOST
            //
            // Bottom already has:
            //
            // Superb Force        x15
            // Critically Accurate x2
            //
            // = x30
            //
            // T5 adds:
            // x10
            //
            // = x300 TOTAL
            // ====================================================

            var damage =
                weapon.projectile
                    .GetDamageModel();

            if (damage != null)
            {
                damage.damage *= 10f;
            }


            // ====================================================
            // POP EVERYTHING
            //
            // Copy Apex's damage immunities.
            //
            // Lead is no longer allowed to bully our T5.
            // ====================================================

            var apex =
                Game.instance.model
                    .GetTowerFromId("DartMonkey-Paragon");

            var apexDamage =
                apex.GetWeapon()
                    .projectile
                    .GetDamageModel();

            if (
                damage != null &&
                apexDamage != null
            )
            {
                damage.immuneBloonProperties =
                    apexDamage.immuneBloonProperties;

                damage.immuneBloonPropertiesOriginal =
                    apexDamage.immuneBloonPropertiesOriginal;
            }


            // ====================================================
            // HOMING
            //
            // Base Sub TrackTargetModel:
            //
            // distance       9999
            // seek angle     180
            // turn rate      360
            //
            // Since After-images now increases LIFETIME instead
            // of projectile speed, this should actually get time
            // to steer toward the target.
            // ====================================================

            var sub =
                Game.instance.model
                    .GetTowerFromId("MonkeySub");

            var subProjectile =
                sub.GetWeapon()
                    .projectile;

            var sourceTracking =
                subProjectile
                    .GetBehavior<TrackTargetModel>();

            if (sourceTracking != null)
            {
                var existingTracking =
                    weapon.projectile
                        .GetBehavior<TrackTargetModel>();

                if (existingTracking == null)
                {
                    weapon.projectile.AddBehavior(
                        sourceTracking.Duplicate()
                    );
                }
            }
        }


        // ========================================================
        // ========================================================
        //
        // DISPLAYS
        //
        // ========================================================
        // ========================================================

        private static void ApplyDisplay(
            TowerModel towerModel,
            int top,
            int middle,
            int bottom)
        {
            // ====================================================
            // T5
            // ====================================================

            if (top >= 5)
            {
                // ================================================
                // SINGULARITY JUGGERNAUT
                //
                // TEMPORARY:
                // Ultra-Juggernaut model.
                //
                // White Juggernaut comes later.
                // ================================================

                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-500")
                        .display;

                return;
            }


            if (middle >= 5)
            {
                // God of the Armies

                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("SuperMonkey-500")
                        .display;

                return;
            }


            if (bottom >= 5)
            {
                // Unmissable Precision

                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-005")
                        .display;

                return;
            }


            // ====================================================
            // T4
            // ====================================================

            if (top >= 4)
            {
                // Apex Plasma Master keeps Ultra-Juggernaut model.

                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-500")
                        .display;

                return;
            }


            if (middle >= 4)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-050")
                        .display;

                return;
            }


            if (bottom >= 4)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-004")
                        .display;

                return;
            }


            // ====================================================
            // T3
            // ====================================================

            if (top >= 3)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-500")
                        .display;

                return;
            }


            if (middle >= 3)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-040")
                        .display;

                return;
            }


            if (bottom >= 3)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-003")
                        .display;

                return;
            }


            // ====================================================
            // TOP T2
            // ====================================================

            if (top >= 2)
            {
                if (middle >= 1)
                {
                    towerModel.display =
                        Game.instance.model
                            .GetTowerFromId("DartMonkey-420")
                            .display;
                }
                else if (bottom >= 1)
                {
                    towerModel.display =
                        Game.instance.model
                            .GetTowerFromId("DartMonkey-402")
                            .display;
                }
                else
                {
                    towerModel.display =
                        Game.instance.model
                            .GetTowerFromId("DartMonkey-400")
                            .display;
                }

                return;
            }


            // ====================================================
            // MIDDLE T2
            // ====================================================

            if (middle >= 2)
            {
                if (top >= 1)
                {
                    towerModel.display =
                        Game.instance.model
                            .GetTowerFromId("DartMonkey-230")
                            .display;
                }
                else if (bottom >= 1)
                {
                    towerModel.display =
                        Game.instance.model
                            .GetTowerFromId("DartMonkey-032")
                            .display;
                }
                else
                {
                    towerModel.display =
                        Game.instance.model
                            .GetTowerFromId("DartMonkey-030")
                            .display;
                }

                return;
            }


            // ====================================================
            // BOTTOM T2
            // ====================================================

            if (bottom >= 2)
            {
                if (top >= 1)
                {
                    towerModel.display =
                        Game.instance.model
                            .GetTowerFromId("DartMonkey-203")
                            .display;
                }
                else if (middle >= 1)
                {
                    towerModel.display =
                        Game.instance.model
                            .GetTowerFromId("DartMonkey-023")
                            .display;
                }
                else
                {
                    towerModel.display =
                        Game.instance.model
                            .GetTowerFromId("DartMonkey-002")
                            .display;
                }

                return;
            }


            // ====================================================
            // T1 CROSSPATHS
            // ====================================================

            if (top >= 1 && middle >= 1)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-220")
                        .display;

                return;
            }


            if (top >= 1 && bottom >= 1)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-202")
                        .display;

                return;
            }


            if (middle >= 1 && bottom >= 1)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-022")
                        .display;

                return;
            }


            // ====================================================
            // PURE T1
            // ====================================================

            if (top >= 1)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-200")
                        .display;
            }
            else if (middle >= 1)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-020")
                        .display;
            }
            else if (bottom >= 1)
            {
                towerModel.display =
                    Game.instance.model
                        .GetTowerFromId("DartMonkey-002")
                        .display;
            }
        }
    }
}
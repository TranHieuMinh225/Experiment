using MelonLoader;
using BTD_Mod_Helper;

[assembly: MelonInfo(
    typeof(SuperSoldierExperiment.SuperSoldierExperimentMod),
    "Super Soldier Experiment",
    "0.1.0",
    "Hieu Minh"
)]

[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace SuperSoldierExperiment
{
    public class SuperSoldierExperimentMod : BloonsTD6Mod
    {
        public override void OnApplicationStart()
        {
            ModHelper.Msg<SuperSoldierExperimentMod>(
                "Super Soldier Experiment loaded!"
            );
        }
    }
}
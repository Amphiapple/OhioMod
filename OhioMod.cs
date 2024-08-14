using MelonLoader;

using BTD_Mod_Helper;

using BTD_Mod_Helper.Extensions;

using OhioMod;

using Harmony;

using System;

using System.Text.RegularExpressions;

using Il2Cpp;

using Il2CppAssets.Scripts.Models.Towers;

using Il2CppAssets.Scripts.Models.Towers.Behaviors;

using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;

using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

using Il2CppAssets.Scripts.Models.Towers.Projectiles;

using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

using Il2CppAssets.Scripts.Unity;

using Il2CppAssets.Scripts.Unity.Scenes;

[assembly: MelonInfo(typeof(OhioMod.OhioMod), OhioMod.ModHelperData.Name, OhioMod.ModHelperData.Version, OhioMod.ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace OhioMod;

public class OhioMod : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        base.OnApplicationStart();
        Console.WriteLine("Entering Ohio...");
    }

    [HarmonyPatch(typeof(TitleScreen), "Start")]
    public class Awake_Patch
    {

        [HarmonyPostfix]
        public static void Postfix()
        {
            PriceChanges();

            foreach (TowerModel tower in Game.instance.model.towers)
            {
                SuperMonkey(tower);
                CaptainChurchill(tower);
            }
        }
    }

    //Price changes
    private static void PriceChanges()
    {
        Game.instance.model.GetUpgrade("Knockback").cost = 2000;
        Game.instance.model.GetUpgrade("Dark Knight").cost = 6500;
    }

    //Super changes
    private static void SuperMonkey(TowerModel t)
    {
        if (Regex.IsMatch(t.name, "SuperMonkey-..[1-2]"))
        {
            foreach (var w in t.GetWeapons())
            {
                try
                {
                    //Decrease bloon and moab knockback
                    var b = w.GetBehavior<KnockbackModel>();
                    b.lightMultiplier = 1.2f;
                    b.moabMultiplier = 0.2f;
                }
                catch
                {

                }
                
            }
        }

        if (Regex.IsMatch(t.name, "SuperMonkey-3.."))
        {
            foreach (var w in t.GetWeapons())
            {
                try
                {
                    w.emission.Cast<RandomArcEmissionModel>().count = 4;
                }
                catch
                {

                }
            }
        }

        if (Regex.IsMatch(t.name, "SuperMonkey-.[3-5]."))
        {
            foreach (var w in t.GetWeapons())
            {
                w.projectile.pierce += 1;
            }
        }

        if (Regex.IsMatch(t.name, "SuperMonkey-..[3-5]"))
        {
            foreach (var w in t.GetWeapons())
            {
                try
                {
                    //Increase bloon knockback
                    w.GetBehavior<KnockbackModel>().lightMultiplier = 1.3f;
                }
                catch
                {

                }
            }
        }
    }

    //Churchill changes
    private static void CaptainChurchill(TowerModel t)
    {
        if (Regex.IsMatch(t.name, "CaptainChurchill"))
        {
            foreach (var w in t.GetWeapons())
            {
                if (w.projectile.id == "Projectile")
                {
                    w.rate /= 1.5f;
                    var b = w.projectile.GetBehavior<CreateProjectileOnExhaustPierceModel>();
                    b.minimumTimeDifferenceInFrames = 2;
                    b.projectile.GetDamageModel().damage /= 1.5f;

                    try
                    {
                        var c = b.projectile.GetBehavior<DamageModifierForTagModel>();
                        if (c.tag == "Fortified")
                        {
                            c.damageAddative /= 1.5f;
                        }
                    }
                    catch
                    {

                    }
                }
            }

            foreach (var a in t.GetAbilities())
            {
                foreach (var b in a.behaviors)
                {
                    try
                    {
                        var d = b.Cast<MutateProjectileOnAbilityModel>();
                        d.damageIncrease *= 2;
                        d.projectileBehaviorModel.Cast<DamageModifierForTagModel>().damageAddative /= 1.5f;
                    }
                    catch
                    {

                    }
                }
            }
        }
    }
}
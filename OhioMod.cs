using MelonLoader;

using BTD_Mod_Helper;

using BTD_Mod_Helper.Extensions;

using OhioMod;

using HarmonyLib;

using System;

using System.Text.RegularExpressions;

using Il2Cpp;

using Il2CppAssets.Scripts.Models.Towers;

using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;

using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

using Il2CppAssets.Scripts.Unity;

using Il2CppAssets.Scripts.Unity.Scenes;
using Il2CppAssets.Scripts.Models.Bloons.Behaviors;

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
                MonkeyBuccaneer(tower);
                MortarMonkey(tower);
                SuperMonkey(tower);
                SpikeFactory(tower);
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

    //Boat changes
    private static void MonkeyBuccaneer(TowerModel t)
    {
        EmissionModel e = null;
        foreach (var w in Game.instance.model.GetTowerFromId("MonkeyBuccaneer-200").GetWeapons())
        {
            //Get double shot attack
            if (w.name == "WeaponModel_Weapon")
            {
                e = w.emission;
            }
        }

        if (Regex.IsMatch(t.name, "MonkeyBuccaneer-23."))
        {
            foreach (var w in t.GetWeapons())
            {
                //Add double shot attack to cannon
                if (w.name.Contains("Cannon"))
                {
                    w.emission = e;
                    w.emission.Cast<ParallelEmissionModel>().spreadLength = 6;
                }
            }
        }

        if (Regex.IsMatch(t.name, "MonkeyBuccaneer-.[4-5]."))
        {
            foreach (var w in t.GetWeapons())
            {
                if (w.name.Contains("Cannon"))
                {
                    //Increase explosion damage
                    w.projectile.GetBehavior<CreateProjectileOnContactModel>().projectile.GetDamageModel().damage += 1;

                    //Add additional bomb
                    if (Regex.IsMatch(t.name, "MonkeyBuccaneer-2[4-5]."))
                    {
                        try
                        {
                            w.emission.Cast<ArcEmissionModel>().count = 4;
                        }
                        catch
                        {

                        }
                    }
                }
            }
        }
    }

    //Mortar changes
    private static void MortarMonkey(TowerModel t)
    {
        if (Regex.IsMatch(t.name, "MortarMonkey-..[2-5]"))
        {
            foreach (var w in t.GetWeapons())
            {
                foreach (var b in w.projectile.GetBehaviors<CreateProjectileOnExhaustFractionModel>())
                {
                    try
                    {
                        //Increase burn rate
                        var c = b.projectile.GetBehavior<AddBehaviorToBloonModel>();
                        c.GetBehavior<DamageOverTimeModel>().interval = 0.5f;
                        c.GetBehavior<DamageOverTimeModel>().Interval = 0.5f;

                        //change burn duration
                        if (Regex.IsMatch(t.name, "MortarMonkey-..5"))
                        {
                            c.lifespan = 5.1f;
                        }
                        else
                        {
                            c.lifespan = 1.6f;
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
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
                //Increase sun avatar projectiles
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
                //Increase robo monkey pierce
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

    //Spac changes
    private static void SpikeFactory(TowerModel t)
    {

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
                    //Increase main attack rate
                    w.rate /= 1.5f;

                    //Decrease main attack explosion delay
                    var b = w.projectile.GetBehavior<CreateProjectileOnExhaustPierceModel>();
                    b.minimumTimeDifferenceInFrames = 2;

                    //Decrease explosion damage
                    b.projectile.GetDamageModel().damage /= 1.5f;

                    //Decrease explosion fortified damage
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
                //Fix ability damage
                try
                {
                    var d = a.GetBehavior<MutateProjectileOnAbilityModel>();
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
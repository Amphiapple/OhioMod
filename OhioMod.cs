using MelonLoader;

using BTD_Mod_Helper;

using BTD_Mod_Helper.Extensions;

using OhioMod;

using HarmonyLib;

using System;

using System.Text.RegularExpressions;

using Il2Cpp;

using Il2CppAssets.Scripts.Models.Bloons.Behaviors;

using Il2CppAssets.Scripts.Models.Towers;

using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;

using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;

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
        var models = Game.instance.model;
        models.GetUpgrade("Knockback").cost = 2000;
        models.GetUpgrade("Dark Knight").cost = 6500;
    }

    //Boat changes
    private static void MonkeyBuccaneer(TowerModel t)
    {
        EmissionModel e = null;
        foreach (var w in Game.instance.model.GetTowerFromId("MonkeyBuccaneer-200").GetWeapons())
        {
            //Get double shot emission
            if (w.name == "WeaponModel_Weapon")
            {
                e = w.emission;
            }
        }

        if (Regex.IsMatch(t.name, "MonkeyBuccaneer-.[2-5]."))
        {
            foreach (var w in t.GetWeapons())
            {
                //Get grape shot weapon
                if (w.name.Contains("Grape"))
                {
                    try
                    {
                        //Increase hot shot burn rate
                        var b = w.projectile.GetBehavior<AddBehaviorToBloonModel>();
                        if (Regex.IsMatch(t.name, "MonkeyBuccaneer-.5."))
                        {
                            b.lifespan = 5.1f;
                        }
                        else
                        {
                            b.lifespan = 1.6f;
                        }

                        //Decrease hot shot burn duration and damage
                        var d = b.GetBehavior<DamageOverTimeModel>();
                        d.damage = 1;
                        d.interval = 0.5f;
                        d.Interval = 0.5f;
                    }
                    catch
                    {

                    }

                    if (Regex.IsMatch(t.name, "MonkeyBuccaneer-.3."))
                    {
                        //Decrease cannon ship grape damage
                        w.projectile.GetDamageModel().damage = 2;
                    }

                    if (Regex.IsMatch(t.name, "MonkeyBuccaneer-.4."))
                    {
                        //Decrease monkey pirates grape damage
                        w.projectile.GetDamageModel().damage = 4;

                        try
                        {
                            //Increase monkey pirates burn damage
                            w.projectile.GetBehavior<AddBehaviorToBloonModel>().GetBehavior<DamageOverTimeModel>().damage = 2;
                        }
                        catch
                        {

                        }
                    }

                    if (Regex.IsMatch(t.name, "MonkeyBuccaneer-.5."))
                    {
                        try
                        {
                            //Increase pirate lord burn damage
                            w.projectile.GetBehavior<AddBehaviorToBloonModel>().GetBehavior<DamageOverTimeModel>().damage = 4;
                        }
                        catch
                        {

                        }
                    }
                }

                //Get cannon weapon
                if (w.name.Contains("Cannon"))
                {

                    if (Regex.IsMatch(t.name, "MonkeyBuccaneer-23."))
                    {
                        try
                        {
                            //Increase double shot cannon ship projectiles
                            w.emission = e;
                            w.emission.Cast<ParallelEmissionModel>().spreadLength = 6;
                        }
                        catch
                        {

                        }
                    }

                    if (Regex.IsMatch(t.name, "MonkeyBuccaneer-2[4-5]."))
                    {
                        try
                        {
                            //Increase double shot monkey pirates projectiles
                            w.emission.Cast<ArcEmissionModel>().count = 4;
                        }
                        catch
                        {

                        }
                    }

                    if (Regex.IsMatch(t.name, "MonkeyBuccaneer-.4."))
                    {
                        //Increase monkey pirates bomb damage
                        var p = w.projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
                        p.GetDamageModel().damage = 4;
                    }

                    if (Regex.IsMatch(t.name, "MonkeyBuccaneer-.5."))
                    {
                        //Increase monkey pirates bomb damage
                        var p = w.projectile.GetBehavior<CreateProjectileOnContactModel>().projectile;
                        p.GetDamageModel().damage = 8;
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
                //Get burn projectiles
                foreach (var b in w.projectile.GetBehaviors<CreateProjectileOnExhaustFractionModel>())
                {
                    try
                    {
                        //change burny stuff burn duration
                        var c = b.projectile.GetBehavior<AddBehaviorToBloonModel>();
                        if (Regex.IsMatch(t.name, "MortarMonkey-..5"))
                        {
                            c.lifespan = 5.1f;
                        }
                        else
                        {
                            c.lifespan = 1.6f;
                        }

                        //Increase burny stuff burn rate
                        var d = c.GetBehavior<DamageOverTimeModel>();
                        d.interval = 0.5f;
                        d.Interval = 0.5f;
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
                try
                {
                    //Increase sun avatar projectiles
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
                w.projectile.pierce = 6;
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
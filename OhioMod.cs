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

using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;

using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;



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
                DartMonkey(tower);
                TackShooter(tower);
                MonkeyBuccaneer(tower);
                MonkeyAce(tower);
                MortarMonkey(tower);
                SuperMonkey(tower);
                Mermonkey(tower);
                BeastHandler(tower);
                CaptainChurchill(tower);
            }
        }
    }

    //Price changes
    private static void PriceChanges()
    {
        var models = Game.instance.model;
        models.GetUpgrade("Neva-Miss Targeting").cost = 2000;
        models.GetUpgrade("Spectre").cost = 24000;
        models.GetUpgrade("Flying Fortress").cost = 80000;
        models.GetUpgrade("Knockback").cost = 2000;
        models.GetUpgrade("Dark Knight").cost = 6500;
    }

    //Dart changes
    private static void DartMonkey(TowerModel t)
    {
        
    }

    //Tack changes
    private static void TackShooter(TowerModel t)
    {
        if (Regex.IsMatch(t.name, "TackShooter-.[4-5]."))
        {
            try
            {
                t.GetAbility().GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].GetBehavior<SpinModel>().rotationPerSecond = 120;
                t.GetAbility().GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].emission.Cast<ArcEmissionModel>().count = 1;
            }
            catch
            {

            }
        }
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

    //Ace changes
    private static void MonkeyAce(TowerModel t)
    {
        if (Regex.IsMatch(t.name, "MonkeyAce-..[3-5]"))
        {
            foreach (var w in t.GetWeapons())
            {
                if (w.projectile.name == "ProjectileModel_MainProjectile")
                {
                    try
                    {
                        w.projectile.GetBehavior<TrackTargetModel>().turnRate = 360;
                    }
                    catch
                    {

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
                    }
                    catch
                    {

                    }

                    try
                    {
                        if (b.projectile.name == "ProjectileModel_WallOfFire")
                        {
                            if (Regex.IsMatch(t.name, "MortarMonkey-[1-2].5"))
                            {
                                b.projectile.scale = 2;
                                b.projectile.radius = 35;
                            }
                            else if (Regex.IsMatch(t.name, "MortarMonkey-..5"))
                            {
                                b.projectile.scale = 1.5f;
                                b.projectile.radius = 25;
                            }
                            
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

    //Merm changes
    private static void Mermonkey(TowerModel t)
    {
        MapBorderReboundModel b = null;
        var a = Game.instance.model.GetTowerFromId("Mermonkey-050").GetAbility();
        foreach (var c in a.GetBehavior<ActivateAttackModel>().attacks[0].weapons)
        {
            if (c.name == "WeaponModel_Weapon")
            {
                //Increase arctic knight ability lifespan
                c.projectile.GetBehavior<TravelStraitModel>().lifespan = 15;

                //Get map border bouncing model
                b = c.projectile.GetBehavior<MapBorderReboundModel>();
            }
        }

        if (Regex.IsMatch(t.name, "Mermonkey-..1"))
        {
            foreach (var w in t.GetWeapons())
            {
                try
                {
                    //Increase echosence precision turn rate
                    if (Regex.IsMatch(t.name, "Mermonkey-.[3-5][1-2]"))
                    {
                        w.projectile.GetBehavior<TrackTargetModel>().turnRate = 150;
                        w.projectile.GetBehavior<TrackTargetModel>().TurnRate = 150;
                    }
                    else
                    {
                        w.projectile.GetBehavior<TrackTargetModel>().turnRate = 300;
                        w.projectile.GetBehavior<TrackTargetModel>().TurnRate = 300;
                    }
                }
                catch
                {

                }
            }
        }
        
        if (Regex.IsMatch(t.name, "Mermonkey-.5."))
        {
            try
            {
                foreach (var w in t.GetWeapons())
                {
                    //Remove one weapon, add projectile to other weapon
                    if (w.name == "WeaponModel_Weapon")
                    {
                        t.GetBehavior<AttackModel>().RemoveWeapon(w);
                    }
                    else if (w.name == "WeaponModel_Weapon2")
                    {
                        var e = w.emission.Cast<ArcEmissionModel>();
                        e.count = 3;
                        e.angle = 45;
                    }
                }
            }
            catch
            {

            }
        }

        if (Regex.IsMatch(t.name, "Mermonkey-.4."))
        {
            try
            {
                //Add arctic knight map border bouncing 
                var p = t.GetAbility().GetBehavior<ActivateAttackModel>().attacks[0].weapons[0].projectile;
                p.AddBehavior(b);
                p.GetBehavior<TravelStraitModel>().lifespan = 5;
            }
            catch
            {

            }
        }

        if (Regex.IsMatch(t.name, "Mermonkey-.3."))
        {
            foreach (var w in t.GetWeapons())
            {
                var p = w.projectile;
                if (p.id == "Projectile")
                {
                    //Increase riptide champion pierce
                    p.CapPierce(0);
                    p.pierce = 14;

                    try
                    {
                        //Increase riptide scaling
                        p.GetBehavior<ScaleDamageWithTimeModel>().scalePerSecond = 0.75f;

                        //Increase riptide champion freeze duration and damage
                        p.GetBehavior<FreezeModel>().lifespan = 0.5f;
                        p.GetBehavior<DamageModifierForBloonStateModel>().damageAdditive = 3;

                        //Increase riptide champion damage and give plasma damage type
                        var d = p.GetDamageModel();
                        d.CapDamage(0);
                        d.damage = 6;
                        d.immuneBloonProperties = (Il2Cpp.BloonProperties)8;
                        d.immuneBloonPropertiesOriginal = (Il2Cpp.BloonProperties)8;

                        //Increase riptide champion split strength
                        p = p.GetBehavior<ScaleProjectileOverTimeModel>().bonusProjectileModel;
                        p.CapPierce(0);
                        p.pierce = 12;
                        p.GetBehavior<DamageModifierForBloonStateModel>().damageAdditive = 3;
                        d = w.projectile.GetBehavior<ScaleProjectileOverTimeModel>().bonusProjectileModel.GetDamageModel();
                        d.CapDamage(0);
                        d.damage = 6;
                        d.immuneBloonProperties = (Il2Cpp.BloonProperties)8;
                        d.immuneBloonPropertiesOriginal = (Il2Cpp.BloonProperties)8;
                    }
                    catch
                    {

                    }
                }
            }
        }
    }

    //Beast changes
    private static void BeastHandler(TowerModel t)
    {
        if (Regex.IsMatch(t.name, "BeastHandler-5.."))
        {
            try
            {
                var p = t.GetBehavior<BeastHandlerLeashModel>().towerModel;
                p.GetBehavior<BeastHandlerPetModel>().damageRange = 1200;
                var a = p.GetWeapon(0).projectile.GetBehavior<CreateProjectileOnExhaustFractionModel>().projectile.GetBehavior<CreateGreatWhiteEffectModel>();
                a.thrashingProjectileModel.GetBehavior<DamageModel>().damage = 1050;
                a.bloonFollowProjectileModel.GetBehavior<CreateProjectileOnIntervalModel>().projectile.GetBehavior<DamageModel>().damage = 1050;
            }
            catch
            {

            }
        }
    }

    //Churchill changes
    private static void CaptainChurchill(TowerModel t)
    {
        /*
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
        */
    }
}
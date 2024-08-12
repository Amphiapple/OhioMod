using MelonLoader;
using BTD_Mod_Helper;
using Harmony;
using Il2CppSystem.Collections;
using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Models.Towers.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Upgrades;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Attack;
using Il2CppAssets.Scripts.Simulation;
using Il2CppAssets.Scripts.Simulation.Objects;
using Il2CppAssets.Scripts.Simulation.Bloons;
using Il2CppAssets.Scripts.Simulation.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.Weapons;
using Il2CppAssets.Scripts.Unity;
using Il2CppAssets.Scripts.Unity.Bridge;
using Il2CppAssets.Scripts.Unity.Scenes;
using Il2CppAssets.Scripts.Unity.UI_New.InGame;
using Il2CppAssets.Scripts.Unity.UI_New.InGame.Races;
using Il2CppAssets.Scripts.Unity.UI_New.Main;
using Il2CppAssets.Scripts.Unity.UI_New.Popups;
using Il2CppAssets.Scripts.Utils;
using Il2CppTMPro;
using System;
using UnityEngine;
using BTD_Mod_Helper.Extensions;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Emissions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;
using UnityEngine.UIElements;
using Il2CppAssets.Scripts.Unity.Towers;
using Il2CppAssets.Scripts.Unity.Towers.Upgrades;
using System.Text.RegularExpressions;
using Il2CppAssets.Scripts.Models.Towers.Projectiles;
using System.Linq;
using Il2CppAssets.Scripts.Models.Towers.Weapons.Behaviors;
using Il2CppAssets.Scripts.Models.Towers.Behaviors.Abilities.Behaviors;


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

            var models = Game.instance.model;

            foreach (TowerModel tower in Game.instance.model.towers)
            {
                //Duration and Cooldown tester
                if (Regex.IsMatch(tower.name, "TackShooter-.5."))
                {
                    foreach (var a in tower.GetAbilities())
                    {
                        a.cooldown *= 2;
                        foreach (var b in a.behaviors)
                        {
                            try
                            {
                                b.Cast<ActivateAttackModel>().lifespan *= 4;
                                
                            }
                            catch
                            {

                            }
                            
                        }
                    }
                }

                //Damage tester
                if (Regex.IsMatch(tower.name, "DartMonkey-..[3-5]"))
                {
                    foreach (var w in tower.GetWeapons())
                    {
                        w.projectile.GetDamageModel().damage *= 10;
                    }
                }
                if (Regex.IsMatch(tower.name, "DartMonkey-..[4-5]"))
                {
                    foreach (var w in tower.GetWeapons())
                    {
                        foreach (var b in w.behaviors)
                        {
                            try
                            {
                                b.Cast<CritMultiplierModel>().damage *= 10;
                            }
                            catch
                            {

                            }
                        }
                        
                    }
                }

                //Pierce tester
                if (Regex.IsMatch(tower.name, "DartMonkey-[1-5].."))
                {
                    foreach (var w in tower.GetWeapons())
                    {
                        w.projectile.pierce += 100;
                    }
                }

                //Rate tester
                if (Regex.IsMatch(tower.name, "DartMonkey-.[1-5]."))
                {
                    foreach (var w in tower.GetWeapons())
                    {
                        w.rate *= 0.1f;
                    }
                }

                //Range tester
                if (Regex.IsMatch(tower.name, "DartMonkey-..[1-5]"))
                {
                    tower.range *= 1.5f;

                    foreach (var bev in tower.behaviors)
                    {
                        try
                        {
                            bev.Cast<AttackModel>().range *= 1.5f;
                        }
                        catch
                        {

                        }

                    }
                }
                
            }

            //Cost tester
            models.GetTowerFromId("DartMonkey").cost = 100;
            models.GetUpgrade("Plasma Monkey Fan Club").cost = 35000;
            
        }
    }

}
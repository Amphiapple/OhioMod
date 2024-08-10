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


[assembly: MelonInfo(typeof(OhioMod.OhioMod), OhioMod.ModHelperData.Name, OhioMod.ModHelperData.Version, OhioMod.ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]

namespace OhioMod;

public class OhioMod : BloonsTD6Mod
{
    public override void OnApplicationStart()
    {
        base.OnApplicationStart();
    }

    [HarmonyPatch(typeof(TitleScreen), "Start")]
    public class Awake_Patch
    {

        [HarmonyPostfix]
        public static void Postfix()
        {
            foreach (TowerModel tower in Game.instance.model.towers)
            {
                //Range tester
                if (tower.baseId == "IceMonkey" && tower.HasUpgrade(1, 3))
                {
                    tower.range *= (float)2.5;

                    foreach (var bev in tower.behaviors)
                    {
                        try
                        {
                            bev.Cast<AttackModel>().range *= (float)2.5;
                        }
                        catch
                        {

                        }

                    }
                }
            }

            foreach (UpgradeModel upgrade in Game.instance.model.upgrades)
            {
                //Cost tester
                Console.WriteLine(upgrade.name);
                if (upgrade.name == "Super Monkey Fan Club")
                {
                    upgrade.cost = 3000;
                }
                if (upgrade.name == "Plasma Monkey Fan Club")
                {
                    upgrade.cost = 35000;
                }
                if (upgrade.name == "Crossbow Master")
                {
                    upgrade.cost = 20000;
                }
                if (upgrade.name == "Glaive Lord")
                {
                    upgrade.cost = 32000;
                }
                if (upgrade.name == "MOAB Eliminator")
                {
                    upgrade.cost = 25000;
                }
                if (upgrade.name == "Snowstorm")
                {
                    upgrade.cost = 3000;
                }
                if (upgrade.name == "Cryo Cannon")
                {
                    upgrade.cost = 1750;
                }
                if (upgrade.name == "Icicles")
                {
                    upgrade.cost = 2000;
                }
                if (upgrade.name == "The Bloon Solver")
                {
                    upgrade.cost = 25000;
                }
                if (upgrade.name == "Glue Storm")
                {
                    upgrade.cost = 15000;
                }
                if (upgrade.name == "MOAB Glue")
                {
                    upgrade.cost = 4000;
                }
                if (upgrade.name == "Elite Defender")
                {
                    upgrade.cost = 12000;
                }
                if (upgrade.name == "Energizer")
                {
                    upgrade.cost = 28000;
                }
                if (upgrade.name == "Carrier Flagship")
                {
                    upgrade.cost = 26000;
                }
                if (upgrade.name == "Ground Zero")
                {
                    upgrade.cost = 14000;
                }
                if (upgrade.name == "Tsar Bomba")
                {
                    upgrade.cost = 32000;
                }
                if (upgrade.name == "Maim Moab")
                {
                    upgrade.cost = 5000;
                }
                if (upgrade.name == "Flying Fortress")
                {
                    upgrade.cost = 75000;
                }
                if (upgrade.name == "Support Chinook")
                {
                    upgrade.cost = 6000;
                }
                if (upgrade.name == "Special Poperations")
                {
                    upgrade.cost = 25000;
                }
                if (upgrade.name == "MOAB Shove")
                {
                    upgrade.cost = 3500;
                }
                if (upgrade.name == "Plasma Accelerator")
                {
                    upgrade.cost = 9000;
                }
                if (upgrade.name == "Hydra Rocket Pods")
                {
                    upgrade.cost = 4000;
                }
                if (upgrade.name == "Rocket Storm")
                {
                    upgrade.cost = 4000;
                }
                if (upgrade.name == "Buckshot")
                {
                    upgrade.cost = 0;
                }
                if (upgrade.name == "Bloon Area Denial System")
                {
                    upgrade.cost = 15500;
                }
                if (upgrade.name == "Summon Phoenix")
                {
                    upgrade.cost = 4500;
                }
                if (upgrade.name == "Sun Avatar")
                {
                    upgrade.cost = 17500;
                }
                if (upgrade.name == "Bloon Sabotage")
                {
                    upgrade.cost = 6000;
                }
                if (upgrade.name == "Bloon Master Alchemist")
                {
                    upgrade.cost = 20000;
                }
                if (upgrade.name == "Abyss Dweller")
                {
                    upgrade.cost = 1000;
                }
                if (upgrade.name == "Abyssal Warrior")
                {
                    upgrade.cost = 2500;
                }
                if (upgrade.name == "Lord of the Abyss")
                {
                    upgrade.cost = 22000;
                }
                if (upgrade.name == "Riptide Champion")
                {
                    upgrade.cost = 2000;
                }
            }
        }
    }

}
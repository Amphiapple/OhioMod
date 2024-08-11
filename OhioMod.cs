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

                //Range tester
                if (tower.baseId == "IceMonkey" && tower.HasUpgrade(1, 3))
                {
                    tower.range *= (float)1.5;

                    foreach (var bev in tower.behaviors)
                    {
                        try
                        {
                            bev.Cast<FreezeNearbyWaterModel>().radius *= (float)1.5;

                        }
                        catch
                        {

                        }
                        try
                        {
                            bev.Cast<AttackModel>().range *= (float)1.5;

                        }
                        catch
                        {

                        }

                    }
                }
            }

            //Cost Changes
            models.GetTowerFromId("Mermonkey").cost = 400;
            models.GetTowerFromId("CaptainChurchill").cost = 1000;
            models.GetTowerFromId("Corvus").cost = 2000;
            models.GetUpgrade("Super Monkey Fan Club").cost = 3000;
            models.GetUpgrade("Plasma Monkey Fan Club").cost = 35000;
            models.GetUpgrade("Crossbow Master").cost = 20000;
            models.GetUpgrade("Glaive Lord").cost = 32000;
            models.GetUpgrade("MOAB Eliminator").cost = 25000;
            models.GetUpgrade("Snowstorm").cost = 3000;
            models.GetUpgrade("Absolute Zero").cost = 16000;
            models.GetUpgrade("Cryo Cannon").cost = 1750;
            models.GetUpgrade("Icicles").cost = 2000;
            models.GetUpgrade("The Bloon Solver").cost = 25000;
            models.GetUpgrade("Glue Storm").cost = 15000;
            models.GetUpgrade("MOAB Glue").cost = 4000;
            models.GetUpgrade("Elite Sniper").cost = 12000;
            models.GetUpgrade("Elite Defender").cost = 12000;
            models.GetUpgrade("Energizer").cost = 28000;
            models.GetUpgrade("Buccaneer-Carrier Flagship").cost = 26000;
            models.GetUpgrade("Ground Zero").cost = 14000;
            models.GetUpgrade("Tsar Bomba").cost = 32000;
            models.GetUpgrade("Neva-Miss Targeting").cost = 2000;
            models.GetUpgrade("Spectre").cost = 24000;
            models.GetUpgrade("Flying Fortress").cost = 75000;
            models.GetUpgrade("Apache Dartship").cost = 13000;
            models.GetUpgrade("Apache Prime").cost = 50000;
            models.GetUpgrade("Downdraft").cost = 2500;
            models.GetUpgrade("Support Chinook").cost = 5000;
            models.GetUpgrade("Special Poperations").cost = 25000;
            models.GetUpgrade("MOAB Shove").cost = 3500;
            models.GetUpgrade("Shattering Shells").cost = 7000;
            models.GetUpgrade("Laser Cannon").cost = 3000;
            models.GetUpgrade("Plasma Accelerator").cost = 9000;
            models.GetUpgrade("Hydra Rocket Pods").cost = 4000;
            models.GetUpgrade("Rocket Storm").cost = 4000;
            models.GetUpgrade("Buckshot").cost = 0;
            models.GetUpgrade("Bloon Exclusion Zone").cost = 60000;
            models.GetUpgrade("Summon Phoenix").cost = 4500;
            models.GetUpgrade("Laser Blasts").cost = 1000;
            models.GetUpgrade("Sun Avatar").cost = 17500;
            models.GetUpgrade("Knockback").cost = 4000;
            models.GetUpgrade("Bloon Sabotage").cost = 6000;
            models.GetUpgrade("Transforming Tonic").cost = 3500;
            models.GetUpgrade("Total Transformation").cost = 35000;
            models.GetUpgrade("Bloon Master Alchemist").cost = 20000;
            models.GetUpgrade("Abyss Dweller").cost = 1000;
            models.GetUpgrade("Abyssal Warrior").cost = 3000;
            models.GetUpgrade("Lord of the Abyss").cost = 22000;
            models.GetUpgrade("Riptide Champion").cost = 555;
            models.GetUpgrade("Arctic Knight").cost = 5555;
            models.GetUpgrade("Popseidon").cost = 55555;
            models.GetUpgrade("Alluring Melody").cost = 1400;
            models.GetUpgrade("Symphonic Resonance").cost = 6000;
            models.GetUpgrade("Long Life Spikes").cost = 800;
            models.GetUpgrade("Deadly Spikes").cost = 2500;
            models.GetUpgrade("Perma-Spike").cost = 32000;
            models.GetUpgrade("Monkey Intelligence Bureau").cost = 4000;
            models.GetUpgrade("Homeland Defense").cost = 30000;
            models.GetUpgrade("Sentry Paragon").cost = 28000;
            models.GetUpgrade("Ultraboost").cost = 37000;
            models.GetUpgrade("Megalodon").cost = 30000;
            models.GetUpgrade("Giant Condor").cost = 12500;
            models.GetUpgrade("Pouakai").cost = 12500;
            
        }
    }

}
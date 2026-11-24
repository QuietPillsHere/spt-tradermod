using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BepInEx.Logging;
using Diz.Utils;
using HarmonyLib;
using SPT.Reflection.Patching;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace tarkin.tradermod.eft.Bep.Patches
{
    internal class Patch_SEB_CheckSence: ModulePatch
    {
        private static string[] _skipScenes = new string[] 
        {
            "bunker_2", // hideout
            "MenuUIScene", // main menu
            "Vendors_Prapor",
            "Vendors_Therapist",
            "Vendors_Fence",
            "Vendors_Skier",
            "Vendors_Mechanic",
            "Vendors_Ragman",
            "Vendors_Jaeger",
        };

        public static bool IsInRaidScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            if (scene.IsValid())
            {
                Plugin.Log.LogInfo($"Current Scene: {scene.name}");
                return _skipScenes.Contains(scene.name);
            }
            else
            {
                Plugin.Log.LogInfo($"Unknow Scene: {scene.name}");
                return false;
            }
        }

        protected override MethodBase GetTargetMethod()
        {
            throw new NotImplementedException();
        }
    }
}
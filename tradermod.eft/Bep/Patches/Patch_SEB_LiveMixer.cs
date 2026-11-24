using System;
using System.Collections.Generic;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using SPT.Reflection.Patching;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace tarkin.tradermod.eft.Bep.Patches
{
    internal class Patch_SEB_LiveMixer : Patch_SEB_CheckSence
    {
        protected override MethodBase GetTargetMethod()
        {
            var method = AccessTools.Method("SoundEngineBackport.LiveEngine.LiveMixer:Install");
            if (method == null)
            {
                Logger.LogInfo("SEB not found, ignore this patch");
            }
            return method;
        }

        [PatchPrefix]
        public static bool Prefix()
        {
            return !IsInRaidScene();
        }
    }
}
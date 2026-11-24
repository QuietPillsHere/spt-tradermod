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
    internal class Patch_SEB_MixerRouting : Patch_SEB_CheckSence
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method("SoundEngineBackport.LiveEngine.LiveMixerRouting:OnSceneLoaded");
        }

        [PatchPrefix]
        public static bool Prefix(Scene scene)
        {
            return !IsInRaidScene();
        }
    }
}
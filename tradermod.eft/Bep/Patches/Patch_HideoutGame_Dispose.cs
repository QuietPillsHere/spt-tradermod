using Cysharp.Threading.Tasks;
using EFT;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using static EFT.Profile;
using static EFT.TarkovApplication;
using NarrateScenesController = EFT.VendorScenePresets;

namespace tarkin.tradermod.eft.Bep.Patches
{
    // Patch AbstractGame.Dispose: add null check that case by "thead alter" patch code from Patch_NarrateController_Unload.cs
    [HarmonyPatch(typeof(HideoutGame), nameof(HideoutGame.Dispose))]
    public static class Patch_HideoutGame_Dispose
    {
        static bool Prefix(HideoutGame __instance)
        {
            //Plugin.Log.LogInfo($"Patch_HideoutGame_Dispose check null - prefix method");

            if (__instance == null)
            {
                //Plugin.Log.LogInfo($"Patch_HideoutGame_Dispose check null for true, skip");
                return false;
            }
            else
            {
                //Plugin.Log.LogInfo($"Patch_HideoutGame_Dispose check null for false, pass");
                return true;
            }
        }
    }
}

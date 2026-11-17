using Cysharp.Threading.Tasks;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using tarkin.tradermod.bep;
using UnityEngine;
using static EFT.TarkovApplication;

namespace tarkin.tradermod.eft.Bep.Patches
{
    internal class Patch_NarrateController_Unload : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_NarrateController_Unload));

        public static event Action OnPostfix;

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(NarrateController), nameof(NarrateController.Unload));
        }

        [PatchPostfix]
        private static void Postfix(ref Task __result, object __instance)
        {
            // 调试用，确认当前线程
            Logger.LogDebug($"Postfix instance:{__instance.GetHashCode()} ,thread: {Thread.CurrentThread.ManagedThreadId},");
            try
            {
                var originalTask = __result;
                UnloadWrapper(originalTask).Forget();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Failed to unload traders: {ex}");
            }
        }

        private static async UniTask UnloadWrapper(Task originalTask)
        {
            await originalTask;

            await UniTask.Yield();
            //await UniTask.DelayFrame(1);

            //await UniTask.SwitchToMainThread();

            await TraderBundleManager.UnloadAllBundles();

            //await TraderBundleManager.UnloadAllBundles().AsUniTask();

            OnPostfix?.Invoke();

            //var tcs = new TaskCompletionSource<bool>();

            //var currentContext = UniTask.ReturnToCurrentSynchronizationContext();

            //await UniTask.SwitchToThreadPool();

            //var cts = new CancellationTokenSource();

            //var unloadbundleTask = TraderBundleManager.UnloadAllBundles();

            //await UniTask.WaitUntil(() => unloadbundleTask.);

            //await UniTask.SwitchToMainThread();

            //return originalTask.ContinueWith(t => { }, TaskScheduler.Current);
        }
    }
}

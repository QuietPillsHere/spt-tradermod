using EFT;
using EFT.Quests;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;

namespace tarkin.tradermod.eft.Bep.Patches
{
    internal class Patch_QuestControllerClient_TryNotifyConditionalStatusChanged : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_QuestControllerClient_TryNotifyConditionalStatusChanged));

        public static event Action<Quest> OnPostfix;

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(QuestControllerClient), nameof(QuestControllerClient.TryNotifyConditionalStatusChanged));
        }

        [PatchPostfix]
        private static void PatchPostfix(Quest quest)
        {
            try
            {
                OnPostfix?.Invoke(quest);
            }
            catch (Exception e) { Logger.LogError(e); }
        }
    }
}
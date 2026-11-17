using EFT;
using EFT.Quests;
using EFT.Trading;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;

namespace tarkin.tradermod.eft.Bep.Patches
{
    internal class Patch_QuestsListView_Show : ModulePatch
    {
        public static event Action<QuestController, Trader> OnPostfix;

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(QuestsListView), nameof(QuestsListView.Show));
        }

        [PatchPostfix]
        private static void PatchPostfix(QuestsListView __instance, QuestController questController, Trader trader)
        {
            try
            {
                OnPostfix?.Invoke(questController, trader);
            }
            catch { }
        }
    }
}

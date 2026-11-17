using EFT.Quests;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using System.Threading.Tasks;
using static EFT.UI.WeaponModding.WeaponModdingScreen;

namespace tarkin.tradermod.eft.Bep.Patches
{
    internal class Patch_QuestObjectiveView_QuestHandover : ModulePatch
    {
        private static readonly BepInEx.Logging.ManualLogSource Logger = BepInEx.Logging.Logger.CreateLogSource(nameof(Patch_QuestObjectiveView_QuestHandover));

        public static event Action<Quest, bool> OnPostfix;

        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(QuestObjectiveView), nameof(QuestObjectiveView.QuestHandover), new Type[] { typeof(Quest) });
        }

        [PatchPostfix]
        private static void PatchPostfix(QuestObjectiveView __instance, ref Task __result, Quest selectedQuest, Condition ___Condition)
        {
            __result = Wrapper(__instance, __result, selectedQuest, ___Condition);
        }

        // check whether player handed over the items or backed out of the window
        private static async Task Wrapper(QuestObjectiveView view, Task originalTask, Quest selectedQuest, Condition condition)
        {
            double initialProgress = 0;
            bool wasDone = false;
            bool stateCapturedSuccessfully = false;

            try
            {
                if (selectedQuest != null && condition != null)
                {
                    if (selectedQuest.ProgressCheckers != null && 
                        selectedQuest.ProgressCheckers.TryGetValue(condition, out var checker))
                    {
                        initialProgress = checker.CurrentValue;
                    }
                    
                    wasDone = selectedQuest.IsConditionDone(condition);
                    stateCapturedSuccessfully = true;
                }
            }
            catch (Exception e)
            {
                Logger.LogError($"Error capturing quest state before handover: {e}");
            }

            await originalTask;

            try
            {
                if (stateCapturedSuccessfully && selectedQuest != null && condition != null)
                {
                    bool isDone = selectedQuest.IsConditionDone(condition);
                    double currentProgress = 0;

                    if (selectedQuest.ProgressCheckers != null && 
                        selectedQuest.ProgressCheckers.TryGetValue(condition, out var checker))
                    {
                        currentProgress = checker.CurrentValue;
                    }

                    bool itemsHandedOver = (isDone && !wasDone) || (currentProgress > initialProgress);

                    OnPostfix?.Invoke(selectedQuest, itemsHandedOver);
                }
            }
            catch (Exception e)
            {
                Logger.LogError($"Error calculating progress after handover: {e}");
            }
        }
    }
}

using EFT;
using EFT.InventoryLogic;
using EFT.Trading;
using EFT.UI;
using HarmonyLib;
using SPT.Reflection.Patching;
using System;
using System.Reflection;
using UnityEngine;

namespace tarkin.tradermod.bep.UI.Trading
{
    internal class Patch_BarterSchemePanel_method_5 : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(BarterSchemePanel), nameof(BarterSchemePanel.SetBuyRestrictionStatus));
        }

        [PatchPostfix]
        private static void PatchPostfix(BarterSchemePanel __instance,
            GameObject ____buyRestrictionWarning,
            Assortment ____traderAssortment)
        {
            try
            {
                if (____buyRestrictionWarning.activeSelf)
                {
                    bool showLabel = __instance.SelectedItem.IsEmptyStack ||
                        __instance.SelectedItem.BuyRestrictionCurrent >= ____traderAssortment.SelectedItemBuyRestrictionMax;

                    if (!showLabel)
                        ____buyRestrictionWarning.SetActive(false);
                }
            }
            catch { }
        }
    }
}

using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tarkin.tradermod.eft
{
    internal class Main
    {
        public static void Init()
        {
            Plugin.Log.LogInfo("tarkin.tradermod.eft Init");

            Harmony harmony = new Harmony("com.tarkin.tradermod");
            harmony.PatchAll();
        }
    }
}

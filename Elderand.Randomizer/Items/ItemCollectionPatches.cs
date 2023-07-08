using Elderand.Data;
using Elderand.NodeCanvas.SceneObjects;
using Elderand.SceneObjects;
using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace Elderand.Randomizer.Items
{
    // When a chest is opened, change it to a random item
    [HarmonyPatch(typeof(GetChestDrop), "OnExecute")]
    class ChestDrop_Open_Patch
    {
        public static void Postfix(GetChestDrop __instance)
        {
            string locationId = "Chest_" + __instance.agent.ChestData.name.Replace(" ", "_");
            Main.Log("Location id for chest: " + locationId);

            DropValueData drop = new(new List<ItemDropValue>()
            {
                new ItemDropValue()
                {
                    item = Main.ItemRandomizer.GetItemByName("Large Healing Potion"),
                    amount = 1
                }
            });
            __instance.outDrop.value = drop;
        }
    }

    // When a drop item is loaded at the beginning of a level, change it to a random item
    [HarmonyPatch(typeof(DropItem), "OnEnable")]
    class DropItem_Enable_Patch
    {
        public static void Prefix(DropItem __instance, bool ___wasSpawned)
        {
            if (__instance.Item == null || ___wasSpawned) return;

            string locationId = "Drop_" + __instance.Item.ItemName.ToString().Replace(" ", "_");
            Main.Log("Location id for drop item: " + locationId);

            __instance.SetItem(Main.ItemRandomizer.GetItemByName("Large Mana Potion"), 1);
        }
    }
}

﻿using Elderand.Data;
using Elderand.NodeCanvas.SceneObjects;
using Elderand.Player;
using Elderand.Randomizer.Extensions;
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
            string locationId = __instance.GetLocationId();
            ItemReward item = Main.ItemRandomizer.GetItemAtLocation(locationId);

            if (item != null)
            {
                // If there is a random item at this location
                DropValueData drop = new(new List<ItemDropValue>()
                {
                    new ItemDropValue()
                    {
                        item = Main.Data.GetItemObject(item.id),
                        amount = 1
                    }
                });
                __instance.outDrop.value = drop;
            }

            Main.Log($"Replacing '{locationId}' with '{item?.id ?? "Default"}'");
        }
    }

    // When a drop item is loaded at the beginning of a level or when spawned, change it to a random item
    [HarmonyPatch(typeof(DropItem), "CanBeSaved", MethodType.Getter)]
    class DropItem_Start_Patch
    {
        public static void Postfix(DropItem __instance, bool ___wasSpawned)
        {
            if (__instance.Item == null || ___wasSpawned) return;

            string locationId = __instance.GetLocationId();
            ItemReward item = Main.ItemRandomizer.GetItemAtLocation(locationId);

            if (item != null)
            {
                // If there is a random item at this location
                __instance.SetItem(Main.Data.GetItemObject(item.id), 1);
            }

            Main.Log($"Replacing '{locationId}' with '{item?.id ?? "Default"}'");

        }
    }

    // Make items always dissapear when collected, even if at max
    [HarmonyPatch(typeof(PlayerController), "AddItem")]
    class Player_Item_Patch
    {
        public static void Postfix(ref bool __result)
        {
            __result = true;
        }
    }
}

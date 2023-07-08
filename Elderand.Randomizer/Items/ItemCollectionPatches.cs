using Elderand.Data;
using Elderand.NodeCanvas.Player.Inventory;
using Elderand.NodeCanvas.SceneObjects;
using Elderand.SceneObjects;
using HarmonyLib;
using NodeCanvas.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace Elderand.Randomizer.Items
{
    //[HarmonyPatch(typeof(Chest), "ChestData", MethodType.Getter)]
    //class Chest_GetData_Patch
    //{
    //    public static void Postfix(Chest __instance, ref DropValueData __result)
    //    {
    //        Main.Log("Getting chest contents");
    //        __result = new DropValueData();

    //        //DropValueData drop = __instance.ChestData.DropValueData;
    //        //Main.Log("Bronze: " + drop.BronzeCoins);
    //        //foreach (ItemDropValue item in drop.DroppedItems)
    //        //{
    //        //    Main.Log("Item: " + item.Quantity);//.item.ItemName);
    //        //}
    //        //foreach (Component c in __instance.gameObject.GetComponents<Component>())
    //        //    Main.Log(c.ToString());
    //    }
    //}

    [HarmonyPatch(typeof(ChestData), "DropValueData", MethodType.Getter)]
    class ChestData_GetData_Patch
    {
        public static void Postfix(ChestData __instance, ref DropValueData __result) // Should probably move this to nodecanvas get chest data
        {
            string locationId = __instance.name.Replace(" ", "_");

            Main.LogWarning("Location id for chest: " + locationId);
            Main.ItemRandomizer.StoreItemAtLocation(locationId);
            if (Main.ItemRandomizer.CurrentRandomizedItem != null)
                __result = new DropValueData();
        }
    }

    [HarmonyPatch(typeof(DropValueData), MethodType.Constructor)]
    class DropData_Constructor_Patch
    {
        public static void Postfix(ref int ___bronzeCoins, ref int ___silverCoins, ref int ___goldCoins,
                                   ref List<ItemDropValue> ___droppedItems, ref List<Transform> ___droppedObjects)
        {
            ___bronzeCoins = 0;
            ___silverCoins = 0;
            ___goldCoins = 0;
            ___droppedItems = new()
            {
                new ItemDropValue()
                {
                    item = Main.ItemRandomizer.CurrentRandomizedItem,
                    amount = 1
                }
            };
            ___droppedObjects = new();
        }
    }

    [HarmonyPatch(typeof(DropItem), "OnEnable")]
    class DropItem_Enable_Patch
    {
        public static void Prefix(DropItem __instance)
        {
            string locationId = "Drop_" + __instance.Item.ItemName.ToString().Replace(" ", "_");

            if (__instance.Item != null)
            {
                Main.Log("Location id for drop item: " + locationId);
                __instance.SetItem(Main.ItemRandomizer.GetItemByName("Large Mana Potion"), 1);
            }
        }
    }

    [HarmonyPatch(typeof(Drop), "OnExecute")]
    class Drop_Spawn_Patch
    {
        public static void Prefix(BBParameter<IDrop> ___dropDataValue)
        {
            if (___dropDataValue.value is DropValueData && ___dropDataValue.value.DroppedItems.Count > 0)
            {
                Main.LogWarning("Item: " + ___dropDataValue.value.DroppedItems[0].item.ItemName);
            }
            else
            {
                Main.LogWarning("Item: none");
            }
        }
    }

    [HarmonyPatch(typeof(LoadDropItem), "OnExecute")]
    class LoadDrop_Spawn_Patch
    {
        // Apparently this is only on chests
        public static void Prefix(LoadDropItem __instance)
        {
            Main.LogError("Load drop items from chest: " + __instance.agent.SaveKey);
        }
    }

    [HarmonyPatch(typeof(Gameplay.GameplayReferences), "SpawnItem")]
    class GamePlayer_Spawn_Patch
    {
        public static void Prefix(ItemDropValue drop, Vector3 position)
        {
            Main.LogError($"Spawning item: {drop.item.ItemName} at {position}");
        }
    }
}

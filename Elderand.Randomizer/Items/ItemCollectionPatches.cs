using Elderand.Data;
using Elderand.SceneObjects;
using HarmonyLib;
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
        public static void Postfix(ChestData __instance, ref DropValueData __result)
        {
            string locationId = __instance.name.Replace(" ", "_");
            Main.LogWarning("Location id for chest: " + locationId);

            Main.ItemRandomizer.StoreItemAtLocation(locationId);
            if (Main.ItemRandomizer.CurrentRandomizedItem != null)
                __result = new DropValueData();
        }
    }

    [HarmonyPatch(typeof(DropValueData), MethodType.Constructor)]
    public class DropData_Constructor_Patch
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
}

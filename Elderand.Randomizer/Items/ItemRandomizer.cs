﻿using Elderand.AssetManagement;
using Elderand.Data;
using Elderand.SceneObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Elderand.Randomizer.Items
{
    public class ItemRandomizer : Manager
    {
        

        public override void LevelLoaded(string name)
        {
            Main.LogError("Sword: " + Main.Data.GetItemLocationData("Drop_Light-Sword").originalItem);
        }


        public ItemData GetRandomItem()
        {
            return Main.Data.GetItemByName(items[Random.RandomRangeInt(0, items.Length)]);
        }


        private Dictionary<string, string> tempItemMapping = new()
        {
            //{ "Drop_Light_Sword", "Gurom'karah" },
            //{ "Cave_38_Chest_01", "The Interior" },
            //{ "Drop_Fool's_Letter", "Lasher's Whip" },
            //{ "Cave_05_Chest_01", "HealthOrb" },
            //{ "Drop_Observer's_Notes_#1", "Crimson Jewel Buckler" },
            //{ "Cave_05_Chest_02", "Nyeth's Feather" },
            //{ "Cave_16_Chest_01", "Last Hook" },
        };

        string[] items = new string[]
        {
            "Gurom'karah",
            "The Interior",
            "Lasher's Whip",
            "HealthOrb",
            "Crimson Jewel Buckler",
            "Nyeth's Feather",
            "Last Hook",
        };

        // Save keys for stuff
        // Add more location/item ids
        // Add logic
        // Real shuffle algorithm
    }
}

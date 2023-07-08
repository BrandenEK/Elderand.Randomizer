﻿using Elderand.AssetManagement;
using Elderand.Data;
using Elderand.SceneObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Elderand.Randomizer.Items
{
    public class ItemRandomizer : Manager
    {
        private Dictionary<string, string> _mappedItems;

        public override void Initialize()
        {
            ShuffleItems();
        }

        public override void LevelLoaded(string name)
        {
            
        }


        //public ItemData GetRandomItem()
        //{
        //    return Main.Data.GetItemByName(items[Random.RandomRangeInt(0, items.Length)]);
        //}

        public ItemReward GetItemAtLocation(string locationId)
        {
            if (_mappedItems == null || !_mappedItems.ContainsKey(locationId))
                return null;
                //throw new System.Exception($"Location {locationId} doesn't exist");

            return Main.Data.GetItemRewardData(_mappedItems[locationId]);
        }

        private void ShuffleItems()
        {
            _mappedItems = new();
            List<string> locationIds = Main.Data.AllItemLocations;
            List<string> itemIds = new(Main.Data.AllItemRewards.Keys);

            //if (locationIds.Count != itemIds.Count)
            //    throw new System.Exception("Item list and location list are different sizes");

            System.Random rng = new();

            while (locationIds.Count > 0)
            {
                int itemIdx = itemIds.Count - 1;
                int locationIdx = rng.Next(locationIds.Count);

                Main.Log(locationIds[locationIdx] + ": " + itemIds[itemIdx]);
                _mappedItems.Add(locationIds[locationIdx], itemIds[itemIdx]);
                itemIds.RemoveAt(itemIdx);
                locationIds.RemoveAt(locationIdx);
            }

            Main.LogWarning("Shuffled all items and locations");
        }

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

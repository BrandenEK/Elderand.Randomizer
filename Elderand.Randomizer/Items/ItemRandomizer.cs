using Elderand.AssetManagement;
using Elderand.Data;
using Elderand.SceneObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Elderand.Randomizer.Items
{
    public class ItemRandomizer : Manager
    {
        private readonly Dictionary<string, ItemData> _itemObjects = new();

        public override void Initialize()
        {
            LoadObjects();
        }

        public override void LevelLoaded(string name)
        {
            foreach (DropItem drop in Object.FindObjectsOfType<DropItem>())
            {
                string locationId = "Drop_" + drop.Item.ItemName.ToString().Replace(" ", "_");
                Main.LogWarning("Location id for drop: " + locationId);

                StoreItemAtLocation(locationId);
                if (CurrentRandomizedItem != null)
                    drop.SetItem(CurrentRandomizedItem, 1);
            }
        }

        public void StoreItemAtLocation(string locationId)
        {
            if (tempItemMapping.TryGetValue(locationId, out string itemName))
            {
                CurrentRandomizedItem = GetItemByName(itemName);
            }
            else
            {
                CurrentRandomizedItem = null;
            }
        }

        public ItemData GetItemByName(string name)
        {
            if (_itemObjects.TryGetValue(name, out ItemData item))
                return item;

            throw new System.ArgumentException($"Item '{name}' does not exist");
        }

        private void LoadObjects()
        {
            var itemList = AssetManager.LoadAssetsFromKeySync<ItemData>("Item");
            _itemObjects.Clear();

            foreach (ItemData item in itemList.Keys)
            {
                //Main.Log(item.ItemName + ": " + item.GetType().ToString());
                string itemId;

                if (item.ItemName == null)
                {
                    if (item is OrbItemData orb)
                        itemId = orb.OrbType.ToString() + "Orb";
                    else
                        itemId = "Unknown";
                }
                else
                {
                    itemId = item.ItemName;
                }

                _itemObjects[itemId] = item;
            }
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

        public ItemData CurrentRandomizedItem { get; private set; }

        // Save keys for stuff
        // Add more location/item ids
        // Add logic
        // Real shuffle algorithm
    }
}

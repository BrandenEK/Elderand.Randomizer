using Elderand.AssetManagement;
using Elderand.Data;
using Elderand.Randomizer.Items;
using Elderand.SceneObjects;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Elderand.Randomizer
{
    public class DataLoader : Manager
    {
        private readonly string dataPath = Path.GetFullPath("Modding\\data\\Randomizer\\");

        public override void Initialize()
        {
            LoadItemLocations();
            LoadItemRewards();
            LoadObjects();
        }


        // Load all item location data from json


        private readonly Dictionary<string, ItemLocation> _itemLocations = new();

        private void LoadItemLocations()
        {
            ItemLocation[] allLocations = LoadJsonFromData<ItemLocation>("item-locations.json");
            foreach (ItemLocation location in allLocations)
            {
                _itemLocations.Add(location.id, location);
            }
            Main.Log($"Loaded {_itemLocations.Count} item locations!");
        }

        public List<string> AllItemLocations => new(_itemLocations.Keys);

        public ItemLocation GetItemLocationData(string locationId) => _itemLocations[locationId];


        // Load all item reward data from json


        private readonly Dictionary<string, ItemReward> _itemRewards = new();
        public Dictionary<string, ItemReward> AllItemRewards => _itemRewards;

        public ItemReward GetItemRewardData(string rewardId) => _itemRewards[rewardId];

        private void LoadItemRewards()
        {
            ItemReward[] allRewards = LoadJsonFromData<ItemReward>("item-rewards.json");
            foreach (ItemReward reward in allRewards)
            {
                _itemRewards.Add(reward.id, reward);
            }
            Main.Log($"Loaded {_itemRewards.Count} item rewards!");
        }


        // Load all item objects from assetmanager


        private readonly Dictionary<string, ItemData> _itemObjects = new();

        public ItemData GetItemObject(string id)
        {
            if (_itemObjects.TryGetValue(id, out ItemData item))
                return item;

            throw new System.ArgumentException($"Item '{id}' does not exist");
        }

        private void LoadObjects()
        {
            var itemList = AssetManager.LoadAssetsFromKeySync<ItemData>("Item");
            _itemObjects.Clear();

            foreach (ItemData item in itemList.Keys)
            {
                string itemId;
                if (item.ItemName == null)
                {
                    if (item is OrbItemData orb)
                        itemId = orb.OrbType.ToString() + "Orb";
                    else
                        throw new System.Exception("Item does not have a valid name");
                }
                else
                {
                    itemId = GetItemIdFromName(item.ItemName);
                    if (itemId == null)
                        continue;
                }

                _itemObjects[itemId] = item;
            }

            Main.LogWarning("Loaded objects");
            foreach (string itemId in _itemObjects.Keys)
                Main.LogWarning(itemId + ": " + _itemObjects[itemId].ItemName);

            string GetItemIdFromName(string itemName)
            {
                foreach (ItemReward item in _itemRewards.Values)
                {
                    if (item.name == itemName)
                        return item.id;
                }

                return null;
            }
        }

        // Helper functions

        private T[] LoadJsonFromData<T>(string fileName)
        {
            if (!File.Exists(dataPath + fileName))
                throw new System.Exception($"The data file {fileName} does not exist");

            string text = File.ReadAllText(dataPath + fileName);
            return JsonConvert.DeserializeObject<T[]>(text);
        }
    }
}

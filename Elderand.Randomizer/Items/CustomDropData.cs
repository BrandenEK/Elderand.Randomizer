using Elderand.Data;
using System.Collections.Generic;
using UnityEngine;

namespace Elderand.Randomizer.Items
{
    public class CustomDropData : IDrop
    {
        public int BronzeCoins => _bronzeCoins;
        public int SilverCoins => _silverCoins;
        public int GoldCoins => _goldCoins;

        public List<ItemDropValue> DroppedItems => _droppedItems;
        public List<Transform> DroppedObjects => _droppedObjects;

        public CustomDropData(int bronze, int silver, int gold, List<ItemDropValue> items)
        {
            _bronzeCoins = bronze;
            _silverCoins = silver;
            _goldCoins = gold;

            _droppedItems = items;
            _droppedObjects = new();
        }

        private readonly int _bronzeCoins;
        private readonly int _silverCoins;
        private readonly int _goldCoins;

        private readonly List<ItemDropValue> _droppedItems;
        private readonly List<Transform> _droppedObjects;
    }
}

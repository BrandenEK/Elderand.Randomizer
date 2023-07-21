using Elderand.Level;
using Elderand.NodeCanvas.SceneObjects;
using Elderand.SceneObjects;
using UnityEngine;

namespace Elderand.Randomizer.Extensions
{
    public static class ItemLocationExtensions
    {
        public static string GetLocationId(this GetChestDrop chest)
        {
            string[] parts = chest.agent.ChestData.name.Split(' ');
            return $"Chest_{parts[0]}-{parts[1]}-{parts[3]}";
        }

        public static string GetLocationId(this DropItem drop)
        {
            int index = 1;
            foreach (Transform child in drop.transform.parent)
            {
                if (child == drop.transform)
                    break;
                if (child.GetComponent<DropItem>())
                    index++;
            }

            string room = LevelManager.CurrentRoomInfo.PrefabName.Replace(' ', '-');
            return $"Drop_{room}-{index:00}";
        }
    }
}

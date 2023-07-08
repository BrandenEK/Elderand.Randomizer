using Elderand.NodeCanvas.SceneObjects;
using Elderand.SceneObjects;

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
            return "Drop_" + drop.Item.ItemName.ToString().Replace(' ', '-');
        }
    }
}

using Newtonsoft.Json;

namespace Elderand.Randomizer.Items
{
    public class ItemReward
    {
        [JsonProperty] public readonly string id;
        [JsonProperty] public readonly string name;

        [JsonProperty] public readonly int count;

        [JsonProperty] public readonly ItemRewardType type;

        public enum ItemRewardType
        {
            Standard
        }
    }
}

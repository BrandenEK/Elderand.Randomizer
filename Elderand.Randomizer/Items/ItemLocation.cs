using Newtonsoft.Json;

namespace Elderand.Randomizer.Items
{
    public class ItemLocation
    {
        [JsonProperty] public readonly string id;
        [JsonProperty] public readonly string name;

        [JsonProperty] public readonly ItemLocationType type;
        [JsonProperty] public readonly string originalItem;

        [JsonProperty] public readonly string logic;

        public enum ItemLocationType
        {
            Normal,
            Note,
            Inconvenient,
        }
    }
}

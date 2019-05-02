namespace ShopCommon.Models
{
    using Newtonsoft.Json;
    public class City
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

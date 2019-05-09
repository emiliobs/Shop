namespace ShopCommon.Models
{
    using Newtonsoft.Json;
    public class City
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}

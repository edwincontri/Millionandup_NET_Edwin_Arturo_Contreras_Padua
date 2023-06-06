using Newtonsoft.Json;

namespace Api_Properties.Models
{
    public class Property
    {
        [JsonProperty("pkProperty")]
        public string PkProperty { get; set; }

        [JsonProperty("propertyName")]
        public string PropertyName { get; set; }

        [JsonProperty("propertyAddress")]
        public string PropertyAddress { get; set; }

        [JsonProperty("propertyPrice")]
        public decimal PropertyPrice { get; set; }

        [JsonProperty("propertyCodeInternal")]
        public string PropertyCodeInternal { get; set; }

        [JsonProperty("propertyYear")]
        public int PropertyYear { get; set; }

        [JsonProperty("fkOwner")]
        public string FkOwner { get; set; }
    }
}

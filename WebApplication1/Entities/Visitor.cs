using Newtonsoft.Json;

namespace WebApplication1.Entities
{
    public class Visitor : BaseEntity
    {
        [JsonProperty(PropertyName = "id" , NullValueHandling = NullValueHandling.Ignore) ]
        public string Id { get; set; }
        [JsonProperty(PropertyName = "name", NullValueHandling = NullValueHandling.Ignore )]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "phoneNumber", NullValueHandling = NullValueHandling.Ignore)]
        public string PhoneNumber { get; set; }
        [JsonProperty(PropertyName = "address", NullValueHandling = NullValueHandling.Ignore)]
        public string Address { get; set; }
        [JsonProperty(PropertyName = "companyName", NullValueHandling = NullValueHandling.Ignore)]
        public string CompanyName { get; set; }
        [JsonProperty(PropertyName = "purpose", NullValueHandling = NullValueHandling.Ignore)]
        public string Purpose { get; set; }
        [JsonProperty(PropertyName = "entryTime", NullValueHandling = NullValueHandling.Ignore)]
        public string EntryTime { get; set; }
        [JsonProperty(PropertyName = "ExitTime", NullValueHandling = NullValueHandling.Ignore)]
        public string ExitTime { get; set; }
        [JsonProperty(PropertyName = "passStatus", NullValueHandling = NullValueHandling.Ignore)]
        public bool PassStatus { get; set; }
        [JsonProperty(PropertyName = "role", NullValueHandling = NullValueHandling.Ignore)]
        public string Role { get; set; }

       
    }
}

using Newtonsoft.Json;
using WebApplication1.Common;

namespace WebApplication1.Entities
{
    public class BaseEntity
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "uId", NullValueHandling = NullValueHandling.Ignore)]
        public string UId { get; set; }

        [JsonProperty(PropertyName = "documentType", NullValueHandling = NullValueHandling.Ignore)]
        public string DocumentType { get; set; }

        [JsonProperty(PropertyName = "version", NullValueHandling = NullValueHandling.Ignore)]
        public int Version { get; set; }

        [JsonProperty(PropertyName = "updatedBy", NullValueHandling = NullValueHandling.Ignore)]
        public string UpdatedBy { get; set; }

        [JsonProperty(PropertyName = "updatedOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty(PropertyName = "createBy", NullValueHandling = NullValueHandling.Ignore)]
        public string CreatedBy { get; set; }

        [JsonProperty(PropertyName = "createdOn", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime CreatedOn { get; set; }

        [JsonProperty(PropertyName = "active", NullValueHandling = NullValueHandling.Ignore)]
        public bool Active { get; set; }

        [JsonProperty(PropertyName = "archived", NullValueHandling = NullValueHandling.Ignore)]
        public bool Archived { get; set; }

        public void Intialize(bool isNew, string dType, string createdOrUpdatedBy, string createdOrUpdatedByName)
        {
            DocumentType = dType;
            Id = Guid.NewGuid().ToString();
            Active = true;
            Archived = false;

            if (isNew)
            {
                //Adding new Record
                Id = Guid.NewGuid().ToString();
                UId = Id;
                UId = Id;
                CreatedBy = createdOrUpdatedBy;
                CreatedOn = DateTime.UtcNow;
                Version = 1;
                UpdatedBy = createdOrUpdatedBy;
                UpdatedOn = CreatedOn;
            }
            else
            {
                //Updated the record
                UpdatedBy = createdOrUpdatedBy;
                UpdatedOn = DateTime.UtcNow;
                Version++;
            }

        }
    }
}

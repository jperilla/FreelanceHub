using Raven.Imports.Newtonsoft.Json;

namespace Web.Models
{
    public class CustomSearchSite
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Url { get; set; }
        public JobType JobType { get; set; }

        [JsonIgnore]
        public bool UserWantsToSearch { get; set; }
    }

    public enum JobType
    {
        All,
        Writing,
        Design,
        IT,
        Photography,
        Other
    }
}
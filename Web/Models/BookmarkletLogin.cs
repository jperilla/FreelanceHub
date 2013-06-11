using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Raven.Imports.Newtonsoft.Json;

namespace Web.Models
{
    public class BookmarkletLogin : Login
    {
        [JsonIgnore]
        public string Title { get; set; }

        [JsonIgnore]
        public string Description { get; set; }

        [JsonIgnore]
        public string URL { get; set; }
    }
}
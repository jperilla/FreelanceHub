
using System;
using System.Collections.Generic;

namespace Web.Models
{    
    
    public partial class Job
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Budget { get; set; }
        public string ShortDescription { get; set; }
        public Nullable<int> Status { get; set; }    
        public virtual JobStatus JobStatus { get; set; }

        public string DisplaySite
        {
            get
            {
                string shortUrl = null;

                // remove protocol
                if(URL.Contains("https://"))
                {
                    shortUrl = URL.Remove(0,8);
                }
                else if (URL.Contains("http://"))
                {
                    shortUrl = URL.Remove(0,7);
                }
                else
                {
                    shortUrl = URL;
                }

                // remove everything after /
                if (shortUrl != null && shortUrl.IndexOf('/') != -1)
                {
                    shortUrl = shortUrl.Substring(0, shortUrl.IndexOf('/'));
                }

                return shortUrl;
            }
        }

    }
}

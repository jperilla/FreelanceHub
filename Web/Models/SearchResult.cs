using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SearchResult : Bing.WebResult
    {
        public string DisplaySite
        {
            get 
            {
                string shortUrl = null;
                if(DisplayUrl.IndexOf('/') != -1)
                {
                    shortUrl = DisplayUrl.Substring(0, DisplayUrl.IndexOf('/'));
                }
                else
                {
                    shortUrl = DisplayUrl;
                }

                // TODO - match up short url with customsite and get display name

                return shortUrl;
            }
        }
    }
}
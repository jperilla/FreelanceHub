using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class SearchResult : Bing.WebResult
    {
        public SearchResult()
        {
        }

        public SearchResult(Bing.WebResult bingResult)
        {
            this.Description = bingResult.Description;
            this.DisplayUrl = bingResult.DisplayUrl;
            this.ID = bingResult.ID;
            this.Title = bingResult.Title;
            this.Url = bingResult.Url;
        }

        public static SearchResult BingResultToSearchResult(Bing.WebResult bingResult)
        { 
            return new SearchResult(bingResult);
        }

        public string DisplaySite
        {
            get 
            {
                string shortUrl = null;
                if(DisplayUrl != null && DisplayUrl.IndexOf('/') != -1)
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BingSearchAPI
{
    public class BingSearchWeb : BingSearch
    {
        public BingSearchWeb(IList<string> sitesToSearch)
            : base("https://api.datamarket.azure.com/Bing/SearchWeb")
        {
            SitesToSearch = sitesToSearch;
        }
    }
}

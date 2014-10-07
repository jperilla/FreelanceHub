using System.Collections.Generic;

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

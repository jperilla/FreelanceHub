using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bing;
using System.Net;

namespace BingSearchAPI
{
    public abstract class BingSearch
    {
        private string _rootUri;
        private Bing.BingSearchContainer _bingContainer;
        private const string _accountKey = "kbtZZTK/3L3wqQ8oxInANChDSPw9YKptwzA1SdA+pwo=";

        public IList<string> SitesToSearch { get; set; }

        public BingSearch(string root)
        {
            _rootUri = root;
            _bingContainer = new Bing.BingSearchContainer(new Uri(_rootUri));
            _bingContainer.Credentials = new NetworkCredential(_accountKey, _accountKey);
        }

        public IList<WebResult> Search(string query, int resultCount = 200, int pageNo = 0)
        {
            string queryWithSites = BuildQueryString(query);
            var webQuery = _bingContainer.Web(queryWithSites, null, null, null, null, null, null, null, resultCount, pageNo);
            //webQuery = webQuery.AddQueryOption("$top", 20);
            //webQuery = webQuery.AddQueryOption("$skip", 20);

            return(webQuery.Execute().ToList());
        }

        private string BuildQueryString(string query)
        {
            StringBuilder queryWithSites = new StringBuilder();
            
            //Build the query string with sites (if any), and query
            bool firstSite = true;
            foreach (var site in SitesToSearch)
            {
                if (!firstSite)
                {
                    queryWithSites.Append(" | ");
                }
                else
                {
                    firstSite = false;
                }

                queryWithSites.Append("site:" + site + " ");
            }

            queryWithSites.Append(query);
            return queryWithSites.ToString();
        }
    }
}

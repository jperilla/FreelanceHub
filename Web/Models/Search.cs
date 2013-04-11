using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bing;

namespace Web.Models
{
    public class Search
    {
        public string Query { get; set; }
        public IList<SearchResult> Results { get; set; }
        public int CurrentPage { get; set; }

        public Search()
        {
            CurrentPage = 1;
            Results = new List<SearchResult>();
        }
    }
}
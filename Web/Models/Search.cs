using System.Collections.Generic;

namespace Web.Models
{
    public class Search
    {
        public bool FirstVisit { get; set; }
        public string Query { get; set; }
        public IList<SearchResult> Results { get; set; }
        public int CurrentPage { get; set; }

        public Search()
        {
            CurrentPage = 1;
            Results = new List<SearchResult>();
            FirstVisit = true;
        }
    }
}
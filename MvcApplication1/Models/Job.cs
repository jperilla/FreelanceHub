
using System;
using System.Collections.Generic;

namespace Web.Model
{    
    
    public partial class Job
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string URL { get; set; }
        public string Budget { get; set; }
        public string ShortDescription { get; set; }
        public Nullable<int> Status { get; set; }
    
        public virtual JobStatus JobStatus { get; set; }
    }
}

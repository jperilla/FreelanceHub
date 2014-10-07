using System.Collections.Generic;

namespace Web.Models
{ 
    
    public partial class JobStatus
    {
        public JobStatus()
        {
            this.Jobs = new HashSet<Job>();
        }
    
        public int Id { get; set; }
        public string Status { get; set; }
    
        public virtual ICollection<Job> Jobs { get; set; }
    }
}

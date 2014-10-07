using System;

namespace Web.Models
{
    public class Log
    {
        public int Id { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public string StackTrace { get; set; }
    }
}
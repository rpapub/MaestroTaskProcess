using System;
using System.Collections.Generic;

namespace MaestroTaskProcessTemplate.Models
{
    public class Status
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorType { get; set; }
        public string Code { get; set; }
        public Dictionary<string, object> Details { get; set; }
        public string RetryAfter { get; set; }
        public string Source { get; set; }
        public string ExceptionType { get; set; }
        public DateTime Timestamp { get; set; }
        public int DurationMs { get; set; }
    }
}

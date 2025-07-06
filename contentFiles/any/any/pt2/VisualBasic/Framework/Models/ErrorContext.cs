using System;
using System.Collections.Generic;

namespace MaestroTaskProcessTemplate.Models
{
    /// <summary>
    /// Internal error envelope that accumulates diagnostic information
    /// before transforming into a Status object for external response.
    /// </summary>
    public class ErrorContext
    {
        public string ErrorType { get; set; }               // "retryable", "nonretryable", "unknown"
        public string ErrorMessage { get; set; }
        public string ExceptionType { get; set; }
        public Exception Exception { get; set; }
        public string Source { get; set; }                  // Maps directly to Status.source
        public string RetryAfterIsoUtc { get; set; }
        public string Code { get; set; }                    // Optional internal classification (e.g. "FailedIn:Processing")
        public Dictionary<string, object> Details { get; set; } = new();
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorType);
    }
}

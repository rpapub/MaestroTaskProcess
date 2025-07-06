using System;

namespace MaestroTaskProcessTemplate.Models
{
    public class ErrorContext
    {
        public string ErrorType { get; set; }           // "retryable", "nonretryable", "unknown"
        public string ErrorMessage { get; set; }
        public string ExceptionType { get; set; }
        public Exception Exception { get; set; }
        public string FailedPhase { get; set; }         // e.g. "Validate", "Processing"
        public string RetryAfterIsoUtc { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public bool HasError => !string.IsNullOrWhiteSpace(ErrorType);
    }
}

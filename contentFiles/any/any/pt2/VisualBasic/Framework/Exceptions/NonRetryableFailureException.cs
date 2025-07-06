using System;

namespace RpaPub.Devkit
{
    /// <summary>
    /// Represents a failure that is considered terminal or unrecoverable.
    /// Throw this exception for logic errors, input validation issues, or business rule violations.
    /// These exceptions are not retried and should be handled immediately or escalated.
    /// </summary>
    [Serializable]
    public class NonRetryableFailureException : Exception
    {
        public NonRetryableFailureException() { }

        public NonRetryableFailureException(string message) 
            : base(message) { }

        public NonRetryableFailureException(string message, Exception innerException) 
            : base(message, innerException) { }
    }
}

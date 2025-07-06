using System;

namespace RpaPub.Devkit
{
    /// <summary>
    /// Represents a failure that is considered transient or recoverable.
    /// Throw this exception when the error is expected to succeed upon retry,
    /// such as timeouts, rate limiting, or temporary unavailability of services or infrastructure.
    /// </summary>
    [Serializable]
    public class RetryableFailureException : Exception
    {
        public RetryableFailureException() { }

        public RetryableFailureException(string message)
            : base(message) { }

        public RetryableFailureException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}

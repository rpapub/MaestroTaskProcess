using System;
using Newtonsoft.Json.Linq;
using MaestroTaskProcessTemplate.Models;

namespace MaestroTaskProcessTemplate.Adapters
{
    public static class StatusAdapter
    {
        /// <summary>
        /// Updates a Status object based on ErrorContext.
        /// This method should be called in the teardown phase to finalize the status.
        /// </summary>
        public static void UpdateFromError(Status status, ErrorContext err)
        {
            if (err == null || !err.HasError) return;

            status.IsSuccess     = false;
            status.ErrorMessage  = err.ErrorMessage;
            status.ErrorType     = err.ErrorType;
            status.ExceptionType = err.ExceptionType;
            status.RetryAfter    = err.RetryAfterIsoUtc;
            status.Source        = err.Source;
            status.Code          = err.Code;
            status.Details       = err.Details;
            status.Timestamp     = DateTime.UtcNow;
        }

        /// <summary>
        /// Marks a Status object as successful.
        /// Should be used when no error was detected during execution.
        /// </summary>
        public static void MarkSuccess(Status status)
        {
            status.IsSuccess = true;
            status.Code = "Completed";
            status.Timestamp = DateTime.UtcNow;
        }

        /// <summary>
        /// Converts a Status instance into a JObject, suitable for JSON serialization or output argument binding.
        /// Null values are omitted for schema cleanliness.
        /// </summary>
        public static JObject ToJObject(Status status)
        {
            var obj = new JObject
            {
                ["isSuccess"] = status.IsSuccess,
                ["timestamp"] = status.Timestamp.ToString("o"),
                ["durationMs"] = status.DurationMs
            };

            void AddIfNotNull(string key, object value)
            {
                if (value != null)
                    obj[key] = JToken.FromObject(value);
            }

            AddIfNotNull("errorMessage", status.ErrorMessage);
            AddIfNotNull("errorType", status.ErrorType);
            AddIfNotNull("code", status.Code);
            AddIfNotNull("details", status.Details);
            AddIfNotNull("retryAfter", status.RetryAfter);
            AddIfNotNull("source", status.Source);
            AddIfNotNull("exceptionType", status.ExceptionType);

            return obj;
        }
    }
}

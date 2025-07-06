using System;
using System.Collections.Generic;
using MaestroTaskProcessTemplate.Models;

namespace MaestroTaskProcessTemplate.Builders
{
    public enum StatusErrorType
    {
        Retryable,
        NonRetryable,
        Unknown
    }

    public static class StatusBuilder
    {
        // Factory for fully typed Status object
        public static Status Create(
            bool isSuccess = false,
            string errorMessage = null,
            string errorType = null,
            string code = null,
            Dictionary<string, object> details = null,
            string retryAfter = null,
            string source = null,
            string exceptionType = null,
            int durationMs = 0)
        {
            ValidateErrorType(errorType);

            return new Status
            {
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage,
                ErrorType = errorType,
                Code = code,
                Details = details ?? new Dictionary<string, object>(),
                RetryAfter = retryAfter,
                Source = source,
                ExceptionType = exceptionType,
                Timestamp = DateTime.UtcNow,
                DurationMs = durationMs
            };
        }

        // Public method to turn a Status object into a Dictionary<string, object>
        public static Dictionary<string, object> ToDictionary(Status status)
        {
            var dict = new Dictionary<string, object>
            {
                ["isSuccess"] = status.IsSuccess,
                ["timestamp"] = status.Timestamp.ToString("o"),
                ["durationMs"] = status.DurationMs
            };

            void AddIfNotNull(string key, object value)
            {
                if (value != null) dict[key] = value;
            }

            AddIfNotNull("errorMessage", status.ErrorMessage);
            AddIfNotNull("errorType", status.ErrorType);
            AddIfNotNull("code", status.Code);
            AddIfNotNull("details", status.Details);
            AddIfNotNull("retryAfter", status.RetryAfter);
            AddIfNotNull("source", status.Source);
            AddIfNotNull("exceptionType", status.ExceptionType);

            return dict;
        }

        // Optionally normalize and enrich the 'details' dictionary
        public static Dictionary<string, object> NormalizeDetails(
            Dictionary<string, object> details,
            string workflow = null,
            string env = null,
            string version = null)
        {
            var normalized = details != null
                ? new Dictionary<string, object>(details)
                : new Dictionary<string, object>();

            void AddIfMissing(string key, string value)
            {
                if (!normalized.ContainsKey(key) && !string.IsNullOrWhiteSpace(value))
                    normalized[key] = value;
            }

            AddIfMissing("workflow", workflow);
            AddIfMissing("env", env);
            AddIfMissing("version", version);

            return normalized;
        }

        // Simple success creator
        public static Status SetSuccess(
            string code = null,
            Dictionary<string, object> details = null,
            int durationMs = 0)
        {
            return Create(
                isSuccess: true,
                code: code,
                details: details,
                durationMs: durationMs
            );
        }

        // Retryable failure from exception
        public static Status SetRetryable(Exception ex)
        {
            return SetRetryable(
                errorMessage: ex?.Message,
                exceptionType: ex?.GetType().ToString()
            );
        }

        public static Status SetRetryable(
            string errorMessage = null,
            string exceptionType = null)
        {
            return Create(
                isSuccess: false,
                errorMessage: errorMessage,
                errorType: StatusErrorType.Retryable.ToString().ToLowerInvariant(),
                exceptionType: exceptionType,
                retryAfter: DateTime.UtcNow.AddSeconds(5).ToString("o")
            );
        }

        public static Status SetNonRetryable(
            Exception ex) =>
            SetNonRetryable(ex?.Message, ex?.GetType().ToString());

        public static Status SetNonRetryable(
            string errorMessage = null,
            string exceptionType = null)
        {
            return Create(
                isSuccess: false,
                errorMessage: errorMessage,
                errorType: StatusErrorType.NonRetryable.ToString().ToLowerInvariant(),
                exceptionType: exceptionType
            );
        }

        private static void ValidateErrorType(string errorType)
        {
            if (!string.IsNullOrWhiteSpace(errorType) &&
                !Enum.TryParse(typeof(StatusErrorType), errorType, ignoreCase: true, out _))
            {
                throw new ArgumentException($"Invalid errorType: {errorType}. Must be one of: retryable, nonretryable, unknown.");
            }
        }
    }
}

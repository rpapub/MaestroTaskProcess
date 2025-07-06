using System;
using MaestroTaskProcessTemplate.Models;

namespace MaestroTaskProcessTemplate.Utils
{
    /// <summary>
    /// Provides safe access and utilities for the global ErrorContext variable.
    /// Handles initialization, casting, and error inspection.
    /// </summary>
    public static class GlobalErrorHelpers
    {
        /// <summary>
        /// Retrieves the global ErrorContext. Initializes it if missing or incorrectly typed.
        /// </summary>
        public static ErrorContext GetOrCreate()
        {
            if (GlobalVariablesNamespace.GlobalVariables.err is ErrorContext ctx)
                return ctx;

            ctx = new ErrorContext();
            GlobalVariablesNamespace.GlobalVariables.err = ctx;
            return ctx;
        }

        /// <summary>
        /// Returns true if the global ErrorContext exists and contains an error.
        /// </summary>
        public static bool HasError()
        {
            return GetOrCreate().HasError;
        }

        /// <summary>
        /// Sets error information on the global error context with minimal information.
        /// </summary>
        /// <param name="ex">The exception that occurred.</param>
        /// <param name="errorType">The error type: "retryable", "nonretryable", or "unknown".</param>
        public static void SetError(Exception ex, string errorType)
        {
            SetError(ex, errorType, null, null);
        }
/// <summary>
/// Populates the global ErrorContext with enriched error details for propagation to the final Status.
/// This centralizes error capture and classification.
/// </summary>
/// <param name="ex">The thrown <see cref="Exception"/> instance.</param>
/// <param name="errorType">One of: "retryable", "nonretryable", or "unknown".</param>
/// <param name="retryAfterIsoUtc">Optional ISO8601 retry-after time.</param>
/// <param name="source">Optional component or module that triggered the error.</param>
public static void SetError(
    Exception ex,
    string errorType = "nonretryable",
    string retryAfterIsoUtc = null,
    string source = null)
{
    var ctx = GetOrCreate();

    ctx.ErrorMessage   = ex?.Message;
    ctx.Exception      = ex;
    ctx.ExceptionType  = ex?.GetType().FullName;
    ctx.ErrorType      = errorType;
    ctx.RetryAfterIsoUtc = retryAfterIsoUtc;
    ctx.Source         = source;
}


        /// <summary>
        /// Sets the global ErrorContext variable to the specified instance.
        /// </summary>
        /// <param name="context">
        /// The <see cref="ErrorContext"/> object to assign to <c>GlobalVariables.err</c>.
        /// Must not be <c>null</c>.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="context"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// This method should be used instead of direct assignment to <c>GlobalVariables.err</c>
        /// to ensure type safety and consistent access patterns throughout the project.
        /// </remarks>
        public static void Set(ErrorContext context)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (context.GetType() != typeof(ErrorContext))
                throw new ArgumentException("Invalid type assigned to GlobalVariables.err. Must be ErrorContext.");

            GlobalVariablesNamespace.GlobalVariables.err = context;
        }

    }
}

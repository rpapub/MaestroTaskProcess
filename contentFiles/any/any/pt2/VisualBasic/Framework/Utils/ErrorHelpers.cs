using MaestroTaskProcessTemplate.Models;

namespace MaestroTaskProcessTemplate.Utils
{
    /// <summary>
    /// Provides convenience methods for accessing global variables like ErrorContext.
    /// </summary>
    public static class ErrorHelpers
    {
        /// <summary>
        /// Returns the globally scoped ErrorContext.
        /// You must ensure that GlobalVariables.err has been initialized (e.g., in Initialize).
        /// </summary>
        public static ErrorContext GetGlobal()
        {
            return (ErrorContext)GlobalVariablesNamespace.GlobalVariables.err;
        }
    }
}

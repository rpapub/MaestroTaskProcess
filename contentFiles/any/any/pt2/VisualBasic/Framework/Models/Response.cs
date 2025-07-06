using System.Collections.Generic;

namespace MaestroTaskProcessTemplate.Models
{
    /// <summary>
    /// Represents the internal response object, i.e. the business output.
    /// This is mapped to the Maestro `Response` output (JObject).
    /// </summary>
    public class Response
    {
        /// <summary>
        /// The business result. Use case-specific.
        /// </summary>
        public Dictionary<string, object> Data { get; set; } = new();
    }
}

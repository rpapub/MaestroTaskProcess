using System.Collections.Generic;
using Newtonsoft.Json;
using MaestroTaskProcessTemplate.Models;
using MaestroTaskProcessTemplate.Builders;

namespace MaestroTaskProcessTemplate.Extensions
{
    public static class StatusExtensions
    {
        /// <summary>
        /// Converts a typed Status object to a Dictionary<string, object> compatible with the Maestro schema.
        /// </summary>
        public static Dictionary<string, object> ToDictionary(this Status status)
        {
            return StatusBuilder.ToDictionary(status);
        }

        /// <summary>
        /// Serializes the Status object to a JSON string using default Newtonsoft settings.
        /// </summary>
        public static string ToJson(this Status status)
        {
            var dict = StatusBuilder.ToDictionary(status);
            return JsonConvert.SerializeObject(dict, Formatting.None);
        }
    }
}

using System;
using System.Collections.Generic;

namespace MaestroTaskProcessTemplate.Framework.Adapters
{
    public static class RequestDeserializer
    {
        /// <summary>
        /// Deserializes the generic payload object to a strongly typed T.
        /// </summary>
        public static T ExtractPayload<T>(object payload)
        {
            // NOTE: implement your logic here — for example:
            // return JsonConvert.DeserializeObject<T>(payload.ToString());
            throw new NotImplementedException("You must implement payload deserialization.");
        }

        /// <summary>
        /// Attempts to extract the payload dictionary from a flat request object.
        /// </summary>
        public static Dictionary<string, object> GetRawPayload(object request)
        {
            if (request is Dictionary<string, object> dict &&
                dict.TryGetValue("payload", out var payloadObj) &&
                payloadObj is Dictionary<string, object> payloadDict)
            {
                return payloadDict;
            }

            throw new ArgumentException("Missing or invalid 'payload' field in request.");
        }

        /// <summary>
        /// Returns payload as a string if it is a single-string value.
        /// </summary>
        public static string TryExtractSingleStringPayload(object request)
        {
            if (request is Dictionary<string, object> dict &&
                dict.TryGetValue("payload", out var payloadObj) &&
                payloadObj is string payloadString)
            {
                return payloadString;
            }

            return null;
        }
    }
}

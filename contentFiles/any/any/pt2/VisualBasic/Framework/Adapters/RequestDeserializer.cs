using System;
using System.Collections.Generic;

namespace MaestroTaskProcessTemplate.Framework.Adapters
{
    public static class RequestDeserializer
    {
        // This method is a stub. Developers must implement their own payload mapping here.
        public static T ExtractPayload<T>(Dictionary<string, object> requestPayload)
        {
            // Example: use Newtonsoft or manual mapping
            throw new NotImplementedException("You must implement payload deserialization.");
        }

        // Optional helper if you want a raw payload accessor
        public static Dictionary<string, object> GetRawPayload(object requestArgument)
        {
            if (requestArgument is Dictionary<string, object> root &&
                root.TryGetValue("request", out var reqObj) &&
                reqObj is Dictionary<string, object> reqDict &&
                reqDict.TryGetValue("payload", out var payloadObj) &&
                payloadObj is Dictionary<string, object> payloadDict)
            {
                return payloadDict;
            }

            throw new ArgumentException("Invalid request structure.");
        }
        public static string TryExtractSingleStringPayload(object request)
        {
            if (request is Dictionary<string, object> root &&
                root.TryGetValue("request", out var requestObj) &&
                requestObj is Dictionary<string, object> reqDict &&
                reqDict.TryGetValue("payload", out var payloadObj) &&
                payloadObj is string payloadString)
            {
                return payloadString;
            }

            return null; // not a single-string payload
        }
    }
}

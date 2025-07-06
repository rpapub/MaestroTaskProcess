using System;
using System.Collections.Generic;

namespace MaestroTaskProcessTemplate.Models
{
    /// <summary>
    /// Represents the root-level Maestro task request.
    /// Corresponds to `request-schema.v1.json`
    /// </summary>
    public class Request
    {
        public string Version { get; set; }

        public RequestEnvelope RequestBody { get; set; }

        public class RequestEnvelope
        {
            public string CorrelationId { get; set; }

            public string RequestId { get; set; }

            public DateTime? Timestamp { get; set; }

            public Dictionary<string, object> LogContext { get; set; }

            /// <summary>
            /// The raw input payload.
            /// The structure of this object is application-specific.
            /// </summary>
            public Dictionary<string, object> Payload { get; set; }
        }
    }
}

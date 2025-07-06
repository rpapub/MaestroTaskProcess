using System.Collections.Generic;

namespace MaestroTaskProcessTemplate.Models
{
    public class Response
    {
        public object Result { get; set; } // Or Dictionary<string, object>

        public Status Status { get; set; }
    }
}

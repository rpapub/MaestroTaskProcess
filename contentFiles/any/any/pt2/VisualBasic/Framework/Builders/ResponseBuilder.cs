using System.Collections.Generic;
using MaestroTaskProcessTemplate.Models;

namespace MaestroTaskProcessTemplate.Builders
{
    public static class ResponseBuilder
    {
        public static Response FromDictionary(Dictionary<string, object> data)
        {
            return new Response { Data = data ?? new Dictionary<string, object>() };
        }

        public static Response Empty()
        {
            return new Response { Data = new Dictionary<string, object>() };
        }
    }
}

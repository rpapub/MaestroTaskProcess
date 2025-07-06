using Newtonsoft.Json.Linq;
using MaestroTaskProcessTemplate.Models;

namespace MaestroTaskProcessTemplate.Adapters
{
    public static class ResponseAdapter
    {
        public static JObject ToJObject(Response response)
        {
            return response?.Data != null
                ? JObject.FromObject(response.Data)
                : new JObject();
        }
    }
}

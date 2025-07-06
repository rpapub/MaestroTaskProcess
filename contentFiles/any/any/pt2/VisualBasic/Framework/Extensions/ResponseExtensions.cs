using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MaestroTaskProcessTemplate.Models;
using MaestroTaskProcessTemplate.Adapters;

namespace MaestroTaskProcessTemplate.Extensions
{
    public static class ResponseExtensions
    {
        public static JObject ToJObject(this Response response)
            => ResponseAdapter.ToJObject(response);

        public static string ToJson(this Response response)
            => JsonConvert.SerializeObject(response.ToJObject(), Formatting.None);
    }
}

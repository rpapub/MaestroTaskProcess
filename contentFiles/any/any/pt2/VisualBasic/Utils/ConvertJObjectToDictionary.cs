using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace MaestroTaskProcessTemplate.RnD
{
    public static class Scratch_ConvertJObjectToDictionary
    {
        public static Dictionary<string, object?> Convert(JObject jsonObj)
        {
            var result = new Dictionary<string, object?>();

            foreach (var prop in jsonObj.Properties())
            {
                var key = prop.Name;
                var value = prop.Value;

                switch (value.Type)
                {
                    case JTokenType.Object:
                        result[key] = Convert((JObject)value);
                        break;

                    case JTokenType.Array:
                        var list = new List<object?>();
                        foreach (var item in (JArray)value)
                        {
                            if (item.Type == JTokenType.Object)
                                list.Add(Convert((JObject)item));
                            else
                                list.Add(((JValue)item).Value);
                        }
                        result[key] = list;
                        break;

                    default:
                        result[key] = ((JValue)value).Value;
                        break;
                }
            }

            return result;
        }
    }
}

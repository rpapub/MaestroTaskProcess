using System;
using System.Collections.Generic;

namespace MaestroTaskProcessTemplate.Utils
{
    public class LogHelper
    {
        public static string ToLogString(Dictionary<string, object?> dict, bool useNewLine = false, int indent = 0)
        {
            var parts = new List<string>();
            string indentStr = new string(' ', indent);
            string sep = useNewLine ? Environment.NewLine : ", ";
    
            foreach (var kvp in dict)
            {
                string entry;
                if (kvp.Value is Dictionary<string, object?> nestedDict)
                {
                    string nested = ToLogString(nestedDict, useNewLine, indent + 2);
                    entry = $"{indentStr}{kvp.Key}: {{{nested}}}";
                }
                else if (kvp.Value is List<object?> list)
                {
                    var listItems = new List<string>();
                    foreach (var item in list)
                    {
                        if (item is Dictionary<string, object?> itemDict)
                            listItems.Add("{" + ToLogString(itemDict, useNewLine, indent + 4) + "}");
                        else
                            listItems.Add(item?.ToString() ?? "null");
                    }
                    entry = $"{indentStr}{kvp.Key}: [{string.Join(", ", listItems)}]";
                }
                else
                {
                    entry = $"{indentStr}{kvp.Key}: {kvp.Value}";
                }
    
                parts.Add(entry);
            }
    
            return string.Join(sep, parts);
        }
    }
}
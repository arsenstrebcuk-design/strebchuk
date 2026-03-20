using System.Collections.Generic;
using System.Text;
using DataConverterApp.Interfaces;

namespace DataConverterApp.Models
{
    public class XmlFormat : IDataFormat
    {
        public string FormatName => "XML";

        public List<Dictionary<string, string>> Parse(string input)
        {
            return new List<Dictionary<string, string>>();
        }

        public string Serialize(List<Dictionary<string, string>> data)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<root>");
            foreach (var row in data)
            {
                sb.AppendLine("  <item>");
                foreach (var kvp in row)
                {
                    string tag = kvp.Key.Replace(" ", "_");
                    sb.AppendLine($"    <{tag}>{kvp.Value}</{tag}>");
                }
                sb.AppendLine("  </item>");
            }
            sb.AppendLine("</root>");
            return sb.ToString();
        }

        public string Convert(List<Dictionary<string, string>> data)
        {
            return Serialize(data);
        }
    }
}
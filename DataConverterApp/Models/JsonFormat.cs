using System.Collections.Generic;
using System.Text.Json;
using DataConverterApp.Interfaces;

namespace DataConverterApp.Models
{
    public class JsonFormat : IDataFormat
    {
        public string FormatName => "JSON";

        public string Serialize(List<Dictionary<string, string>> data)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(data, options);
        }

        public List<Dictionary<string, string>> Parse(string input)
        {
            return new List<Dictionary<string, string>>();
        }

        public string Convert(List<Dictionary<string, string>> data)
        {
            return Serialize(data); 
        }
    }
}
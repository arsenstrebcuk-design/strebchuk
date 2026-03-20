using CsvHelper;
using DataConverterApp.Interfaces;
using System.Formats.Asn1;
using System.Globalization;

namespace DataConverterApp.Models;

public class CsvFormat : IDataFormat
{
    public string FormatName => "CSV";

    public List<Dictionary<string, string>> Parse(string content)
    {
        using var reader = new StringReader(content);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<dynamic>();
        var result = new List<Dictionary<string, string>>();

        foreach (var record in records)
        {
            var dict = new Dictionary<string, string>();
            foreach (var property in (IDictionary<string, object>)record)
            {
                dict.Add(property.Key, property.Value?.ToString() ?? "");
            }
            result.Add(dict);
        }
        return result;
    }

    public string Serialize(List<Dictionary<string, string>> data) => throw new NotImplementedException();
}   
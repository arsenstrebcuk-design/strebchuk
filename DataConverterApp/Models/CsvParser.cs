using System.Collections.Generic;

public class CsvParser
{
    public static List<Dictionary<string, string>> Parse(string content)
    {
        var result = new List<Dictionary<string, string>>();
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        if (lines.Length < 2) return result;

        var headers = lines[0].Split(',');

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].Split(',');
            var dict = new Dictionary<string, string>();
            for (int j = 0; j < headers.Length; j++)
            {
                dict[headers[j].Trim()] = values[j].Trim();
            }
            result.Add(dict);
        }
        return result;
    }
}   
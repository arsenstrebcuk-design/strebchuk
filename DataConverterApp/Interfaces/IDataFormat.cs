namespace DataConverterApp.Interfaces;

public interface IDataFormat
{
    string FormatName { get; }
    List<Dictionary<string, string>> Parse(string content);

    string Serialize(List<Dictionary<string, string>> data);
}
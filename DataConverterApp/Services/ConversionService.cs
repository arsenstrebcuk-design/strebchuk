using DataConverterApp.Interfaces;
using DataConverterApp.Models;
using System.Collections.Generic;

namespace DataConverterApp.Services
{
    public class ConversionService
    {
        public string Convert(string input, string formatType)
        {
            var parsed = CsvParser.Parse(input);

            IDataFormat format = FormatFactory.CreateFormat(formatType);

            return format.Serialize(parsed);
        }
    }
}
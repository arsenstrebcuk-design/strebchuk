using System;
using DataConverterApp.Interfaces;

namespace DataConverterApp.Models
{
    public static class FormatFactory
    {
        public static IDataFormat CreateFormat(string format)
        {
            switch (format.ToUpper())
            {
                case "JSON":
                    return new JsonFormat();

                case "XML":
                    return new XmlFormat();

                default:
                    throw new Exception("Unsupported format");
            }
        }
    }
}
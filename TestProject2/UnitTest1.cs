using DataConverterApp.Models;
using DataConverterApp.Interfaces;
using DataConverterApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Assert = Xunit.Assert;

namespace TestProject2
{
    public class UnitTest1
    {
        private List<Dictionary<string, string>> GetSampleData()
        {
            return new List<Dictionary<string, string>>
            {
                new Dictionary<string, string>
                {
                    { "name", "Ivan" },
                    { "age", "20" },
                    { "city", "Lviv" }
                },
                new Dictionary<string, string>
                {
                    { "name", "Olena" },
                    { "age", "21" },
                    { "city", "Kyiv" }
                }
            };
        }

        [Fact]
        public void CsvParser_Parse_ReturnsCorrectRowCount()
        {
            string csv = "name,age,city\nIvan,20,Lviv\nOlena,21,Kyiv";
            var result = CsvParser.Parse(csv);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public void CsvParser_Parse_ReturnsCorrectFirstName()
        {
            string csv = "name,age,city\nIvan,20,Lviv";
            var result = CsvParser.Parse(csv);
            Assert.Equal("Ivan", result[0]["name"]);
        }

        [Fact]
        public void CsvParser_Parse_ReturnsCorrectAge()
        {
            string csv = "name,age\nIvan,20";
            var result = CsvParser.Parse(csv);
            Assert.Equal("20", result[0]["age"]);
        }

        [Fact]
        public void CsvParser_Parse_ReturnsCorrectCity()
        {
            string csv = "name,city\nIvan,Lviv";
            var result = CsvParser.Parse(csv);
            Assert.Equal("Lviv", result[0]["city"]);
        }

        [Fact]
        public void CsvParser_Parse_HeaderOnly_ReturnsZeroRows()
        {
            string csv = "name,age,city";
            var result = CsvParser.Parse(csv);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public void CsvParser_Parse_SingleColumn_ReturnsCorrectValues()
        {
            string csv = "name\nIvan\nOlena";
            var result = CsvParser.Parse(csv);
            Assert.Equal(2, result.Count);
            Assert.Equal("Ivan", result[0]["name"]);
            Assert.Equal("Olena", result[1]["name"]);
        }

        [Fact]
        public void JsonFormat_Serialize_ReturnsString()
        {
            var formatter = new JsonFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void JsonFormat_Serialize_ContainsIvan()
        {
            var formatter = new JsonFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("Ivan", result);
        }

        [Fact]
        public void JsonFormat_Serialize_ContainsOlena()
        {
            var formatter = new JsonFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("Olena", result);
        }

        [Fact]
        public void JsonFormat_Serialize_ContainsAgeField()
        {
            var formatter = new JsonFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("age", result);
        }

        [Fact]
        public void JsonFormat_Serialize_ContainsCityField()
        {
            var formatter = new JsonFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("city", result);
        }

        [Fact]
        public void JsonFormat_Serialize_EmptyList_ReturnsJson()
        {
            var formatter = new JsonFormat();
            string result = formatter.Serialize(new List<Dictionary<string, string>>());
            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void XmlFormat_Serialize_ReturnsString()
        {
            var formatter = new XmlFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.False(string.IsNullOrWhiteSpace(result));
        }

        [Fact]
        public void XmlFormat_Serialize_ContainsRoot()
        {
            var formatter = new XmlFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("root", result.ToLower());
        }

        [Fact]
        public void XmlFormat_Serialize_ContainsItem()
        {
            var formatter = new XmlFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("item", result.ToLower());
        }

        [Fact]
        public void XmlFormat_Serialize_ContainsIvan()
        {
            var formatter = new XmlFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("Ivan", result);
        }

        [Fact]
        public void XmlFormat_Serialize_ContainsOlena()
        {
            var formatter = new XmlFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("Olena", result);
        }

        [Fact]
        public void XmlFormat_Serialize_ContainsCity()
        {
            var formatter = new XmlFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("city", result);
        }

        [Fact]
        public void FormatFactory_CreateFormat_Json_ReturnsJsonFormat()
        {
            IDataFormat format = FormatFactory.CreateFormat("JSON");
            Assert.IsType<JsonFormat>(format);
        }

        [Fact]
        public void FormatFactory_CreateFormat_Xml_ReturnsXmlFormat()
        {
            IDataFormat format = FormatFactory.CreateFormat("XML");
            Assert.IsType<XmlFormat>(format);
        }

        [Fact]
        public void FormatFactory_CreateFormat_json_ReturnsJsonFormat()
        {
            IDataFormat format = FormatFactory.CreateFormat("json");
            Assert.IsType<JsonFormat>(format);
        }

        [Fact]
        public void FormatFactory_CreateFormat_xml_ReturnsXmlFormat()
        {
            IDataFormat format = FormatFactory.CreateFormat("xml");
            Assert.IsType<XmlFormat>(format);
        }

        [Fact]
        public void FormatFactory_CreateFormat_Invalid_ThrowsException()
        {
            Assert.Throws<Exception>(() => FormatFactory.CreateFormat("TXT"));
        }

        [Fact]
        public void HistoryService_Add_IncreasesCount()
        {
            var service = new HistoryService();
            service.Add("sample.csv", "JSON", 2);
            Assert.Equal(1, service.Items.Count());
        }

        [Fact]
        public void HistoryService_Add_StoresFileName()
        {
            var service = new HistoryService();
            service.Add("sample.csv", "JSON", 2);
            Assert.Equal("sample.csv", service.Items.First().FileName);
        }

        [Fact]
        public void HistoryService_Add_StoresFormat()
        {
            var service = new HistoryService();
            service.Add("sample.csv", "XML", 2);
            Assert.Equal("XML", service.Items.First().Format);
        }

        [Fact]
        public void HistoryService_Add_StoresRows()
        {
            var service = new HistoryService();
            service.Add("sample.csv", "JSON", 5);
            Assert.Equal(5, service.Items.First().Rows);
        }

        [Fact]
        public void HistoryService_Clear_RemovesAllItems()
        {
            var service = new HistoryService();
            service.Add("a.csv", "JSON", 1);
            service.Add("b.csv", "XML", 2);
            service.Clear();
            Assert.Equal(0, service.Items.Count());
        }

        [Fact]
        public void HistoryService_Add_SetsDate()
        {
            var service = new HistoryService();
            service.Add("sample.csv", "JSON", 1);
            Assert.NotEqual(default(DateTime), service.Items.First().Date);
        }

        [Fact]
        public void JsonAndXml_ReturnDifferentResults()
        {
            var data = GetSampleData();
            string json = new JsonFormat().Serialize(data);
            string xml = new XmlFormat().Serialize(data);
            Assert.NotEqual(json, xml);
        }

        [Fact]
        public void Factory_Json_CanSerialize()
        {
            IDataFormat format = FormatFactory.CreateFormat("JSON");
            string result = format.Serialize(GetSampleData());
            Assert.Contains("Ivan", result);
        }

        [Fact]
        public void Factory_Xml_CanSerialize()
        {
            IDataFormat format = FormatFactory.CreateFormat("XML");
            string result = format.Serialize(GetSampleData());
            Assert.Contains("Ivan", result);
        }

        [Fact]
        public void CsvParser_ThenJson_WorksTogether()
        {
            string csv = "name,age,city\nPetro,19,Odesa";
            var parsed = CsvParser.Parse(csv);
            string json = new JsonFormat().Serialize(parsed);
            Assert.Contains("Petro", json);
        }

        [Fact]
        public void CsvParser_ThenXml_WorksTogether()
        {
            string csv = "name,age,city\nPetro,19,Odesa";
            var parsed = CsvParser.Parse(csv);
            string xml = new XmlFormat().Serialize(parsed);
            Assert.Contains("Petro", xml);
        }

        [Fact]
        public void ParsedData_HasThreeKeys()
        {
            string csv = "name,age,city\nPetro,19,Odesa";
            var parsed = CsvParser.Parse(csv);
            Assert.Equal(3, parsed[0].Keys.Count);
        }

        [Fact]
        public void CsvParser_Parse_ThreeRows_ReturnsThreeRows()
        {
            string csv = "name\nA\nB\nC";
            var result = CsvParser.Parse(csv);
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public void CsvParser_Parse_SecondRowCorrect()
        {
            string csv = "name,age\nIvan,20\nOlena,21";
            var result = CsvParser.Parse(csv);
            Assert.Equal("Olena", result[1]["name"]);
            Assert.Equal("21", result[1]["age"]);
        }

        [Fact]
        public void HistoryService_Add_TwoItems_CountIsTwo()
        {
            var service = new HistoryService();
            service.Add("a.csv", "JSON", 1);
            service.Add("b.csv", "XML", 2);
            Assert.Equal(2, service.Items.Count());
        }

        [Fact]
        public void JsonFormat_ResultStartsWithBracket()
        {
            var formatter = new JsonFormat();
            string result = formatter.Serialize(GetSampleData()).Trim();
            Assert.Equal('[', result[0]);
        }

        [Fact]
        public void XmlFormat_ResultContainsNameTag()
        {
            var formatter = new XmlFormat();
            string result = formatter.Serialize(GetSampleData());
            Assert.Contains("name", result);
        }

        [Fact]
        public void CsvParser_FirstRowContainsNameKey()
        {
            string csv = "name,age\nIvan,20";
            var result = CsvParser.Parse(csv);
            Assert.True(result[0].ContainsKey("name"));
        }
    }
}
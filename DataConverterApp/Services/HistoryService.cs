using System;
using System.Collections.Generic;
using System.Linq;

namespace DataConverterApp.Services
{
    public class HistoryItem
    {
        public string FileName { get; set; } = string.Empty;
        public string Format { get; set; } = string.Empty;
        public int Rows { get; set; }
        public DateTime Date { get; set; }
    }

    public class HistoryService
    {
        private readonly List<HistoryItem> _items = new();

        public IEnumerable<HistoryItem> Items => _items.OrderByDescending(x => x.Date);

        public void Add(string fileName, string format, int rows)
        {
            _items.Add(new HistoryItem
            {
                FileName = fileName,
                Format = format,
                Rows = rows,
                Date = DateTime.Now
            });
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
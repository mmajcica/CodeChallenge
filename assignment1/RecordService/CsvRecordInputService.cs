﻿using CsvHelper;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RecordService
{
    public class CsvRecordInputService : IRecordInputService
    {
        private readonly string filePath;

        public CsvRecordInputService(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException($"'{nameof(filePath)}' cannot be null or empty", nameof(filePath));
            }

            this.filePath = filePath;
        }

        public IEnumerable<Record> GetRecords()
        {
            using StreamReader reader = new StreamReader(filePath);
            using CsvReader csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            csv.Configuration.PrepareHeaderForMatch = (header, index) => header.Trim();
            csv.Configuration.TrimOptions = TrimOptions.Trim;

            return csv.GetRecords<Record>().ToList();

        }
    }
}

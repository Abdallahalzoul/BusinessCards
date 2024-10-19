using CsvHelper;
using Domain.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Services.Service
{
    public class ExportService : IExportService
    {
        public (byte[], string) ExportToCSV(List<BusinessCard> businessCards)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(businessCards);
                writer.Flush();
                var fileName = "BusinessCards.csv";
                return (memoryStream.ToArray(), fileName);
            }
        }

        public (byte[], string) ExportToXML(List<BusinessCard> businessCards)
        {
            using (var memoryStream = new MemoryStream())
            using (var writer = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                var serializer = new XmlSerializer(typeof(List<BusinessCard>));
                serializer.Serialize(writer, businessCards);
                writer.Flush();
                var fileName = "BusinessCards.xml";
                return (memoryStream.ToArray(), fileName);
            }
        }
    }
}

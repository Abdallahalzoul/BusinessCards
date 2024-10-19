using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IExportService
    {
        (byte[], string) ExportToCSV(List<BusinessCard> cards);
        (byte[], string) ExportToXML(List<BusinessCard> cards);
    }
}

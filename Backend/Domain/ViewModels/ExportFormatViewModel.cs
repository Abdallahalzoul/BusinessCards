using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class ExportFormatViewModel
    {
        public string Format { get; set; }
    }
    public class ImportFormatViewModel
    {
        public IFormFile? File { get; set; }
    }

}

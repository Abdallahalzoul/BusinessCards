using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IFileService
    {
        Task<string?> GetBase64String(IFormFile file);
        Task<List<BusinessCard>> ParseCsv(IFormFile file);
        Task<List<BusinessCard>> ParseXml(IFormFile file);
        Task<List<BusinessCard>> ParseQrCode(IFormFile file);
    }
}

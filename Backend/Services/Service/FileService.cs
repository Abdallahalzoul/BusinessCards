using CsvHelper;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ZXing.Common;
using ZXing;

namespace Services.Service
{
    public class FileService : IFileService
    {
        public async Task<string?> GetBase64String(IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return null;
        }

        public async Task<List<BusinessCard>> ParseCsv(IFormFile file)
        {
            using (var streamReader = new StreamReader(file.OpenReadStream()))
            using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
            {
                var records = csvReader.GetRecords<BusinessCard>().ToList();
                return await Task.FromResult(records);
            }
        }

        public async Task<List<BusinessCard>> ParseXml(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                var serializer = new XmlSerializer(typeof(List<BusinessCard>));
                return (List<BusinessCard>)serializer.Deserialize(stream);
            }
        }

        public async Task<List<BusinessCard>> ParseQrCode(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var bitmap = new Bitmap(memoryStream);
                var reader = new BarcodeReader
                {
                    AutoRotate = true,
                    TryInverted = true,
                    Options = new DecodingOptions
                    {
                        PossibleFormats = new List<BarcodeFormat> { BarcodeFormat.QR_CODE }
                    }
                };
                var result = reader.Decode(bitmap);
                return result != null ? JsonConvert.DeserializeObject<List<BusinessCard>>(result.Text) : new List<BusinessCard>();
            }
        }
    }
}

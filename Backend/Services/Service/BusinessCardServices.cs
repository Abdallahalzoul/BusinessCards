using CsvHelper;
using Domain.Models;
using global::Services.Interface;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Repository.Repository;
using System.Drawing;
using System.Formats.Asn1;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;
namespace Services.Service
{


    public class BusinessCardServices : IBusinessCard<BusinessCard>
    {
        private readonly IRepositorySQL<BusinessCard> _repositorySQL;
        private readonly IExportService _exportService;
        private readonly IFileService _fileService;
        public BusinessCardServices(IRepositorySQL<BusinessCard> repositorySQL, IExportService exportService, IFileService fileService)
        {
            _repositorySQL = repositorySQL;
            _exportService = exportService;
            _fileService = fileService;
        }

        public async Task<int> CreateBusinessCardAsync(BusinessCard card)
        {
            var parameters = new
            {
                Name = card.Name,
                Gender = card.Gender,
                DateOfBirth = card.DateOfBirth.ToString("dd/MM/yyyy"),
                Email = card.Email,
                Phone = card.Phone,
                Address = card.Address,
                PhotoBase64 = card.PhotoBase64
            };
            var returnData = await _repositorySQL.IntExecCommand("CreateBusinessCard", parameters);
            return returnData;
        }

        public async Task<List<BusinessCard>> GetBusinessCardsAsync()
        {
            return await _repositorySQL.ListData("GetBusinessCards", null);
        }

        public async Task<BusinessCard> GetBusinessCardByIdAsync(int id)
        {
            var parameters = new { Id = id };
            return await _repositorySQL.FindExecCommand("GetBusinessCardById", parameters);
        }

        public async Task<int> DeleteBusinessCardAsync(int id)
        {
            var parameters = new { Id = id };
            return await _repositorySQL.IntExecCommand("SoftDeleteBusinessCard", parameters);
        }

        public async Task<List<BusinessCard>> FilterBusinessCardsAsync(string name, string email, string phone, string gender)
        {
            var parameters = new
            {
                Name = name,
                Email = email,
                Phone = phone,
                Gender = gender
            };

            return await _repositorySQL.ListData("FilterBusinessCards", parameters);
        }

        public async Task<List<BusinessCard>> ImportBusinessCardsAsync(List<BusinessCard> cards)
        {
            foreach (var card in cards)
            {
                await CreateBusinessCardAsync(card);
            }
            return cards;
        }

        public async Task<(byte[], string)> ExportBusinessCardsAsync(string format)
        {
            var businessCards = await GetBusinessCardsAsync();
            if (businessCards == null || businessCards.Count == 0)
                return (null, null);

            if (format.Equals("csv", StringComparison.OrdinalIgnoreCase))
            {
                return _exportService.ExportToCSV(businessCards);
            }
            else if (format.Equals("xml", StringComparison.OrdinalIgnoreCase))
            {
                return _exportService.ExportToXML(businessCards);
            }

            return (null, null);
        }
        public async Task<string?> GetBase64String(IFormFile photoFile)
        {
            return await _fileService.GetBase64String(photoFile);
        }


        public async Task ImportBusinessCardsAsync(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            var businessCards = new List<BusinessCard>();
            switch (fileExtension)
            {
                case ".csv":
                    businessCards = await _fileService.ParseCsv(file);
                    break;

                case ".xml":
                    businessCards = await _fileService.ParseXml(file);
                    break;

                case ".png":
                case ".jpg":
                case ".jpeg":
                    businessCards = await _fileService.ParseQrCode(file);
                    break;

                default:
                    break;
            }
            foreach (var card in businessCards)
            {
                await CreateBusinessCardAsync(card);
            }
        }
    }
}



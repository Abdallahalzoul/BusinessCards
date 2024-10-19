using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IBusinessCard<BusinessCard>
    {
        Task<int> CreateBusinessCardAsync(BusinessCard card);
        Task  ImportBusinessCardsAsync(IFormFile file);
        Task<List<BusinessCard>> GetBusinessCardsAsync();
 
        Task<BusinessCard> GetBusinessCardByIdAsync(int id);
        Task<int> DeleteBusinessCardAsync(int id);
        Task<List<BusinessCard>> FilterBusinessCardsAsync(string name, string email, string phone, string gender);
        Task<List<BusinessCard>> ImportBusinessCardsAsync(List<BusinessCard> cards);
        Task<(byte[], string)> ExportBusinessCardsAsync(string format);
        Task<string>? GetBase64String(IFormFile PhotoFile);


    }
}

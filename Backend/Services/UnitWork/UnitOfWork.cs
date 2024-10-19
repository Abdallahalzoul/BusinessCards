using Domain.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UnitWork
{
    public class UnitOfWork : IUnitOfWork
    {
        public IBusinessCard<BusinessCard> BusinessCard { get; }
        public UnitOfWork(IBusinessCard<BusinessCard> businessCard)
        {
            BusinessCard = businessCard;
        }
    }
}

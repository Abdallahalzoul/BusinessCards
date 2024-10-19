using Domain.Models;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UnitWork
{
    public interface IUnitOfWork
    {
        public IBusinessCard<BusinessCard> BusinessCard { get; }
    }
}

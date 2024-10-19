using AutoMapper;
using Domain.Models;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.AutoMapper
{
    public class AutoMapperConfiguration
    {

        public static IMapper CreateMapper()
        {
            var MappConfig = new MapperConfiguration(x =>
            {
                x.CreateMap<BusinessCard, BusinessCardViewModel>().ReverseMap();
            });


            return MappConfig.CreateMapper();

        }

    }

}

using Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Constants;
using Repository.Repository;
using Services.Interface;
using Services.Service;
using Services.UnitWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IBusinessCard<BusinessCard>, BusinessCardServices>();


            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IExportService, ExportService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient(typeof(IRepositorySQL<>), typeof(RepositorySQL<>));

            CommonConstants.ConnectionString = configuration.GetConnectionString("DefaultConnection");


        }
    }
}

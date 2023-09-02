using businessLayer.IServices;
using businessLayer.SeedingData;
using businessLayer.Services;
using DataAccessLayer.IRepository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.DependancyResolver;
namespace businessLayer.DependancyResolver
{
    public static class DependancyResolver
    {
        public static void ConfigureDependancieseBusinessLayer(this IServiceCollection services, IConfiguration Configuration)
        {


            var Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(Configuration)
        .CreateLogger();
            services.AddSerilog(Logger);


            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IInvintoryValuationService, InvintoryValuationService>();

            SeedData.SeedIdentityData(services);


        }
    }
}

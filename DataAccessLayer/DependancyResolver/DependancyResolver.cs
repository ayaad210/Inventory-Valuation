using DataAccessLayer;
using DataAccessLayer.IRepository;
using DataAccessLayer.UnitOfWork_;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DataAccessLayer.DependancyResolver
{
    public static class DependancyResolver
    {
        public static void ConfigureDependanciesDataAccessLayer(this IServiceCollection services, IConfiguration Configuration)
        {


    
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("WebApplication1ContextConnection")));

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddTransient<IUnitOfWork, UnitOfWork>();



        }
    }
}

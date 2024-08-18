using Car.Storage.Application.Administrators.Data.Repositories.EFContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Car.Storage.Application.Administrators.IoC
{
    public static class AddDependencyInjectionServices
    {
        // <summary>
        /// Extension method to add configurations variables to be used by IOptions Pattern 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddConfigurationsVariables(this IServiceCollection services, IConfiguration configuration)
        {

           // services.Configure<OpenApiDocSettings>(configuration.GetSection("OpenApiDocSettings"));
            services.AddSingleton<IConfiguration>(configuration);
        }

        /// <summary>
        /// Extension method to add application configuration files
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="Environment"></param>
        public static void AddConfigurationFiles(this IConfigurationBuilder configuration, IWebHostEnvironment Environment)
        {
            configuration.SetBasePath(Environment.ContentRootPath)
           .AddJsonFile("appsettings.json", true, true)
           .AddJsonFile($"appsettings.{Environment.EnvironmentName}.json", true, true)
           .AddEnvironmentVariables();
        }

        /// <summary>
        /// Extension method to add all the general configurations that AspNet needs to work
        /// </summary>
        /// <param name="services"></param>
        public static void AddGeneralMidllewares(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers();
            services.AddMvcCore().AddApiExplorer();
            services.AddLogging();
        }

        public static void AddEfContexts(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarStorageContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}

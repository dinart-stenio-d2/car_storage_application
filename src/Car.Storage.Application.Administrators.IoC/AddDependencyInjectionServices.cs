using Car.Storage.Application.Administrators.Data.Repositories.EFContext;
using Car.Storage.Application.Administrators.IoC.swaggerconfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.IO.Compression;
using System.Reflection;

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

            services.Configure<OpenApiDocSettings>(configuration.GetSection("OpenApiDocSettings"));
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
           .AddUserSecrets(Assembly.GetExecutingAssembly(), optional: true ,reloadOnChange:true) 
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
        }

        /// <summary>
        /// Extension method to add all the Swagger  custom configurations
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddSwaggerGen(SwaggerConfig =>
            {
                SwaggerConfig.OperationFilter<SwaggerDefaultValues>();

                SwaggerConfig.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "Enter the JWT token in this way: Bearer{your token}",
                    Name = "Authorization",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                SwaggerConfig.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {

                              Type = ReferenceType.SecurityScheme,
                               Id = "Bearer"

                            }

                        },
                        new string[] { }
                    }
                });
                var xmlPath = Path.Combine(System.AppContext.BaseDirectory, "car_storage_application.API.xml");
                SwaggerConfig.IncludeXmlComments(xmlPath);
            });



            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            return services;
        }

        /// <summary>
        /// This extension method configures the Swagger UI
        /// </summary>
        /// <param name="app"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSwaggerConfig(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            return app;
        }

        /// <summary>
        /// Extension method used for general API settings
        /// </summary>
        /// <param name="services"></param>
        public static void AddApiConfig(this IServiceCollection services)
        {
            #region API versioning settings
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(majorVersion: 1, minorVersion: 0);
                options.ReportApiVersions = true;
            });

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;

            });

            services.Configure<ApiBehaviorOptions>(options =>
            {

                options.SuppressModelStateInvalidFilter = true;
            });



            #endregion API versioning settings

            #region API compression responses settings
            //To learn more about API compression : https://learn.microsoft.com/en-us/aspnet/core/performance/response-compression?view=aspnetcore-6.0
            services.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);
            services.Configure<BrotliCompressionProviderOptions>(options => options.Level = CompressionLevel.Fastest);

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });
            #endregion API compression responses settings
        }

        /// <summary>
        /// Extension method used to configure Serilog
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="host"></param>
        public static void AddLog(this IServiceCollection services, IConfiguration configuration, IHostBuilder host)
        {
            services.AddLogging(builder => builder.AddSerilog());

            var loggerConfiguration = new LoggerConfiguration()
           .ReadFrom.Configuration(configuration)
           .CreateLogger();

            host.UseSerilog();
        }


        /// <summary>
        /// Extension method to add repositories contexts that use EF core
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddEfContexts(this IServiceCollection services, IConfiguration configuration)
        {
            var test = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<CarStorageContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }


        /// <summary>
        /// Extension method configure Asp.Net Host and services
        /// </summary>
        /// <param name="app"></param>
        public static void CofigureServices(this WebApplication app, IApiVersionDescriptionProvider provider)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                // app.UseCors("Development");
                UseSwaggerConfig(app, provider);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseCors("Production");
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();
            app.UseHttpsRedirection();
            app.UseRouting();
            //Use this feature only if we need to control authorization and authentication
            //app.UseAuthentication();
            //app.UseAuthorization();
            app.UseResponseCompression();
            app.UseStaticFiles();
            app.MapControllers();
            app.Run();
        }
    }
}

using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Car.Storage.Application.Administrators.IoC.swaggerconfigurations
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider provider;
        private OpenApiDocSettings OpenApiDocSettings { get; set; }
        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IOptions<OpenApiDocSettings> OpenApiDocSettings)
        {
            this.provider = provider;
            this.OpenApiDocSettings = OpenApiDocSettings.Value;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description, OpenApiDocSettings));
            }
        }

        /// <summary>
        /// This method configure the OpenApi doc configuration parameters
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description, OpenApiDocSettings openApiDocSettings)
        {

            var info = new OpenApiInfo()
            {
                Title = openApiDocSettings.Title,
                Version = description.ApiVersion.ToString(),
                Description = openApiDocSettings.Description,
                Contact = new OpenApiContact() { Name = openApiDocSettings.Contact.Name, Email = openApiDocSettings.Contact.Email },
                License = new OpenApiLicense() { Name = openApiDocSettings.License.Name, Url = new Uri(openApiDocSettings.License.Url) },
            };

            if (description.IsDeprecated)
            {
                info.Description += openApiDocSettings.DeprecatedMessage;
            }

            return info;

        }
    }
}

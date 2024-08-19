using Car.Storage.Application.Administrators.IoC;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

AddDependencyInjectionServices.AddConfigurationFiles(builder.Configuration, builder.Environment);
AddDependencyInjectionServices.AddConfigurationsVariables(builder.Services, builder.Configuration);
AddDependencyInjectionServices.AddGeneralMidllewares(builder.Services);
AddDependencyInjectionServices.AddApiConfig(builder.Services);
AddDependencyInjectionServices.AddSwaggerConfig(builder.Services);
AddDependencyInjectionServices.AddEfContexts(builder.Services, builder.Configuration);

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
AddDependencyInjectionServices.CofigureServices(app, apiVersionDescriptionProvider);

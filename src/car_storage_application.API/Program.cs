using Car.Storage.Application.Administrators.IoC;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

var builder = WebApplication.CreateBuilder(args);

AddDependencyInjectionServices.AddConfigurationFiles(builder.Configuration, builder.Environment);
AddDependencyInjectionServices.AddConfigurationsVariables(builder.Services, builder.Configuration);
AddDependencyInjectionServices.AddGeneralMidllewares(builder.Services);
AddDependencyInjectionServices.AddAutoMapperMappings(builder.Services);
AddDependencyInjectionServices.AddApiConfig(builder.Services);
AddDependencyInjectionServices.AddLog(builder.Services, builder.Configuration, builder.Host);
AddDependencyInjectionServices.AddSwaggerConfig(builder.Services);
AddDependencyInjectionServices.AddEfContexts(builder.Services, builder.Configuration);
AddDependencyInjectionServices.AddRepositories(builder.Services);
AddDependencyInjectionServices.AddApplicationServices(builder.Services);

var app = builder.Build();

var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
AddDependencyInjectionServices.CofigureServices(app, apiVersionDescriptionProvider);

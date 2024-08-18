using Car.Storage.Application.Administrators.IoC;

var builder = WebApplication.CreateBuilder(args);


AddDependencyInjectionServices.AddConfigurationFiles(builder.Configuration, builder.Environment);
AddDependencyInjectionServices.AddConfigurationsVariables(builder.Services, builder.Configuration);
AddDependencyInjectionServices.AddGeneralMidllewares(builder.Services);
//AddDependencyInjectionServices.AddAutoMapperMappings(builder.Services);
//AddDependencyInjectionServices.AddApiConfig(builder.Services);
//AddDependencyInjectionServices.AddLog(builder.Services, builder.Configuration, builder.Host);
//AddDependencyInjectionServices.AddSwaggerConfig(builder.Services);
//AddDependencyInjectionServices.AddHandlers(builder.Services, builder.Configuration);
//AddDependencyInjectionServices.AddDomainServices(builder.Services, builder.Configuration);
//AddDependencyInjectionServices.AddApplicationServices(builder.Services, builder.Configuration);
AddDependencyInjectionServices.AddEfContexts(builder.Services, builder.Configuration);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();

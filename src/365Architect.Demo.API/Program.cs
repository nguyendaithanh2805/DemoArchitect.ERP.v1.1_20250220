using _365Architect.Demo.API.DependencyInjection.Extensions;
using _365Architect.Demo.API.DependencyInjection.Options;
using _365Architect.Demo.Application.DependencyInjection.Extension;
using _365Architect.Demo.Contract.DependencyInjection.Extensions;
using _365Architect.Demo.Contract.Middlewares;
using _365Architect.Demo.Persistence.DependencyInjection.Extensions;
using _365Architect.Demo.Presentation.Abstractions;
using _365Architect.Demo.Presentation.Common;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.WithOrigins()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var serviceName = "365EJSC API ERP";
builder.Services.AddControllers(options =>
{
}).AddApplicationPart(Assembly.GetExecutingAssembly());

//register api configuration
builder.Services.AddSingleton(new ApiConfig { Name = serviceName });
//Configure swagger
builder.Services.ConfigureOptions<SwaggerConfigureOptions>();

//Configure api versioning
builder.Services.AddApiVersioning(
        options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("x-api-version"),
                new QueryStringApiVersionReader());
        })
    .AddMvc()
    .AddApiExplorer(
        options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
    options.SuppressConsumesConstraintForFormFileParameters = true;
    options.InvalidModelStateResponseFactory = context => null;
});

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Environment.AddEnvironmentHelper();
builder.Services.AddProblemDetails();
builder.Environment.AddEnvironmentHelper();
builder.Services.AddHttpClient();
var app = builder.Build();

app.UseValidateModel();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) app.UseApiLayerSwagger();
//app.UseHsts();
app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseExceptionHandler();
app.UseAuthentication();
app.UseAuthorization();
app.UseStaticFiles();
app.MapPresentation();

app.Run();
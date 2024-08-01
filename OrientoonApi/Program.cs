using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using OrientoonApi;
using OrientoonApi.Data;
using OrientoonApi.Data.Contexts;
using OrientoonApi.Data.Repositories;
using OrientoonApi.Data.Repositories.Implementations;
using OrientoonApi.Data.Repositories.Interfaces;
using OrientoonApi.Infrastructure.Filters;
using OrientoonApi.Infrastructure.Middlewares;
using OrientoonApi.Services.Implementations;
using OrientoonApi.Services.Interfaces;
using OrientoonApi.Status.Implementations;
using OrientoonApi.Utils;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Orientoon API",
        Description = "Orientoon é um leitor de mangá que oferece imagens de alta qualidade!\r\n\r\nEste documento detalha nossa API como ela está no momento.",
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/license/mit")
        }
    });
    options.SchemaFilter<SwaggerDateFormatSchemaFilter>();
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<JsonExceptionFilter>();

}).AddNewtonsoftJson(options => { 
    options.SerializerSettings.Culture = new CultureInfo("pt-BR");
    options.SerializerSettings.Converters.Add(new StrictDateTimeConverter());
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

    options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
}).ConfigureApiBehaviorOptions(options =>
{
   // options.SuppressModelStateInvalidFilter = true; 
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
}); 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


var connectionString = builder.Configuration.GetConnectionString("DatabaseLocal");

builder.Services.AddDbContext<OrientoonContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<IContextRepository, ContextRepository>();
builder.Services.AddScoped<IOrientoonRepository, OrientoonRepository>();
builder.Services.AddScoped<IArtistaRepository, ArtistaRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();
builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
builder.Services.AddScoped<ITipoRepository, TipoRepository>();
builder.Services.AddScoped<IGeneroOrientoonRepository, GeneroOrientoonRepository>();
builder.Services.AddScoped<ITipoOrientoonRepository, TipoOrientoonRepository>();
builder.Services.AddScoped<ICapituloRepository, CapituloRepository>();
builder.Services.AddScoped<IImagemRepository, ImagemRepository>();



builder.Services.AddScoped<IOrientoonService ,OrientoonService>();
builder.Services.AddScoped<IArtistaService, ArtistaService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IGeneroService, GeneroService>();
builder.Services.AddScoped<ITipoService, TipoService>();
builder.Services.AddScoped<ICapituloService, CapituloService>();
builder.Services.AddScoped<IUrlService, UrlService>();




builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

var app = builder.Build();

app.UseCors("AllowOrigin");

app.UseMiddleware<NotFoundExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var imageFolderPath = builder.Configuration["FileUploadPath"];

app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(imageFolderPath),
    RequestPath = "/imagens" // O caminho base para acessar as imagens
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

app.Run();

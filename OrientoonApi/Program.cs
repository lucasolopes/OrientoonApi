using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add<JsonExceptionFilter>();

}).AddNewtonsoftJson(options => { 
   
   
    // options.AllowInputFormatterExceptionMessages = true;
   
    //options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore; // Ignora referências cíclicas
    //options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(); // Converte as propriedades para camelCase
    //options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter()); // Converte enums para string  
      //options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.IsoDateTimeConverter()); // Formata as datas
   // options.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc; // Define o timezone como UTC
  //  options.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.IsoDateFormat; // Formata as datas
  //  options.SerializerSettings.FloatFormatHandling = Newtonsoft.Json.FloatFormatHandling.DefaultValue; // Define o formato dos floats
  //  options.SerializerSettings.FloatParseHandling = Newtonsoft.Json.FloatParseHandling.Double; // Define o formato dos floats
   // options.SerializerSettings.StringEscapeHandling = Newtonsoft.Json.StringEscapeHandling.Default; // Define o escape das strings
  //  options.SerializerSettings.TypeNameHandling = Newtonsoft.Json.TypeNameHandling.None; // Não inclui o tipo no JSON
   // options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore; // Ignora valores nulos
  //  options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented; // Indenta o JSON
  //  options.SerializerSettings.DateParseHandling = Newtonsoft.Json.DateParseHandling.DateTime; // Define o formato dos dates
  //  options.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Error; //nao ignorar campos nao mapeados
  //  options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Include; //nao ignorar campos nulos
  //  options.SerializerSettings.DefaultValueHandling = Newtonsoft.Json.DefaultValueHandling.Include; //nao ignorar campos nulos
   // options.SerializerSettings.DateFormatString = "dd/MM/yyyy HH:mm:ss";
    options.SerializerSettings.Culture = new CultureInfo("pt-BR");
    options.SerializerSettings.Converters.Add(new StrictDateTimeConverter());
   
}).ConfigureApiBehaviorOptions(options =>
{
   // options.SuppressModelStateInvalidFilter = true; 
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "OrientoonApi", Version = "v1" });
    c.SchemaFilter<SwaggerDateFormatSchemaFilter>();
});

var connectionString = builder.Configuration.GetConnectionString("DatabaseLocal");

builder.Services.AddDbContext<OrientoonContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddScoped<IContextRepository, ContextRepository>();
builder.Services.AddScoped<IOrientoonRepository, OrientoonRepository>();
builder.Services.AddScoped<IArtistaRepository, ArtistaRepository>();
builder.Services.AddScoped<IAutorRepository, AutorRepository>();
builder.Services.AddScoped<IStatusRepository, StatusRepository>();

builder.Services.AddScoped<IOrientoonService ,OrientoonService>();
builder.Services.AddScoped<IArtistaService, ArtistaService>();
builder.Services.AddScoped<IAutorService, AutorService>();
builder.Services.AddScoped<IStatusService, StatusService>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
       );
});


var app = builder.Build();

app.UseMiddleware<NotFoundExceptionMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

app.Run();

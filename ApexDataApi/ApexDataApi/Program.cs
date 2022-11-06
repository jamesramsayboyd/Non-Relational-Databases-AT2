using ApexDataApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApexDataApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}

// Old code now moved to Startup.cs
//*******************************************************************


//using ApexDataApi.Services;
//using ApexDataApi.Models;
//using System.Reflection;
//using Microsoft.OpenApi.Models;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using ApexDataApi.Data;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddDbContext<ApexDataApiContext>(options =>
//    options.UseSqlServer(builder.Configuration.GetConnectionString("ApexDataApiContext") ?? throw new InvalidOperationException("Connection string 'ApexDataApiContext' not found.")));

//// Add services to the container.
//builder.Services.Configure<ApexPlayerDatabaseSettings>(
//    builder.Configuration.GetSection("ApexPlayerDatabase"));

//builder.Services.AddSingleton<PlayersService>();
//builder.Services.AddSingleton<CharactersService>();

//builder.Services.AddControllers();
//builder.Services.AddControllersWithViews();
//// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
////builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(options =>
//{
//    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
//        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

//    var basicSecurityScheme = new OpenApiSecurityScheme
//    {
//        Type = SecuritySchemeType.Http,
//        Scheme = "basic",
//        Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
//    };
//    options.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {basicSecurityScheme, new string[] { }}
//    });
//});

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();

//    // *** COMMENT THIS OUT IF IT DOESN'T WORK ***
//    app.UseRouting();
//}

//app.UseDefaultFiles();
//app.UseStaticFiles();

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

//app.Run();

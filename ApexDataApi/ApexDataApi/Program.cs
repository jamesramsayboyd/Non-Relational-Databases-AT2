using ApexDataApi.Services;
using ApexDataApi.Models;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ApexDataApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApexDataApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApexDataApiContext") ?? throw new InvalidOperationException("Connection string 'ApexDataApiContext' not found.")));

// Add services to the container.
builder.Services.Configure<ApexPlayerDatabaseSettings>(
    builder.Configuration.GetSection("ApexPlayerDatabase"));

builder.Services.AddSingleton<PlayersService>();
builder.Services.AddSingleton<CharactersService>();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
        $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));

    //var securityScheme = new OpenApiSecurityScheme
    //{
    //    Name = "JWT Authentication",
    //    Description = "Enter JWT Bearer token **_only_**",
    //    In = ParameterLocation.Header,
    //    Type = SecuritySchemeType.Http,
    //    Scheme = "bearer",
    //    BearerFormat = "JWT",
    //    Reference = new OpenApiReference
    //    {
    //        Id = JwtBearerDefaults.AuthenticationScheme,
    //        Type = ReferenceType.SecurityScheme
    //    }
    //};
    //options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    //options.AddSecurityRequirement(new OpenApiSecurityRequirement
    //{
    //    { securityScheme, new string[] { }}
    //});

    var basicSecurityScheme = new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "basic",
        Reference = new OpenApiReference { Id = "BasicAuth", Type = ReferenceType.SecurityScheme }
    };
    options.AddSecurityDefinition(basicSecurityScheme.Reference.Id, basicSecurityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {basicSecurityScheme, new string[] { }}
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
    //app.UseSwaggerUI(c =>
    //{
    //    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Apex Data API");
    //    c.RoutePrefix = "";
    //});

    // *** COMMENT THIS OUT IF IT DOESN'T WORK ***
    app.UseRouting();
}

app.UseDefaultFiles();
app.UseStaticFiles();
//app.UseMvc(routes =>
//{
//    routes.MapRoute(
//        name: "default",
//        template: "{controller=Home}/{action=Index}/{id?}");
//});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

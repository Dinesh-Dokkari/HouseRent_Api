using HouseRent_Api;
using HouseRent_Api.Data;
using HouseRent_Api.IRepository;
using HouseRent_Api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Log.Logger = new LoggerConfiguration().MinimumLevel.Debug()
//    .WriteTo.File("log/HouseLogs.txt", rollingInterval: RollingInterval.Day).CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers()//options.ReturnHttpNotAcceptable = true; })for not found format
    .AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.AddDbContext<ApplicationDbcontext>(options =>{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        });

builder.Services.AddAutoMapper(typeof(MappingConfig));
builder.Services.AddScoped<IHouseRepository,HouseRepository>();
builder.Services.AddScoped<IHouseNumberRepository,HouseNumberRepository>();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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

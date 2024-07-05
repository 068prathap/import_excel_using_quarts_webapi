using ImportExcelUsingQuartz.JobFactory;
using ImportExcelUsingQuartz.Models;
using ImportExcelUsingQuartz.Sheduler;
using Quartz.Impl;
using Quartz.Spi;
using Quartz;
using ImportExcelUsingQuartz.jobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ImportExcelUsingQuartz.Data;
using Microsoft.AspNetCore.Hosting;
using ImportExcelUsingQuartz;

//var host = new WebHostBuilder()
//.UseKestrel()
//.UseContentRoot(Directory.GetCurrentDirectory())
//.UseUrls("http://localhost:5000", "http://192.168.1.223:5000")
//.UseIISIntegration()
//.UseStartup<Startup>()
//.Build();

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ImportExcelUsingQuartzContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ImportExcelUsingQuartzContext") ?? throw new InvalidOperationException("Connection string 'ImportExcelUsingQuartzContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// job Scheduler
builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();

builder.Services.AddSingleton<IJob, UpdateJob>();
builder.Services.AddSingleton(new JobMetadata(Guid.NewGuid(), typeof(UpdateJob), "Notify Job", "0 */1 * ? * *"));

builder.Services.AddHostedService<MyScheduler>();

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

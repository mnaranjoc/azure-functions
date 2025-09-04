using AzureFunctions.Functions.Data;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

var conectionString = Environment.GetEnvironmentVariable("AzureSqlDataBase");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(conectionString));

builder.Build().Run();

using LeadManager.API.Middlewares;
using LeadManager.Application.Handlers;
using LeadManager.Domain.Interfaces;
using LeadManager.Infrastructure.Data;
using LeadManager.Infrastructure.Repositories;
using LeadManager.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(new Serilog.Formatting.Json.JsonFormatter())
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ILeadRepository, LeadRepository>();
builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateLeadHandler).Assembly);
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

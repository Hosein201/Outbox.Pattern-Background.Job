using App;
using FluentValidation;
using FluentValidation.AspNetCore;
using Info;
using Info.Background.Service;
using Info.Model;
using Info.Rep;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
var appSetting = builder.Configuration
    .GetSection(nameof(AppSetting))
    .Get<AppSetting>();
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IOutboxMessageRepository, OutboxMessageRepository>();
builder.Services.AddScoped(typeof(INoSqlRepository<>), typeof(NoSqlRepository<>));
builder.Services.AddSingleton(appSetting.RediSearchConfiguration);
builder.Services.AddValidatorsFromAssemblyContaining<OutboxPatternApplication>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(appSetting.DbContextConfiguration.Conn);
    // log every thing that related to database

    options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });
    options.EnableSensitiveDataLogging();

});

builder.Services.AddMediatR(typeof(OutboxPatternApplication));
builder.Services.AddQuartz(config =>
{
    var jobKey = new JobKey(nameof(ProductProcessOutboxMessagesJob));
    config.AddJob<ProductProcessOutboxMessagesJob>(jobKey)
    .AddTrigger(trigger =>
                trigger.ForJob(jobKey)
                .WithSimpleSchedule(simple =>
                                    simple.WithIntervalInSeconds(10)
                                    .RepeatForever()));
    config.UseMicrosoftDependencyInjectionJobFactory();

});

builder.Services.AddQuartzHostedService();

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

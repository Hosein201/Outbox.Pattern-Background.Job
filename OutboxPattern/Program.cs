using App;
using FluentValidation;
using FluentValidation.AspNetCore;
using Info;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<OutboxPatternApplication>();
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(string.Empty);
    // log every thing that related to database

    options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name });
    options.EnableSensitiveDataLogging();

});

//builder.Services.AddMediatR(typeof(OutboxPatternApplication));
builder.Services.AddQuartz(config =>
{
    //var jobKey = new JobKey(nameof(ShipmentProcessOutboxMessagesJob));
    //config.AddJob<ShipmentProcessOutboxMessagesJob>(jobKey)
    //.AddTrigger(trigger =>
    //            trigger.ForJob(jobKey)
    //            .WithSimpleSchedule(simple =>
    //                                simple.WithIntervalInMinutes(10)
    //                                .RepeatForever()));
    config.UseMicrosoftDependencyInjectionJobFactory();

});


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

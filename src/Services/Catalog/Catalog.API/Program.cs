using BuildingBlocks.Behaviour;
using Catalog.API.Exceptions.Handler;
using Catalog.API.Products.CreateProduct;
using FluentValidation;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
    config.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("DefaultConnection"));
}).UseLightweightSessions();
builder.Services.AddCarter();

builder.Services.AddHealthChecks().AddNpgSql(builder.Configuration.GetConnectionString("DefaultConnection"));


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapCarter();

app.UseExceptionHandler();

app.UseHealthChecks("/health",
    new HealthCheckOptions { ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse});
//app.UseExceptionHandler(exceptionHandlerApp =>
//{
//    exceptionHandlerApp.Run(async context =>
//    {
//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//        context.Response.ContentType = "application/json";

//        var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

//        if(exception is null)
//        {
//            return; // No exception to handle, exit early
//        }


//            var errorResponse = new ProblemDetails
//            {
//                Title = exception.Message,
//                Status = context.Response.StatusCode,
//                Detail = exception.StackTrace,
//            };

//        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//        logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);
//        context.Response.ContentType = "application/problem+json";
//        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

//        await context.Response.WriteAsJsonAsync(errorResponse);

//    });
//});

app.UseExceptionHandler(opt => { });

app.Run();

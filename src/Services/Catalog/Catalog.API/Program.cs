
var builder = WebApplication.CreateBuilder(args);

var assemply = typeof(Program).Assembly;

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assemply);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>)); // <,> apply for every response/request
});
builder.Services.AddValidatorsFromAssembly(assemply);
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.MapCarter();

app.UseExceptionHandler(options => { });

// app.UseExceptionHandler(exceptionHandlerApp =>
// {
//     exceptionHandlerApp.Run(async context =>
//     {
//         var exception = context.Features.Get<IExceptionHandlerPathFeature>()?.Error;
//
//         if (exception == null)
//         {
//             return;
//         }
//
//         var problemDetails = new ProblemDetails
//         {
//             Title = exception.Message,
//             Status = StatusCodes.Status500InternalServerError,
//             Detail = exception.StackTrace
//         };
//         
//         var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
//         logger.LogError(exception, exception.Message);
//
//         context.Response.StatusCode = StatusCodes.Status500InternalServerError;
//         context.Response.ContentType = "application/problem+json";
//
//
//         await context.Response.WriteAsJsonAsync(problemDetails);
//     });
// });

app.Run();
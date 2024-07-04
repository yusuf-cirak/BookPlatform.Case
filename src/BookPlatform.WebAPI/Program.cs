using BookPlatform.Application;
using BookPlatform.Infrastructure;
using BookPlatform.WebAPI.Extensions;
using BookPlatform.WebAPI.Infrastructure.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddSerilogLogging();

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddApplicationServices();

builder.Services.AddIdentityAuthentication(builder.Configuration);

builder.Services.AddIdentityEndpoints();

builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseSerilogLogging();

app.UseExceptionHandler();

// app.UseHttpsRedirection();

app.MapAuthenticationMiddleware();

app.MapIdentityEndpoints();
app.MapApiEndpoints();

app.ApplyPendingMigrations();
// app.ApplyDatabaseInitializers();

app.Run();
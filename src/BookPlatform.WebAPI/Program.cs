using BookPlatform.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthenticationServices(builder.Configuration);

builder.Services.AddDatabaseServices(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.ApplyPendingMigrations();

app.Run();
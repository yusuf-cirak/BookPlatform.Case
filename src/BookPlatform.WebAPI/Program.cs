using BookPlatform.WebAPI.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseServices(builder.Configuration);

builder.Services.AddIdentityAuthentication(builder.Configuration);

builder.Services.AddIdentityEndpoints();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();

// app.ApplyPendingMigrations();

app.UseIdentityAuth();

app.MapIdentityEndpoints();

app.Run();
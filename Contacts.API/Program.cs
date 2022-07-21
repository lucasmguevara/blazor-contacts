using Contacts.Repositories;
using Microsoft.Net.Http.Headers;
using System.Data;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IContactRepository, ContactRepository>();
string dbConnection = builder.Configuration.GetConnectionString("ConnectionString");
builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(dbConnection));
//builder.Services.AddCors(policy =>
//{
//    policy.AddPolicy("_myAllowSpecificOrigins", builder => builder.WithOrigins("https://localhost:7284/;http://localhost:5284/")
//         .AllowAnyMethod()
//         .AllowAnyHeader()
//         .AllowCredentials());
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(policy =>
    policy.WithOrigins("https://localhost:7284", "http://localhost:5284")
    .AllowAnyMethod()
    .WithHeaders(HeaderNames.ContentType)
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

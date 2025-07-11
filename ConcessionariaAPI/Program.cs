using ConcessionariaAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddOpenApi();

// database connection
string? mySqlConnection = builder.Configuration.GetConnectionString("ConcessDbConnection");
builder.Services.AddDbContext<ConcessDbContext>(options =>
{
    options.UseMySql(mySqlConnection,
        ServerVersion.AutoDetect(mySqlConnection));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/openapi/v1.json", ""); });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

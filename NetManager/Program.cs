using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetManager.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<NetManagerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("NetManagerContext") ?? throw new InvalidOperationException("Connection string 'NetManagerContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c=>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetManager Api");
        c.RoutePrefix = string.Empty;
    });
} 
else if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

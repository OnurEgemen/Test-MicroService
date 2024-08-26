using MarketService.Data.Contexts;
using MarketService.Data.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMassTransit(x =>
{
    // �zellik belirtmedi�imiz i�in default �zellikleri ile aya�a kalkar.
    x.UsingRabbitMq();
});

builder.Services.AddControllers();

builder.Services.AddDbContext<MarketContext>(opt =>
{
    opt.UseSqlServer("Server = Adresi Yaz�n�z; Database=MarketDb; Integrated Security = true;TrustServerCertificate=True;");
});

builder.Services.AddScoped<MarketRepository>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

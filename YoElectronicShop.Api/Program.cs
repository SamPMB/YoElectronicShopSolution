using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using YoElectronicShop.Api.Data;
using YoElectronicShop.Api.Repositories;
using YoElectronicShop.Api.Repositories.Contracts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContextPool<YoElectronicShopDBContext>(options =>

options.UseSqlServer(builder.Configuration.GetConnectionString("YoElectronicShop"))

);
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(Policy => Policy.WithOrigins("http://localhost:7041", "https://localhost:7041")
.AllowAnyMethod()
.WithHeaders(HeaderNames.ContentType)
);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

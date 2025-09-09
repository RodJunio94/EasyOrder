using EasyOrder.Application.Interfaces;
using EasyOrder.Application.Services;
using EasyOrder.Domain.Interfaces;
using EasyOrder.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register repositories for dependency injection
builder.Services.AddSingleton<IOrderRepository, InMemoryOrderRepository>();
builder.Services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

// Register application services for dependency injection
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<ICustomerService, CustomerService>();
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
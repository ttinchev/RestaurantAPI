using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Restaurant.Api.DTO.Request;
using Restaurant.Api.Validators;
using Restaurant.Application.Commands;
using Restaurant.Application.Models.Order;
using Restaurant.Domain.Interfaces;
using Restaurant.Infrastructure.Persistance;
using Restaurant.Infrastructure.Repository;
using Restaurant.Infrastructure.UOW;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddDbContext<RestaurantContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateOrderCommand>());

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();
        builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
        builder.Services.AddScoped<ITableRepository, TableRepository>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // FluentValidation - Order validators
        builder.Services.AddScoped<IValidator<CreateOrderDto>, OrderValidator>();
        builder.Services.AddScoped<IValidator<UpdateOrderDto>, UpdateOrderValidator>();
        builder.Services.AddScoped<IValidator<OrderItemRequestModel>, OrderItemValidator>();

        // FluentValidation - Category validators
        builder.Services.AddScoped<IValidator<CreateCategoryDto>, CreateCategoryValidator>();
        builder.Services.AddScoped<IValidator<UpdateCategoryDto>, UpdateCategoryValidator>();

        // FluentValidation - MenuItem validators
        builder.Services.AddScoped<IValidator<CreateMenuItemDto>, CreateMenuItemValidator>();
        builder.Services.AddScoped<IValidator<UpdateMenuItemDto>, UpdateMenuItemValidator>();

        // FluentValidation - Table validators
        builder.Services.AddScoped<IValidator<CreateTableDto>, CreateTableValidator>();
        builder.Services.AddScoped<IValidator<UpdateTableDto>, UpdateTableValidator>();

        // FluentValidation - Payment validators
        builder.Services.AddScoped<IValidator<CreatePaymentDto>, CreatePaymentValidator>();
        builder.Services.AddScoped<IValidator<UpdatePaymentDto>, UpdatePaymentValidator>();

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
    }
}
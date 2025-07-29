using AutoMapper;
using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Application.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Product mappings
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category != null ? src.Category.Name : ""))
                .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.Stocks != null && src.Stocks.Any() ? src.Stocks.First().Quantity : 0))
                .ForMember(dest => dest.StockLocation, opt => opt.MapFrom(src => src.Stocks != null && src.Stocks.Any() ? src.Stocks.First().Location : ""));

            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();

            // Order mappings
            CreateMap<Order, OrderDto>()
                .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.Table != null ? src.Table.TableNumber : ""))
                .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Customer != null ? $"{src.Customer.FirstName} {src.Customer.LastName}" : ""))
                .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee != null ? $"{src.Employee.FirstName} {src.Employee.LastName}" : ""));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : ""));

            CreateMap<CreateOrderDto, Order>();
            CreateMap<CreateOrderItemDto, OrderItem>();
            CreateMap<UpdateOrderDto, Order>();
            CreateMap<UpdateOrderItemDto, OrderItem>();

            // Stock mappings
            CreateMap<Stock, StockDto>();
            CreateMap<CreateStockDto, Stock>();
            CreateMap<UpdateStockDto, Stock>();

            // Category mappings
            CreateMap<Category, CategoryDto>();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Table mappings
            CreateMap<Table, TableDto>();
            CreateMap<CreateTableDto, Table>();
            CreateMap<UpdateTableDto, Table>();

            // Customer mappings
            CreateMap<Customer, CustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerDto, Customer>();

            // Employee mappings
            CreateMap<Employee, EmployeeDto>();
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();

            // Reservation mappings
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.TableNumber, opt => opt.MapFrom(src => src.Table != null ? src.Table.TableNumber : ""));
            CreateMap<CreateReservationDto, Reservation>();
            CreateMap<UpdateReservationDto, Reservation>();

            // Payment mappings
            CreateMap<Payment, PaymentDto>();
            CreateMap<CreatePaymentDto, Payment>();
            CreateMap<UpdatePaymentDto, Payment>();

            // Ingredient mappings
            CreateMap<Ingredient, IngredientDto>()
                .ForMember(dest => dest.CurrentStock, opt => opt.MapFrom(src => src.Stocks != null && src.Stocks.Any() ? src.Stocks.First().Quantity : 0))
                .ForMember(dest => dest.StockLocation, opt => opt.MapFrom(src => src.Stocks != null && src.Stocks.Any() ? src.Stocks.First().Location : ""));
            CreateMap<CreateIngredientDto, Ingredient>();
            CreateMap<UpdateIngredientDto, Ingredient>();

            // Recipe mappings
            CreateMap<Recipe, RecipeDto>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : ""))
                .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.Ingredient != null ? src.Ingredient.Name : ""));
            CreateMap<CreateRecipeDto, Recipe>();
            CreateMap<UpdateRecipeDto, Recipe>();
        }
    }
} 
using CarShopFinal.Application.Features.Car.CreateCar;
using CarShopFinal.Application.Features.Car.DeleteCar;
using CarShopFinal.Application.Features.Car.GetAllCars;
using CarShopFinal.Application.Features.Car.GetFilteredCar;
using CarShopFinal.Application.Features.Car.GetSingleCar;
using CarShopFinal.Application.Features.Car.ReserveCar;
using CarShopFinal.Application.Features.Car.UpdateCar;
using CarShopFinal.Application.Features.Customer.CreateCustomer;
using CarShopFinal.Application.Features.Customer.GetCustomerByEmail;
using CarShopFinal.Application.Features.Customer.GetCustomerById;
using CarShopFinal.Application.Features.Customer.UpdateCustomer;
using CarShopFinal.Application.Features.Listing.ApproveListing;
using CarShopFinal.Application.Features.Listing.CreateListing;
using CarShopFinal.Application.Features.Listing.DeleteListing;
using CarShopFinal.Application.Features.Listing.GetListings;
using CarShopFinal.Application.Features.Listing.RejectListing;
using CarShopFinal.Application.Features.Listing.UpdateListing;
using CarShopFinal.Application.Features.Order.CancelOrder;
using CarShopFinal.Application.Features.Order.CreateOrder;
using CarShopFinal.Application.Features.Order.GetOrderById;

namespace CarShopFinal.Application.Dependency;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Car handlers
        services.AddScoped<CreateCarHandler>();
        services.AddScoped<GetAllCarsHandler>();
        services.AddScoped<GetSingleCarHandler>();
        services.AddScoped<DeleteCarHandler>();
        services.AddScoped<GetFilteredCarHandler>();
        services.AddScoped<ReserveCarHandler>();
        services.AddScoped<UpdateCarHandler>();

        // Customer handlers
        services.AddScoped<CreateCustomerHandler>();
        services.AddScoped<GetCustomerByIdHandle>();
        services.AddScoped<GetCustomerByEmailHandle>();
        services.AddScoped<UpdateCustomerHandler>();

        // Listing handlers
        services.AddScoped<CreateListingHandler>();
        services.AddScoped<GetListingsHandler>();
        services.AddScoped<ApproveListingHandler>();
        services.AddScoped<RejectListingHandler>();
        services.AddScoped<DeleteListingHandler>();
        services.AddScoped<UpdateListingHandler>();

        // Order handlers
        services.AddScoped<CreateOrderHandle>();
        services.AddScoped<GetOrderByIdHandler>();
        services.AddScoped<CancelOrderHandler>();

        return services;
    }
}


// PRAVKI
/*
 *
 * // Customer handlers
   services.AddScoped<CreateCustomerHandler>();
   services.AddScoped<GetCustomerByIdHandle>();
   services.AddScoped<GetCustomerByEmailHandle>();
   services.AddScoped<UpdateCustomerHandler>();

   // Listing handlers
   services.AddScoped<CreateListingHandler>();
   services.AddScoped<GetListingsHandler>();
   services.AddScoped<ApproveListingHandler>();
   services.AddScoped<RejectListingHandler>();
   services.AddScoped<DeleteListingHandler>();
   services.AddScoped<UpdateListingHandler>();

   // Order handlers
   services.AddScoped<CreateOrderHandle>();
   services.AddScoped<GetOrderByIdHandler>();
   services.AddScoped<CancelOrderHandler>();
 */
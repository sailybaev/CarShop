using CarShopFinal.Application.Features.Car.CreateCar;
using CarShopFinal.Application.Features.Car.DeleteCar;
using CarShopFinal.Application.Features.Car.GetAllCars;
using CarShopFinal.Application.Features.Car.GetFilteredCar;
using CarShopFinal.Application.Features.Car.GetSingleCar;
using CarShopFinal.Application.Features.Car.ReserveCar;
using CarShopFinal.Application.Features.Car.UpdateCar;

//using CarShopFinal.Application;

namespace CarShopFinal.Application.Dependency;


public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateCarHandler>();
        services.AddScoped<GetAllCarsHandler>();
        services.AddScoped<GetSingleCarHandler>();
        services.AddScoped<DeleteCarHandler>();
        services.AddScoped<GetFilteredCarHandler>();
        services.AddScoped<ReserveCarHandler>();
        services.AddScoped<UpdateCarHandler>();
        
        return services;
    }
}
using CarShopFinal.Application.Features.Car.CreateCar;
using CarShopFinal.Application.Features.Car.GetFilteredCar;
//using CarShopFinal.Application.Service;
using CarShopFinal.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly CreateCarHandler _createCarHandler;
    private readonly GetFilteredCarHandler _getFilteredCarHandler;

    public CarsController(CreateCarHandler createCarHandler, GetFilteredCarHandler getFilteredCarHandler)
    {
        _createCarHandler = createCarHandler;
        _getFilteredCarHandler = getFilteredCarHandler;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCar(string brand, string model, decimal price, int year, string vin)
    {
        var command = new CreateCarCommand(brand, model, price, year, new VIN(vin));
        var carId = await _createCarHandler.Handle(command);
        return Ok(carId);
    }

    [HttpGet]
    public async Task<IActionResult> GetFilteredCars([FromQuery] string status)
    {
        var cars = new List<Car>();
        cars = await _getFilteredCarHandler.Handle(new GetFilteredCarQuery(status));
        return Ok(cars);
    }

}
//
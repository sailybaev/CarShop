using CarShopFinal.Application.Features.Car.CreateCar;
using CarShopFinal.Application.Features.Car.DeleteCar;
using CarShopFinal.Application.Features.Car.GetAllCars;
using CarShopFinal.Application.Features.Car.GetFilteredCar;
using CarShopFinal.Application.Features.Car.GetSingleCar;
using CarShopFinal.Application.Features.Car.ReserveCar;
using CarShopFinal.Application.Features.Car.UpdateCar;
using CarShopFinal.Domain.Models;
using CarShopFinal.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarsController : ControllerBase
{
    private readonly CreateCarHandler _createCarHandler;
    private readonly GetFilteredCarHandler _getFilteredCarHandler;
    private readonly GetAllCarsHandler _getAllCarsHandler;
    private readonly GetSingleCarHandler _getSingleCarHandler;
    private readonly UpdateCarHandler _updateCarHandler;
    private readonly DeleteCarHandler _deleteCarHandler;
    private readonly ReserveCarHandler _reserveCarHandler;

    public CarsController(
        CreateCarHandler createCarHandler,
        GetFilteredCarHandler getFilteredCarHandler,
        GetAllCarsHandler getAllCarsHandler,
        GetSingleCarHandler getSingleCarHandler,
        UpdateCarHandler updateCarHandler,
        DeleteCarHandler deleteCarHandler,
        ReserveCarHandler reserveCarHandler)
    {
        _createCarHandler = createCarHandler;
        _getFilteredCarHandler = getFilteredCarHandler;
        _getAllCarsHandler = getAllCarsHandler;
        _getSingleCarHandler = getSingleCarHandler;
        _updateCarHandler = updateCarHandler;
        _deleteCarHandler = deleteCarHandler;
        _reserveCarHandler = reserveCarHandler;
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateCar([FromBody] CreateCarRequest request)
    {
        var command = new CreateCarCommand(request.Brand, request.Model, request.Price, request.Currency, request.Year, new VIN(request.Vin));
        var carId = await _createCarHandler.Handle(command);
        return Ok(carId);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCars()
    {
        var cars = await _getAllCarsHandler.Handle(new GetAllCarsQuery());
        return Ok(cars);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> GetFilteredCars([FromQuery] string status)
    {
        var cars = await _getFilteredCarHandler.Handle(new GetFilteredCarQuery(status));
        return Ok(cars);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCar(Guid id)
    {
        var car = await _getSingleCarHandler.Handle(new GetSingleCarQuery(id));
        if (car is null) return NotFound();
        return Ok(car);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCar(Guid id, [FromBody] UpdateCarRequest request)
    {
        var command = new UpdateCarCommand(id, request.Brand, request.Model, request.Year, request.Vin, request.Price, request.Currency);
        await _updateCarHandler.Handle(command);
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCar(Guid id)
    {
        await _deleteCarHandler.DeleteAsync(new DeleteCarCommand(id));
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:guid}/reserve")]
    public async Task<IActionResult> ReserveCar(Guid id)
    {
        await _reserveCarHandler.Handle(new ReserveCarCommand(id));
        return NoContent();
    }
}

public record CreateCarRequest(string Brand, string Model, decimal Price, string Currency, int Year, string Vin);
public record UpdateCarRequest(string Brand, string Model, int Year, string Vin, decimal Price, string Currency);

/* PRAVKA
   private readonly GetAllCarsHandler _getAllCarsHandler;
   private readonly GetSingleCarHandler _getSingleCarHandler;
   private readonly UpdateCarHandler _updateCarHandler;
   private readonly DeleteCarHandler _deleteCarHandler;
   private readonly ReserveCarHandler _reserveCarHandler;
*/

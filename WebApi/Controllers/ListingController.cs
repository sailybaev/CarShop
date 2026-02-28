using CarShopFinal.Application.Features.Listing.CreateListing;
using CarShopFinal.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;

[ApiController]
[Route("api/listing")]
[Authorize]

public class ListingController:ControllerBase
{
    private readonly CreateListingHandler _createListingHandler;

    public ListingController(CreateListingHandler createListingHandler)
    {
        _createListingHandler = createListingHandler;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateListing([FromBody] CreateListingCommand command)
    {
       await _createListingHandler.Handle(command);
       return Ok();
    }
}
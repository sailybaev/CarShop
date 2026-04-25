using CarShopFinal.Application.Features.Listing.ApproveListing;
using CarShopFinal.Application.Features.Listing.CreateListing;
using CarShopFinal.Application.Features.Listing.DeleteListing;
using CarShopFinal.Application.Features.Listing.GetListingById;
using CarShopFinal.Application.Features.Listing.GetListings;
using CarShopFinal.Application.Features.Listing.RejectListing;
using CarShopFinal.Application.Features.Listing.UpdateListing;
using CarShopFinal.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;

[ApiController]
[Route("api/listing")]
public class ListingController : ControllerBase
{
    private readonly CreateListingHandler _createListingHandler;
    private readonly GetListingsHandler _getListingsHandler;
    private readonly GetListingByIdHandler _getListingByIdHandler;
    private readonly ApproveListingHandler _approveListingHandler;
    private readonly RejectListingHandler _rejectListingHandler;
    private readonly DeleteListingHandler _deleteListingHandler;
    private readonly UpdateListingHandler _updateListingHandler;
    private readonly IFileService _fileService;

    public ListingController(
        CreateListingHandler createListingHandler,
        GetListingsHandler getListingsHandler,
        GetListingByIdHandler getListingByIdHandler,
        ApproveListingHandler approveListingHandler,
        RejectListingHandler rejectListingHandler,
        DeleteListingHandler deleteListingHandler,
        UpdateListingHandler updateListingHandler,
        IFileService fileService)
    {
        _createListingHandler = createListingHandler;
        _getListingsHandler = getListingsHandler;
        _getListingByIdHandler = getListingByIdHandler;
        _approveListingHandler = approveListingHandler;
        _rejectListingHandler = rejectListingHandler;
        _deleteListingHandler = deleteListingHandler;
        _updateListingHandler = updateListingHandler;
        _fileService = fileService;
    }

    [Authorize]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> CreateListing([FromForm] CreateListingRequest request, IFormFileCollection photos)
    {
        var fileNames = new List<string>();
        foreach (var photo in photos)
        {
            var fileName = await _fileService.SaveFileAsync(photo.OpenReadStream(), photo.FileName);
            fileNames.Add(fileName);
        }

        var command = new CreateListingCommand(request.SellerId, request.CarId, request.City, request.Description, fileNames);
        var id = await _createListingHandler.Handle(command);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> GetListings(
        [FromQuery] string? city,
        [FromQuery] string? brand,
        [FromQuery] string? model,
        [FromQuery] string? year,
        [FromQuery] decimal? priceFrom,
        [FromQuery] decimal? priceTo,
        [FromQuery] string? listingStatus,
        [FromQuery] string? moderationStatus)
    {
        var listings = await _getListingsHandler.Handle(
            new GetListingsQuery(city, brand, model, year, priceFrom, priceTo, listingStatus, moderationStatus));
        return Ok(listings);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetListing(Guid id)
    {
        var listing = await _getListingByIdHandler.Handle(new GetListingByIdQuery(id));
        if (listing is null) return NotFound();
        return Ok(listing);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateListing(Guid id, [FromBody] UpdateListingRequest request)
    {
        await _updateListingHandler.Handle(new UpdateListingCommand(id, request.City, request.Description));
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteListing(Guid id)
    {
        await _deleteListingHandler.Handle(new DeleteListingCommand(id));
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:guid}/approve")]
    public async Task<IActionResult> ApproveListing(Guid id)
    {
        await _approveListingHandler.Handle(new ApproveListingCommand(id));
        return NoContent();
    }

    [Authorize]
    [HttpPut("{id:guid}/reject")]
    public async Task<IActionResult> RejectListing(Guid id)
    {
        await _rejectListingHandler.Handle(new RejectListingCommand(id));
        return NoContent();
    }
}

public record CreateListingRequest(Guid SellerId, Guid CarId, string City, string Description);

public record UpdateListingRequest(string City, string Description);

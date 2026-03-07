using CarShopFinal.Application.Features.Seller.CreateSeller;
using CarShopFinal.Application.Features.Seller.GetSellerByEmail;
using CarShopFinal.Application.Features.Seller.GetSellerById;
using CarShopFinal.Application.Features.Seller.GetSellerByUserId;
using CarShopFinal.Application.Features.Seller.UpdateSeller;
using CarShopFinal.Application.Features.Seller.UpdateSellerCity;
using Microsoft.AspNetCore.Mvc;

namespace CarShopFinal.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SellerController:ControllerBase
{
    private readonly CreateSellerHandler _create;
    private readonly GetSellerByUserIDHandler _getSellerByUserID;
    private readonly GetSellerByEmailHandler _getSellerByEmail;
    private readonly GetSellerByIdHandler _getSellerById;
    private readonly UpdateSellerCityHandler  _updateSellerCity;
    private readonly UpdateSellerContactInfoHandler  _updateSellerContactInfo;
    
    public SellerController(
        CreateSellerHandler create, 
        GetSellerByUserIDHandler getSellerByUserID,
        GetSellerByEmailHandler getSellerByEmail,
        GetSellerByIdHandler getSellerById,
        UpdateSellerCityHandler updateSellerCity,
        UpdateSellerContactInfoHandler updateSellerContactInfo)
    {
        _create = create;
        _getSellerByUserID = getSellerByUserID;
        _getSellerByEmail = getSellerByEmail;
        _getSellerById = getSellerById;
        _updateSellerCity = updateSellerCity;
        _updateSellerContactInfo = updateSellerContactInfo;
    }

    [HttpPost]
    public async Task<IActionResult> CreateSeller([FromBody] CreateSellerCommand command)
    {
        var seller = await _create.Handle(command);
        return Ok(seller);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetSellerById([FromRoute] GetSellerByIdQuery query)
    {
        var seller = await _getSellerById.Handle(new GetSellerByIdQuery(query.id));
        if (seller is null) return NotFound();
        return Ok(seller);
    }

    [HttpGet("email")]
    public async Task<IActionResult> GetSellerByEmail([FromRoute] string email)
    {
        var seller = await _getSellerByEmail.Handle(new GetSellerByEmailQuery(email));
        if (seller is null) return NotFound();
        return Ok(seller);
    }

    [HttpGet("UserId")]
    public async Task<IActionResult> GetSellerByUserId([FromRoute] Guid userId)
    {
        var seller = await _getSellerByUserID.Handle(new GetSellerByUserIDQuery(userId));
        if (seller is null) return NotFound();
        return Ok(seller);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateSellerContactInfo([FromRoute] Guid id, [FromBody] string phone, string email)
    {
        await _updateSellerContactInfo.Handle(new UpdateSellerContactInfoCommand(id, phone, email));
        return NoContent();
    }

    [HttpPut("{id:guid}/city")]
    public async Task<IActionResult> UpdateSellerCity([FromRoute] Guid id, [FromBody] string city, string address)
    {
        await _updateSellerCity.Handle(new UpdateSellerCityCommand(id,city,address));
        return NoContent();
    }
}
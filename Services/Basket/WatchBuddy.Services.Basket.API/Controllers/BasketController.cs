using Microsoft.AspNetCore.Mvc;
using WatchBuddy.Services.Basket.API.Dtos;
using WatchBuddy.Services.Basket.API.Services;
using WatchBuddy.Shared.ControllerBases;
using WatchBuddy.Shared.Services;

namespace WatchBuddy.Services.Basket.API.Controllers;

public class BasketController(IBasketService basketService, ISharedIdentityService userService)
    : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetBasket()
    {
        var basketData = await basketService.GetBasket(userService.GetUserId);
        return CreateActionResultInstance(basketData);
    }

    [HttpPost]
    public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
    {
        var response = await basketService.SaveOrUpdateBasket(basketDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasket()
    {
        return CreateActionResultInstance(await basketService.DeleteBasket(userService.GetUserId));
    }
}
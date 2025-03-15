using WatchBuddy.Services.Basket.API.Dtos;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.Services.Basket.API.Services;

public interface IBasketService
{
    Task<BaseServiceResponse<BasketDto>> GetBasket(string userId);
    Task<BaseServiceResponse<bool>> SaveOrUpdateBasket(BasketDto basketDto);
    Task<BaseServiceResponse<bool>> DeleteBasket(string userId);
}
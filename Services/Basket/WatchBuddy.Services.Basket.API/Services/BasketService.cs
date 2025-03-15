using System.Text.Json;
using WatchBuddy.Services.Basket.API.Dtos;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.Services.Basket.API.Services;

public class BasketService(RedisService redisService) : IBasketService
{
    public async Task<BaseServiceResponse<BasketDto>> GetBasket(string userId)
    {
        var existBasket = await redisService.GetDatabase().StringGetAsync(userId);
        
        return string.IsNullOrEmpty(existBasket)
            ? BaseServiceResponse<BasketDto>.Fail("Basket not found", 404)
            : BaseServiceResponse<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
    }

    public async Task<BaseServiceResponse<bool>> SaveOrUpdateBasket(BasketDto basketDto)
    {
        var status = await redisService.GetDatabase().StringSetAsync(basketDto.UserId, JsonSerializer.Serialize(basketDto));
        return status 
            ? BaseServiceResponse<bool>.Success(204) 
            : BaseServiceResponse<bool>.Fail("Something went wrong", 500);
    }

    public async Task<BaseServiceResponse<bool>> DeleteBasket(string userId)
    {
        var existBasket = await redisService.GetDatabase().StringGetAsync(userId);

        if (string.IsNullOrEmpty(existBasket))
            BaseServiceResponse<BasketDto>.Fail("Basket not found", 404);
        
        var status = await redisService.GetDatabase().KeyDeleteAsync(userId);
        return status 
            ? BaseServiceResponse<bool>.Success(204) 
            : BaseServiceResponse<bool>.Fail("Something went wrong", 500);
    }
}
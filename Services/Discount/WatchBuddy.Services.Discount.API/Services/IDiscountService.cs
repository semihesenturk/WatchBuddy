using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.Services.Discount.API.Services;

public interface IDiscountService
{
    Task<BaseServiceResponse<List<Models.Discount>>> GetAll();
    Task<BaseServiceResponse<Models.Discount>> GetById(int id);
    Task<BaseServiceResponse<NoContent>> CreateDiscount(Models.Discount discount);
    Task<BaseServiceResponse<NoContent>> UpdateDiscount(Models.Discount discount);
    Task<BaseServiceResponse<NoContent>> DeleteDiscount(int id);
    Task<BaseServiceResponse<Models.Discount>> GetByCodeAndUserId(string code, string userId);
}
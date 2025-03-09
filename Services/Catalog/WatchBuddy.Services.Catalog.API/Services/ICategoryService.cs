using WatchBuddy.Services.Catalog.API.Dtos;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.Services.Catalog.API.Services;

public interface ICategoryService
{
    Task<BaseServiceResponse<List<CategoryDto>>> GetAllAsync();
    Task<BaseServiceResponse<CategoryDto>> GetByIdAsync(string id);
    Task<BaseServiceResponse<CategoryDto>> CreateAsync(CategoryDto categoryDto);
}
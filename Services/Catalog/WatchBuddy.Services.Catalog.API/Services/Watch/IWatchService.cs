using WatchBuddy.Services.Catalog.API.Dtos;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.Services.Catalog.API.Services.Watch;

public interface IWatchService
{
    Task<BaseServiceResponse<List<WatchDto>>> GetAllAsync();
    Task<BaseServiceResponse<WatchDto>> GetByIdAsync(string id);
    Task<BaseServiceResponse<List<WatchDto>>> GetAllByUserId(string userId);
    Task<BaseServiceResponse<WatchDto>> CreateAsync(WatchCreateDto watch);
    Task<BaseServiceResponse<NoContent>> UpdateAsync(WatchUpdateDto watch);
    Task<BaseServiceResponse<NoContent>> DeleteAsync(string id);
}
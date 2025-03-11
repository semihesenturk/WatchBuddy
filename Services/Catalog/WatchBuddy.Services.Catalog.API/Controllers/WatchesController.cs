using Microsoft.AspNetCore.Mvc;
using WatchBuddy.Services.Catalog.API.Dtos;
using WatchBuddy.Services.Catalog.API.Services.Watch;
using WatchBuddy.Shared.ControllerBases;

namespace WatchBuddy.Services.Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WatchesController(IWatchService watchService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await watchService.GetAllAsync();
        return CreateActionResultInstance(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var response = await watchService.GetByIdAsync(id);
        return CreateActionResultInstance(response);
    }

    [HttpGet]
    [Route("/api/[controller]/GetAllByUserId/{userId}")]
    public async Task<IActionResult> GetAllByUserId(string userId)
    {
        var response = await watchService.GetAllByUserId(userId);
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(WatchCreateDto watchCreateDto)
    {
        var response = await watchService.CreateAsync(watchCreateDto);
        return CreateActionResultInstance(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update(WatchUpdateDto watchUpdateDto)
    {
        var response = await watchService.UpdateAsync(watchUpdateDto);
        return CreateActionResultInstance(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var response = await watchService.DeleteAsync(id);
        return CreateActionResultInstance(response);
    }
}
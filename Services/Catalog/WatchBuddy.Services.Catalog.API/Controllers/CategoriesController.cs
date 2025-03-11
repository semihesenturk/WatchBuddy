using Microsoft.AspNetCore.Mvc;
using WatchBuddy.Services.Catalog.API.Dtos;
using WatchBuddy.Services.Catalog.API.Models;
using WatchBuddy.Services.Catalog.API.Services.Category;
using WatchBuddy.Shared.ControllerBases;

namespace WatchBuddy.Services.Catalog.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController(ICategoryService categoryService) : CustomBaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await categoryService.GetAllAsync();
        return CreateActionResultInstance(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var result = await categoryService.GetByIdAsync(id);
        return CreateActionResultInstance(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CategoryDto categoryDto)
    {
        var result = await categoryService.CreateAsync(categoryDto);
        return CreateActionResultInstance(result);
    }
}
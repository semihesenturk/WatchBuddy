using AutoMapper;
using MongoDB.Driver;
using WatchBuddy.Services.Catalog.API.Dtos;
using WatchBuddy.Services.Catalog.API.Settings;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.Services.Catalog.API.Services.Category;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Models.Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings databaseSettings)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _categoryCollection = database.GetCollection<Models.Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<BaseServiceResponse<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(category => true).ToListAsync();
        var mappedCategories = _mapper.Map<List<CategoryDto>>(categories);

        return BaseServiceResponse<List<CategoryDto>>.Success(mappedCategories, 200);
    }

    public async Task<BaseServiceResponse<CategoryDto>> GetByIdAsync(string id)
    {
        var category = await _categoryCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
        return category is null
            ? BaseServiceResponse<CategoryDto>.Fail("Category not found", 404)
            : BaseServiceResponse<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }

    public async Task<BaseServiceResponse<CategoryDto>> CreateAsync(CategoryDto categoryDto)
    {
        var category = _mapper.Map<Models.Category>(categoryDto);
        await _categoryCollection.InsertOneAsync(category);

        return BaseServiceResponse<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
    }
}
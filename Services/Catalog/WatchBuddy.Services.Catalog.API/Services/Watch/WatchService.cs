using AutoMapper;
using MongoDB.Driver;
using WatchBuddy.Services.Catalog.API.Dtos;
using WatchBuddy.Services.Catalog.API.Settings;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.Services.Catalog.API.Services.Watch;

public class WatchService : IWatchService
{
    private readonly IMongoCollection<Models.Watch> _watchCollection;
    private readonly IMongoCollection<Models.Category> _categoryCollection;
    private readonly IMapper _mapper;

    public WatchService(IDatabaseSettings databaseSettings, IMapper mapper)
    {
        var client = new MongoClient(databaseSettings.ConnectionString);
        var database = client.GetDatabase(databaseSettings.DatabaseName);
        _watchCollection = database.GetCollection<Models.Watch>(databaseSettings.WatchCollectionName);
        _categoryCollection = database.GetCollection<Models.Category>(databaseSettings.CategoryCollectionName);
        _mapper = mapper;
    }

    public async Task<BaseServiceResponse<List<WatchDto>>> GetAllAsync()
    {
        var watches = await _watchCollection.Find(watch => true).ToListAsync();

        if (watches.Count != 0)
        {
            foreach (var watch in watches)
            {
                watch.Category = await _categoryCollection.Find(c => c.Id == watch.CategoryId).FirstAsync();
            }
        }
        else
        {
            watches = [];
        }

        return BaseServiceResponse<List<WatchDto>>.Success(_mapper.Map<List<WatchDto>>(watches), 200);
    }

    public async Task<BaseServiceResponse<WatchDto>> GetByIdAsync(string id)
    {
        var watch = await _watchCollection.Find(watch => watch.Id == id).FirstOrDefaultAsync();

        if (watch == null) return BaseServiceResponse<WatchDto>.Fail("Watch not found", 404);

        watch.Category = await _categoryCollection.Find(c => c.Id == watch.CategoryId).FirstAsync();
        return BaseServiceResponse<WatchDto>.Success(_mapper.Map<WatchDto>(watch), 200);
    }

    public async Task<BaseServiceResponse<List<WatchDto>>> GetAllByUserId(string userId)
    {
        var watches = await _watchCollection.Find(watch => watch.UserId == userId).ToListAsync();

        if (watches.Count != 0)
        {
            foreach (var watch in watches)
            {
                watch.Category = await _categoryCollection.Find(c => c.Id == watch.CategoryId).FirstAsync();
            }
        }
        else
        {
            watches = [];
        }

        return BaseServiceResponse<List<WatchDto>>.Success(_mapper.Map<List<WatchDto>>(watches), 200);
    }

    public async Task<BaseServiceResponse<WatchDto>> CreateAsync(WatchCreateDto watch)
    {
        var watchEntity = _mapper.Map<Models.Watch>(watch);
        watchEntity.CreatedTime = DateTime.UtcNow;
        await _watchCollection.InsertOneAsync(watchEntity);
        return BaseServiceResponse<WatchDto>.Success(_mapper.Map<WatchDto>(watchEntity), 200);
    }

    public async Task<BaseServiceResponse<NoContent>> UpdateAsync(WatchUpdateDto watch)
    {
        var watchEntity = _mapper.Map<Models.Watch>(watch);
        var result = await _watchCollection.FindOneAndReplaceAsync(x => x.Id == watch.Id, watchEntity);
        return result is null
            ? BaseServiceResponse<NoContent>.Fail("Watch not found", 404)
            : BaseServiceResponse<NoContent>.Success(204);
    }

    public async Task<BaseServiceResponse<NoContent>> DeleteAsync(string id)
    {
        var result = await _watchCollection.DeleteOneAsync(x => x.Id == id);
        return result.DeletedCount > 0
            ? BaseServiceResponse<NoContent>.Success(204)
            : BaseServiceResponse<NoContent>.Fail("Watch not found", 404);
    }
}
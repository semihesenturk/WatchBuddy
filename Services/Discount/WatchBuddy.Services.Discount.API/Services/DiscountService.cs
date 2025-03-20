using System.Data;
using Dapper;
using Npgsql;
using WatchBuddy.Shared.Dtos;

namespace WatchBuddy.Services.Discount.API.Services;

public class DiscountService(IConfiguration configuration) : IDiscountService
{
    private readonly IDbConnection _db = new NpgsqlConnection(configuration.GetConnectionString("PostgreSql"));

    public async Task<BaseServiceResponse<List<Models.Discount>>> GetAll()
    {
        var discountList = await _db.QueryAsync<Models.Discount>("SELECT * FROM Discount");
        return BaseServiceResponse<List<Models.Discount>>.Success(discountList.ToList(), 200);
    }

    public async Task<BaseServiceResponse<Models.Discount>> GetById(int id)
    {
        var discount = (await _db.QueryAsync<Models.Discount>("SELECT * FROM Discount WHERE Id = @Id", new { Id = id }))
            .SingleOrDefault();
        return discount == null
            ? BaseServiceResponse<Models.Discount>.Fail("Discount not found", 404)
            : BaseServiceResponse<Models.Discount>.Success(discount, 200);
    }

    public async Task<BaseServiceResponse<NoContent>> CreateDiscount(Models.Discount discount)
    {
        var saveStatus =
            await _db.ExecuteAsync("INSERT INTO Discount(userid, rate, code) VALUES (@userID, @Rate, @Code)", discount);

        return saveStatus == 0
            ? BaseServiceResponse<NoContent>.Fail("Discount couldn't be saved", 404)
            : BaseServiceResponse<NoContent>.Success(204);
    }

    public async Task<BaseServiceResponse<NoContent>> UpdateDiscount(Models.Discount discount)
    {
        var updateStatus = await _db.ExecuteAsync(
            "UPDATE Discount SET userid=@UserId, code=@Code, rate = @Rate WHERE id = @Id",
            new { Id = discount.Id, UserId = discount.UserId, Code = discount.Code, Rate = discount.Rate });

        return updateStatus == 0
            ? BaseServiceResponse<NoContent>.Fail("Discount couldn't be updated", 500)
            : BaseServiceResponse<NoContent>.Success(204);
    }

    public async Task<BaseServiceResponse<NoContent>> DeleteDiscount(int id)
    {
        var deleteStatus = await _db.ExecuteAsync(
            "delete from discount where id=@Id", 
            new { Id = id });
        
        return deleteStatus == 0
            ? BaseServiceResponse<NoContent>.Fail("Discount couldn't be deleted", 404)
            : BaseServiceResponse<NoContent>.Success(204);
    }

    public async Task<BaseServiceResponse<Models.Discount>> GetByCodeAndUserId(string code, string userId)
    {
        var discount = (await _db.QueryAsync<Models.Discount>(
            "select * from Discount where code=@Code and userid=@UserId", 
            new { Code = code, UserId = userId })).SingleOrDefault();
        
        return discount == null
            ? BaseServiceResponse<Models.Discount>.Fail("Discount not found", 404)
            : BaseServiceResponse<Models.Discount>.Success(discount, 200);
    }
}
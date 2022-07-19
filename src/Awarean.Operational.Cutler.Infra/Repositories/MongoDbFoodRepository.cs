using Awarean.Operational.Cutler.Application.Repositories;
using Awarean.Operational.Cutler.Domain;
using Awarean.Operational.Cutler.Infra.Configurations;
using Awarean.Operational.Cutler.Infra.Constants;
using Awarean.Operational.Cutler.Infra.Models;
using Awarean.Sdk.Result;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Awarean.Operational.Cutler.Infra.Repositories;
public class MongoDbFoodRepository : IFoodRepository
{
    private readonly ILogger<MongoDbFoodRepository> _logger;
    private readonly DatabaseConfiguration _options;
    private readonly IMongoCollection<FoodModel> _foods;

    public MongoDbFoodRepository(ILogger<MongoDbFoodRepository> logger, IOptionsMonitor<DatabaseConfiguration> options, IMongoClient client)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _options = options?.Get(DatabaseConstants.FoodsDatabase) ?? throw new ArgumentNullException(nameof(options));
        var database = client?.GetDatabase(_options.Name) ?? throw new ArgumentNullException(nameof(client));
        _foods = database.GetCollection<FoodModel>(_options.CollectionName);
        FoodModel.WithIdFormation(_options);
    }

    public async Task<Result<Food>> GetFoodById(string id)
    {
        var objectId = _options.BuildObjectID(id);
        var queried = await _foods.FindAsync(x => x.FdcId.ToString() == objectId);

        var result = await queried.AnyAsync()
            ? Result.Success(await queried.FirstAsync() as Food)
            : Result.Fail<Food>("FOOD_NOT_FOUND", "Did not found any food for supplied info");

        return result;
    }

    public async Task<Result<IEnumerable<object>>> GetFoods<T>(T query)
    {
        var cursor = await _foods.FindAsync<FoodModel>(new FilterDefinitionBuilder<FoodModel>().Where(x => x.FdcId != 0));

        
    }

    public async Task<Result<string>> SaveAsync(Food food)
    {
        var model = new FoodModel(food);

        try
        {
            await _foods.InsertOneAsync(model);
            return Result.Success(model.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error inserting food in database. Exception {exception}", ex);
            return Result.Fail<string>("SAVE_ERROR", $"Unnable to insert food in database {ex.Message}.");
        }
    }

    public async Task<Result> SaveBatchAsync(IEnumerable<Food> foods)
    {
        try
        {
            var models = foods as IEnumerable<FoodModel> ?? foods.Select(x => new FoodModel(x));
            await _foods.InsertManyAsync(models);
            return Result.Success();
        }
        catch (MongoBulkWriteException bulkException)
        {
            var models = foods.Except(
                foods.Where(
                    food => bulkException.WriteErrors
                        .Any(error => error.Category == ServerErrorCategory.DuplicateKey
                            && error.Message.Contains(food.FdcId.ToString())))).Select(x => new FoodModel(x));

            _logger.LogError("BulkWriteException Happened Exception Occured: {exception}", bulkException);
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception Occured: {exception}", ex);
            _logger.LogError("An error ocurred inserting batch in database {exception}", ex);
        }

        return Result.Fail("FOOD_BATCH_INSERT_ERROR", "Could not insert food batch in database");
    }
}

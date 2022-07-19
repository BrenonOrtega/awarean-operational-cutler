using System.Text.Json;
using System.Text.Json.Serialization;
using Awarean.Operational.Cutler.Application.Repositories;
using Awarean.Operational.Cutler.Infra.Models;
using MongoDB.Driver;

internal class MongoDbInserter : BackgroundService
{
    private readonly IFoodRepository _repo;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly ILogger<MongoDbInserter> _logger;
    private readonly string _filePath;
    public const string FOOD_DEFAULT_CONFIGURATION_PATH = "foodFilePath";

    public MongoDbInserter(ILogger<MongoDbInserter> logger, IFoodRepository repo, IConfiguration configuration)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        _filePath = configuration.GetValue<string>(FOOD_DEFAULT_CONFIGURATION_PATH) ?? throw new ArgumentException($"Food File Path not provided, please add it in configurations under {FOOD_DEFAULT_CONFIGURATION_PATH} section");
        _jsonOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString | JsonNumberHandling.WriteAsString
        };
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Yield();
        await DoStuffAsync();
    }

    private async Task DoStuffAsync()
    {
        var reader = File.OpenText(_filePath);
        var batch = new List<FoodModel>();
        var counter = 0;

        reader.ReadLine();

        while (reader.EndOfStream is false)
        {
            var line = await reader.ReadLineAsync();

            line = line?.Remove(line.Length - 1);

            var food = JsonSerializer.Deserialize<FoodModel>(line, _jsonOptions);

            _logger.LogInformation("Adding food to insert batch: {foodId} - {foodName} - {brandOwner} {foodType}.", food.Id, food.BrandedFoodCategory, food.BrandOwner, food.FoodClass);

            batch.Add(food);
            counter++;
            
            if (counter >= 100)
            {
                await _repo.SaveBatchAsync(batch);
                batch = new List<FoodModel>();
                counter = 0;
            }
        }
    }
}
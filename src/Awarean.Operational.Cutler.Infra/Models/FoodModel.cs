using Awarean.Operational.Cutler.Domain;
using Awarean.Operational.Cutler.Infra.Configurations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Awarean.Operational.Cutler.Infra.Models;

public class FoodModel : Food
{
    private static string _entryIdPrefix;
    private static bool _configured = false;
    private static Func<string, string> _buildBsonId;

    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public string Id => _buildBsonId?.Invoke(Id);

    public FoodModel() : base()
    {
        ThrowIfNotConfigured();
    }

    public FoodModel(Food food)
    {
        FoodClass = food.FoodClass;
        Description = food.Description;
        FoodNutrients = food.FoodNutrients;
        FoodAttributes = food.FoodAttributes;
        ModifiedDate = food.ModifiedDate;
        AvailableDate = food.AvailableDate;
        MarketCountry = food.MarketCountry;
        BrandOwner = food.BrandOwner;
        GtinUpc = food.GtinUpc;
        DataSource = food.DataSource;
        Ingredients = food.Ingredients;
        ServingSize = food.ServingSize;
        ServingSizeUnit = food.ServingSizeUnit;
        LabelNutrients = food.LabelNutrients;
        BrandedFoodCategory = food.BrandedFoodCategory;
        FdcId = food.FdcId;
        DataType = food.DataType;
        FoodUpdateLog = food.FoodUpdateLog;
        PublicationDate = food.PublicationDate;
        ThrowIfNotConfigured();
    }

    public static void WithIdFormation(IIdFormator formator)
    {
        _entryIdPrefix = formator.EntityPrefix;
        _buildBsonId = formator.BuildObjectID;
        _configured = true;
    }

    private static string ThrowIfNotConfigured()
    {
        if (_configured is false)
            throw new InvalidOperationException("Cannot Instantiate an Entity before using FoodModel.WithIdFormation(string) to configure Id Formation");

        return _entryIdPrefix;
    }
}
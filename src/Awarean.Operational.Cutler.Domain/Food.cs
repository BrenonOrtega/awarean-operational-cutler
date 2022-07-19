namespace Awarean.Operational.Cutler.Domain;

public class Food
{
    public string FoodClass { get; set; }
    public string Description { get; set; }
    public List<FoodNutrient> FoodNutrients { get; set; } = new();
    public List<FoodAttribute> FoodAttributes { get; set; } = new();
    public string ModifiedDate { get; set; }
    public string AvailableDate { get; set; }
    public string MarketCountry { get; set; }
    public string BrandOwner { get; set; }
    public string GtinUpc { get; set; }
    public string DataSource { get; set; }
    public string Ingredients { get; set; }
    public double ServingSize { get; set; }
    public string ServingSizeUnit { get; set; }
    public LabelNutrients LabelNutrients { get; set; } = new();
    public string BrandedFoodCategory { get; set; }
    public double FdcId { get; set; }
    public string DataType { get; set; }
    public List<FoodUpdateLog> FoodUpdateLog { get; set; } = new();
    public string PublicationDate { get; set; }
}

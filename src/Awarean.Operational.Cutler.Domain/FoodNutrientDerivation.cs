namespace Awarean.Operational.Cutler.Domain;

public class FoodNutrientDerivation
{
    public string code { get; set; }
    public string description { get; set; }
    public FoodNutrientSource foodNutrientSource { get; set; } = new();
}

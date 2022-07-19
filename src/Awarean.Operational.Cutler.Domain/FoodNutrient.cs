namespace Awarean.Operational.Cutler.Domain;

public class FoodNutrient
{
    public string type { get; set; }
    public double id { get; set; }
    public Nutrient nutrient { get; set; } = new();
    public FoodNutrientDerivation foodNutrientDerivation { get; set; } = new();
    public double amount { get; set; }
}

namespace Awarean.Operational.Cutler.Domain.Macronutrients;

public abstract class Macronutrient
{
    public double Value { get; set; }
    public abstract double CaloriesPerGram { get; }

    public double TotalCalories => Value * CaloriesPerGram;
}
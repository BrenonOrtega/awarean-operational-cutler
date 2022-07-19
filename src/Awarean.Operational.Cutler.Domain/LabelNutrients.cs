using Awarean.Operational.Cutler.Domain.Macronutrients;
using Awarean.Operational.Cutler.Domain.Micronutrients;

namespace Awarean.Operational.Cutler.Domain;

public class LabelNutrients
{
    public Fat fat { get; set; }
    public SaturatedFat saturatedFat { get; set; }
    public TransFat transFat { get; set; }
    public Cholesterol cholesterol { get; set; }
    public Sodium sodium { get; set; }
    public Carbohydrates carbohydrates { get; set; }
    public Fiber fiber { get; set; }
    public Sugars sugars { get; set; }
    public Protein protein { get; set; }
    public Calcium calcium { get; set; }
    public Iron iron { get; set; }
    public Calories calories { get; set; }
}

using System.Collections.Generic;

namespace Awarean.Operational.Cutler.Domain;

public class FoodUpdateLog
{
    public string foodClass { get; set; }
    public string description { get; set; }
    public List<FoodAttribute> foodAttributes { get; set; } = new();
    public double fdcId { get; set; }
    public string dataType { get; set; }
    public string publicationDate { get; set; }
}

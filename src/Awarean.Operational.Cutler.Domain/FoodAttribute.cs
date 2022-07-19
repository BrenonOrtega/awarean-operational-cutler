namespace Awarean.Operational.Cutler.Domain;

public class FoodAttribute
{
    public double id { get; set; }
    public string name { get; set; }
    public string value { get; set; }
    public FoodAttributeType foodAttributeType { get; set; }
}

public class FoodAttributeType
{
    public double id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
}
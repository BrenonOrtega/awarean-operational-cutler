namespace Awarean.Operational.Cutler.Infra.Configurations
{
    public interface IIdFormator
    {
        string EntityPrefix { get; set; }
        string BuildObjectID(string id);
    }
}
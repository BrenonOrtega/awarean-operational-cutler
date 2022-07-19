namespace Awarean.Operational.Cutler.Infra.Configurations;

public class DatabaseConfiguration : IIdFormator
{
    public string Name { get; set; }
    public string CollectionName { get; set; }
    public string EntityPrefix { get; set; }

    public string BuildObjectID(string id) => (id?.StartsWith(EntityPrefix) ?? false) ? id : EntityPrefix + id;
}
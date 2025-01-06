namespace Model;

public class CosmosDBSettings
{
    public const string SETTINGS_NAME = "CosmosDb";

    public string ConnectionString { get; set; } = string.Empty;
    public string Database { get; set; } = string.Empty;
    public string ContainerName { get; set; } = string.Empty;
}

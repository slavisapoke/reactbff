using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model;
using Ports;

namespace CosmosDbRepository;

public class FoodRepository : IFoodRepository
{
    readonly CosmosDBSettings _dbSettings;
    readonly ILogger<FoodRepository> _logger;


    public FoodRepository(IOptions<CosmosDBSettings> dbSettings,
        ILogger<FoodRepository> logger)
    {
        _dbSettings = dbSettings.Value;
        _logger = logger;
    }

    public async Task<List<Food>> GetFoodByType(string type)
    {
        try
        {
            List<Food> result = [];

            var container = GetContainerClient();
            var query = $"SELECT * FROM c WHERE c.type = \"{type}\" and c.soldOut  = false";

            QueryDefinition queryDef = new QueryDefinition(query);
            FeedIterator<Food> resultSetIterator = container.GetItemQueryIterator<Food>(queryDef);

            while (resultSetIterator.HasMoreResults)
            {
                FeedResponse<Food> foodResponse = await resultSetIterator.ReadNextAsync();
                foreach (Food food in foodResponse)
                {
                    result.Add(food);
                }
            }

            return result;
        }
        catch(Exception ex)
        {
            _logger.LogError(ex, "Failed to retrieve food by type: " + type);
            return [];
        }
    }

    public async Task<Food?> Insert(Food item)
    {
        try
        {
            var container = GetContainerClient();

            var result = await container.CreateItemAsync(item, new PartitionKey(item.Type));
            if (result.StatusCode is System.Net.HttpStatusCode.OK or System.Net.HttpStatusCode.Created)
            {
                return result.Resource;
            }

            _logger.LogWarning("Inserting new food resulted with status: " + result.StatusCode);
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to insert new Food into CosmosDb");
            return null;
        }
    }

    private Container GetContainerClient()
    {
        var cosmosDbClient = new CosmosClient(_dbSettings.ConnectionString);
        var container = cosmosDbClient.GetContainer(_dbSettings.Database, _dbSettings.ContainerName);
        return container;
    }
}

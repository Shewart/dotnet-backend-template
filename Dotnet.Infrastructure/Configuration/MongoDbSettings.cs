namespace Dotnet.Infrastructure.Configuration;

public class MongoDbSettings
{
    public required string DatabaseName { get; set; }
    public required MongoCollections Collections { get; set; }
}

public class MongoCollections
{
    public required string Products { get; set; }
}

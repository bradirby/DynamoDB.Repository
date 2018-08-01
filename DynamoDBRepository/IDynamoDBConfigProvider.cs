using Amazon.DynamoDBv2;

namespace DynamoDB.Repository
{
    public interface IDynamoDBConfigProvider
    {
        AmazonDynamoDBConfig GetConfig();
    }
}
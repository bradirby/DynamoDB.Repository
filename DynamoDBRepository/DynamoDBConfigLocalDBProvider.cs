using Amazon.DynamoDBv2;

namespace DynamoDB.Repository
{
    /// <summary>
    /// This will use the local DynamoDB installation ( http://localhost:8000)
    /// </summary>
    public class DynamoDBConfigLocalDBProvider : IDynamoDBConfigProvider
    {

        public AmazonDynamoDBConfig GetConfig()
        {
            var ddbConfig = new AmazonDynamoDBConfig {ServiceURL = "http://localhost:8000"};
            return ddbConfig;
        }


    }
}

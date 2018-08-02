using Amazon;
using Amazon.DynamoDBv2;

namespace DynamoDB.Repository
{
    /// <summary>
    /// This will use the Default credentials in the AWS credentials file (C:\Users\{username}\.aws)
    /// </summary>
    public class DynamoDBConfigDefaultUserProvider : IDynamoDBConfigProvider
    {
 
        public AmazonDynamoDBConfig GetConfig()
        {
            var ddbConfig = new AmazonDynamoDBConfig {RegionEndpoint = RegionEndpoint.USEast1};
            return ddbConfig;
        }

    }
}

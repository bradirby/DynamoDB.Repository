using Amazon;
using Amazon.DynamoDBv2;

namespace DynamoDB.Repository
{

    /// <summary>
    /// This will use the Default credentials in the AWS credentials file (C:\Users\{username}\.aws)
    /// </summary>
    public class DynamoDbConfigFactory 
    {
 
        public static AmazonDynamoDBConfig GetConfigForEndpoint(RegionEndpoint endPoint)
        {
            return new AmazonDynamoDBConfig {RegionEndpoint = endPoint};
        }

        public static AmazonDynamoDBConfig GetConfigForLocalSimulator()
        {
            return new AmazonDynamoDBConfig {ServiceURL = "http://localhost:8000"};
        }


    }
}

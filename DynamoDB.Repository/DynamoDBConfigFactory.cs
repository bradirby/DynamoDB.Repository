using Amazon;
using Amazon.DynamoDBv2;

namespace DynamoDB.Repository
{

    /// <summary>
    /// This will use the Default credentials in the AWS credentials file (C:\Users\{username}\.aws)
    /// </summary>
    public static class DynamoDbConfigFactory 
    {
 
        public static AmazonDynamoDBConfig GetConfigForEndpoint(RegionEndpoint endPoint)
        {
            return new AmazonDynamoDBConfig {RegionEndpoint = endPoint};
        }

        /// <summary>
        /// Go here for instructions on getting the local simulator running
        /// https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/DynamoDBLocal.DownloadingAndRunning.html
        /// </summary>
        /// <returns></returns>
        public static AmazonDynamoDBConfig GetConfigForLocalSimulator()
        {
            return new AmazonDynamoDBConfig {ServiceURL = "http://localhost:8000"};
        }


    }
}

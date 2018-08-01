using System;
using System.Collections.Generic;
using System.Text;
using Amazon;
using Amazon.DynamoDBv2;

namespace DynamoDB.Repository
{
    public class DynamoDBConfigProvider : IDynamoDBConfigProvider
    {

        public AmazonDynamoDBConfig GetConfig()
        {
            // First, set up a DynamoDB client for DynamoDB Local
            var ddbConfig = new AmazonDynamoDBConfig();
            //ddbConfig.ServiceURL = "http://localhost:8000";  use this instead of setting region endpoint to use a local dynamodb
            ddbConfig.RegionEndpoint = RegionEndpoint.USEast1;
            return ddbConfig;
        }


    }
}

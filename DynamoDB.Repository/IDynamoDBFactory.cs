using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.Repository
{
    public interface IDynamoDBFactory
    {
        void CreateTable(string tableName, IEnumerable<DynamoDBKeyDescriptor> descriptors, ProvisionedThroughput thruPut);
    }
}
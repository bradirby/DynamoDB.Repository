using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
[assembly: InternalsVisibleTo("DynamoDBRepository.UnitTests")]
[assembly: InternalsVisibleTo("DynamoDBRepository.IntgTests")]

namespace DynamoDB.Repository
{
    public class DynamoDBFactory : IDynamoDBFactory
    {
        private IDynamoDBConfigProvider ConfigProvider { get; set; }

        public DynamoDBFactory(IDynamoDBConfigProvider configProvider)
        {
            ConfigProvider = configProvider;
        }

        /// <summary>
        /// This retrieves the pre-existing table by name, using the given client
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public Table GetTableObject(string tableName) 
        {
            var client = GetClient();
            var table = Table.LoadTable(client, tableName);
            return table;
        }

        public AmazonDynamoDBClient GetClient()
        {
            var client = new AmazonDynamoDBClient(ConfigProvider.GetConfig());
            return client;
        }

        private CreateTableRequest GetCreateTableRequest(string tableName, List<AttributeDefinition> attributes,
            List<KeySchemaElement> schema)
        {
            var thru = new ProvisionedThroughput(1, 1);
            var createTableRequest = new CreateTableRequest(tableName, schema, attributes, thru);
            return createTableRequest;
        }

        internal CreateTableResponse CreateTable(string tableName, List<AttributeDefinition> attributes,
            List<KeySchemaElement> schema, AmazonDynamoDBClient client)
        {
            return CreateTableAsync(tableName, attributes, schema, client).Result;
        }


        internal Task<CreateTableResponse> CreateTableAsync(string tableName, List<AttributeDefinition> attributes,
            List<KeySchemaElement> schema, AmazonDynamoDBClient client)
        {
            var createRequest = GetCreateTableRequest(tableName, attributes, schema);
            var createResponse = client.CreateTableAsync(createRequest);
            return createResponse;
        }



    }
}

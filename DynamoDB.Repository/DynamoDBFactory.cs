using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
[assembly: InternalsVisibleTo("DynamoDB.Repository.UnitTests")]
[assembly: InternalsVisibleTo("DynamoDB.Repository.IntgTests")]

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
        internal Table GetTableObject(string tableName) 
        {
            var client = GetClient();
            var table = Table.LoadTable(client, tableName);
            return table;
        }

        internal AmazonDynamoDBClient GetClient()
        {
            var client = new AmazonDynamoDBClient(ConfigProvider.GetConfig());
            return client;
        }

        private CreateTableRequest GetCreateTableRequest(string tableName, IEnumerable<AttributeDefinition> attributes,
            IEnumerable<KeySchemaElement> schema, ProvisionedThroughput thruPut)
        {
            var createTableRequest = new CreateTableRequest(tableName, schema.ToList(), attributes.ToList(), thruPut);
            return createTableRequest;
        }


        internal Task<CreateTableResponse> CreateTableAsync(string tableName, IEnumerable<AttributeDefinition> attributes,
            IEnumerable<KeySchemaElement> schema, ProvisionedThroughput thruPut, AmazonDynamoDBClient client)
        {
            var createRequest = GetCreateTableRequest(tableName, attributes, schema, thruPut);
            var createResponse = client.CreateTableAsync(createRequest);
            return createResponse;
        }


        /// <summary>
        /// Adds a key descriptor used in creating a new table.  Unless you are creating a new table,
        /// the key descriptors are not necessary
        /// </summary>
        private void ValidateKeyDescriptors(IEnumerable<DynamoDBKeyDescriptor> descriptors)
        {
            if (descriptors == null) throw new ArgumentNullException(nameof(descriptors));
            var lst = descriptors.ToList();
            if (lst.ToList().Count > 2) throw new ArgumentOutOfRangeException("Only 2 Key Descriptors allowed");
            if (lst.ToList().Count < 2) throw new ArgumentOutOfRangeException("Must specify at least one key");
            if (lst.Count == 2 && lst[0].KeyType == lst[1].KeyType)
                throw new ArgumentOutOfRangeException("Only 1 of each keyType allowed");
        }

        public void CreateTable(string tableName, IEnumerable<DynamoDBKeyDescriptor> descriptors, ProvisionedThroughput thruPut)
        {
            ValidateKeyDescriptors(descriptors);
            var client = GetClient();
            var result = CreateTableAsync(tableName, GetKeyAttributes(descriptors), GetKeySchema(descriptors), thruPut, client).Result;
        }

        /// <summary>
        /// Returns the key schema which is used in table creation
        /// </summary>
        [DebuggerStepThrough]
        private List<KeySchemaElement> GetKeySchema(IEnumerable<DynamoDBKeyDescriptor> descriptors)
        {
            var keySchema = new List<KeySchemaElement>();
            foreach (var keyDesc in descriptors)
                keySchema.Add(new KeySchemaElement(keyDesc.Name, keyDesc.KeyType.ToAWS()));
            return keySchema;
        }

        /// <summary>
        /// Returns the key column field attributes, which are used in table creation
        /// </summary>
        [DebuggerStepThrough]
        private List<AttributeDefinition> GetKeyAttributes(IEnumerable<DynamoDBKeyDescriptor> descriptors)
        {
            var AttributeDefinitions = new List<AttributeDefinition>();
            foreach (var keyDesc in descriptors)
                AttributeDefinitions.Add(new AttributeDefinition(keyDesc.Name, keyDesc.FieldType.ToAWS()));
            return AttributeDefinitions;
        }

    }
}

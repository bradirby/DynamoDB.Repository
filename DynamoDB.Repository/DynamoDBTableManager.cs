using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;


namespace DynamoDB.Repository
{
    public interface IDynamoDBTableManager
    {



        CreateTableResponse CreateTable(string tableName, List<AttributeDefinition> attributes,
            List<KeySchemaElement> schema, ProvisionedThroughput thruPut);

        CreateTableResponse CreateTable(string tableName, IEnumerable<DynamoDBKeyDescriptor> descriptorLst,
            ProvisionedThroughput thruPut);

        Task<CreateTableResponse> CreateTableAsync(string tableName, IEnumerable<DynamoDBKeyDescriptor> descriptorLst,
            ProvisionedThroughput thruPut);

        Task<CreateTableResponse> CreateTableAsync(string tableName, List<AttributeDefinition> attributes,
            List<KeySchemaElement> schema, ProvisionedThroughput thruPut);

    }


    public class DynamoDBTableManager : IDynamoDBTableManager
    {
        private AmazonDynamoDBConfig Config{ get; set; }

        public DynamoDBTableManager(AmazonDynamoDBConfig config)
        {
            Config = config;
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
            return  Table.LoadTable(client, tableName);
        }

        internal AmazonDynamoDBClient GetClient()
        {
            return new AmazonDynamoDBClient(Config);
        }


        public CreateTableResponse CreateTable(string tableName, List<AttributeDefinition> attributes,
            List<KeySchemaElement> schema, ProvisionedThroughput thruPut)
        {
            return CreateTableAsync(tableName, attributes, schema, thruPut).GetAwaiter().GetResult();
        }

        public CreateTableResponse CreateTable(string tableName, IEnumerable<DynamoDBKeyDescriptor> descriptorLst, ProvisionedThroughput thruPut)
        {
            var descriptors = descriptorLst.ToList();
            ValidateKeyDescriptors(descriptors);
            return CreateTable(tableName, GetKeyAttributes(descriptors), GetKeySchema(descriptors), thruPut);
        }

        public async Task<CreateTableResponse> CreateTableAsync(string tableName, IEnumerable<DynamoDBKeyDescriptor> descriptorLst, ProvisionedThroughput thruPut)
        {
            var descriptors = descriptorLst.ToList();
            ValidateKeyDescriptors(descriptors);
            return await CreateTableAsync(tableName, GetKeyAttributes(descriptors), GetKeySchema(descriptors), thruPut);
        }

        public async Task<CreateTableResponse> CreateTableAsync(string tableName, List<AttributeDefinition> attributes,
            List<KeySchemaElement> schema, ProvisionedThroughput thruPut)
        {
            var client = GetClient();
            var createRequest = new CreateTableRequest(tableName, schema, attributes, thruPut);
            return await client.CreateTableAsync(createRequest);
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

using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using Newtonsoft.Json;

namespace DynamoDB.Repository
{
    public abstract class BaseDynamoDBTable<EntType>
    {
        [JsonIgnore]
        protected List<DynamoDBKeyDescriptor> KeyDescriptors { get; set; }

        [JsonIgnore]
        public string TableName { get; private set; }

        protected BaseDynamoDBTable(string tableName)
        {
            TableName = tableName;
            KeyDescriptors = new List<DynamoDBKeyDescriptor>();
        }

        public List<KeySchemaElement> GetKeys()
        {
            var lst = new List<KeySchemaElement>();
            foreach (var desc in KeyDescriptors)
                lst.Add(new KeySchemaElement(desc.Name, desc.KeyType.ToAWS()));
            return lst;
        }

        public List<AttributeDefinition> GetAttributes()
        {
            var lst = new List<AttributeDefinition>();
            foreach (var desc in KeyDescriptors)
                lst.Add(new AttributeDefinition(desc.Name, desc.FieldType.ToAWS()));
            return lst;
        }

        public  Dictionary<string, AttributeValue> GetUpdateKeys()
        {
            var lst = new Dictionary<string, AttributeValue>();
            foreach (var desc in KeyDescriptors)
                lst.Add(desc.Name, new AttributeValue(desc.KeyType.ToAWS()));
            return lst;
        }

        public abstract UpdateItemRequest GetUpdateRequest(EntType entity);
    }
}

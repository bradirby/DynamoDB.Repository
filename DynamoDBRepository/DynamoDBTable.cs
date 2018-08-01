using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.Repository
{
    public class DynamoDBTable<EntType>
    {
        public List<DynamoDBKeyDescriptor> KeyDescriptors { get; internal set; }

        public string TableName { get; private set; }

        public DynamoDBTable(string tableName)
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

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace DynamoDB.Repository
{
    internal class DynamoDBKeyDescriptor
    {
        public string Name { get;  }
        public DynamoDBKeyType KeyType { get; }
        public DynamoDBDataType FieldType { get; }

        public DynamoDBKeyDescriptor(string name, DynamoDBKeyType keyType, DynamoDBDataType fieldType)
        {
            Name = name;
            KeyType = keyType;
            FieldType = fieldType;
        }
    }
}

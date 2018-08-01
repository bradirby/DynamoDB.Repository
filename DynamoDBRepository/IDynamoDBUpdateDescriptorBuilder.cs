using System;
using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.Repository
{
    public interface IDynamoDBUpdateDescriptorBuilder
    {
        void AddUpdateValue(string fieldName, string newValue);
        void AddUpdateValue(string fieldName, DateTime newValue);
        void AddUpdateValue(string fieldName, double newValue);
        void AddUpdateValue(string fieldName, List<string> newValues);
        UpdateItemRequest GetUpdateItemRequest(string tableName, Dictionary<string, AttributeValue> updateKeys);
        void Clear();
    }
}
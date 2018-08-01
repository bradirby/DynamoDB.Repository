using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.Repository.IntgTests
{
    public interface IMovieDynTbl
    {
        Dictionary<string, DynamoDBEntry> GetRowKey(int year, string title);
        UpdateItemRequest GetUpdateRequest(Movie entity);
        string TableName { get; }
        List<KeySchemaElement> GetKeys();
        List<AttributeDefinition> GetAttributes();
        Dictionary<string, AttributeValue> GetUpdateKeys();
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.Repository
{
    public class DynamoDBUpdateDescriptorBuilder : IDynamoDBUpdateDescriptorBuilder
    {
        private StringBuilder SetQuery { get; set; } 

        private Dictionary<string, AttributeValue> UpdateValueExpressions { get; set; } 

        private int fieldCounter = 0;
        private string comma = "";

        public DynamoDBUpdateDescriptorBuilder()
        {
            Clear();
        }

        public void AddUpdateValue(string fieldName, string newValue)
        {
            AddFieldName(fieldName);
            UpdateValueExpressions.Add($":{fieldCounter}", new AttributeValue(newValue));
        }

        public void AddUpdateValue(string fieldName, DateTime newValue)
        {
            AddFieldName(fieldName);
            UpdateValueExpressions.Add($":{fieldCounter}", new AttributeValue(newValue.ToLongDateString()));
        }

        public void AddUpdateValue(string fieldName, double newValue)
        {
            AddFieldName(fieldName);
            UpdateValueExpressions.Add($":{fieldCounter}", new AttributeValue(newValue.ToString()));
        }

        private void AddFieldName(string fieldName)
        {
            fieldCounter += 1;
            SetQuery.Append($"{comma} {fieldName} = :{fieldCounter}");
            comma = ",";
        }

        public void AddUpdateValue(string fieldName, List<string> newValues)
        {
            AddFieldName(fieldName);
            UpdateValueExpressions.Add($":{fieldCounter}", new AttributeValue(newValues));
        }

        private string GetSetStatement()
        {
            return "SET " + SetQuery.ToString();
        }

        private Dictionary<string, AttributeValue> GetUpdateExpressions()
        {
            return UpdateValueExpressions;
        }

        public void Clear()
        {
            fieldCounter = 0;
            SetQuery = new StringBuilder();
            UpdateValueExpressions = new Dictionary<string, AttributeValue>();
        }

        public UpdateItemRequest GetUpdateItemRequest(string tableName, Dictionary<string, AttributeValue> updateKeys)
        {
            var expressions = GetUpdateExpressions();
            var statement = GetSetStatement();
            var updReq = new UpdateItemRequest
            {
                TableName = tableName,
                ExpressionAttributeValues = expressions,
                UpdateExpression = statement,
                Key = updateKeys
            };
            return updReq;
        }
    }
}

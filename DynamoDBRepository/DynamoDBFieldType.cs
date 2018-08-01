using System;
using System.Collections.Generic;
using System.Text;

namespace DynamoDB.Repository
{
    public enum DynamoDBDataType
    {
        String,
        Number,
        Boolean,
        ByteBuffer,
        Date,
        StringCollection,
        NumberCollection,
        BinaryCollection
    }

    public static class DynamoDBDataTypesExtensionMethods
    {
        public static string ToAWS(this DynamoDBDataType typ)
        {
            switch (typ)
            {
                case DynamoDBDataType.Boolean: return "BOOL";
                case DynamoDBDataType.BinaryCollection: return "BS";
                case DynamoDBDataType.ByteBuffer: return "B";
                case DynamoDBDataType.Date: return "S";
                case DynamoDBDataType.Number: return "N";
                case DynamoDBDataType.NumberCollection: return "NS";
                case DynamoDBDataType.String: return "S";
                case DynamoDBDataType.StringCollection: return "SS";
                default: throw new ArgumentOutOfRangeException("Invalid DynamoDBDataTypes");
            }
        }
    }
}

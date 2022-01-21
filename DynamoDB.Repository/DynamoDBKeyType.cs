using System;
using System.Collections.Generic;
using System.Text;

namespace DynamoDBRepository
{
    public enum DynamoDBKeyType
    {
        Hash,
        Range
    }

    public static class DynamoDBKeyTypeExtensionMethods
    {
        public static string ToAWS(this DynamoDBKeyType typ)
        {
            switch (typ)
            {
                case DynamoDBKeyType.Hash: return "HASH";
                case DynamoDBKeyType.Range: return "RANGE";
                default: throw new ArgumentOutOfRangeException("Invalid DynamoDBKeyType");
            }
        }
    }

}

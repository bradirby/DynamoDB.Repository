using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDB.Repository
{
    public interface IDynamoDBFactory
    {
        AmazonDynamoDBClient GetClient();

        /// <summary>
        /// This retrieves the pre-existing table by name, using the given client
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        Table GetTableObject(string tableName);
    }
}
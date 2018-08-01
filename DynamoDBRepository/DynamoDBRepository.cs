using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace DynamoDB.Repository
{
    public abstract class DynamoDBRepository<EntType>
    {
        protected Table DynamoTable { get; set; }

        private List<DynamoDBKeyDescriptor> KeyDescriptors { get;  }

        private string TableName { get; }

        protected void AddKeyDescriptor(string keyName,DynamoDBKeyType keyType, DynamoDBDataType dataType)
        {
            KeyDescriptors.Add(new DynamoDBKeyDescriptor( keyName, keyType, dataType));
        }

        protected DynamoDBRepository(string tableName, IDynamoDBConfigProvider configProvider)
        {
            TableName = tableName;
            KeyDescriptors = new List<DynamoDBKeyDescriptor>();

            var factory = new DynamoDBFactory(configProvider);
            DynamoTable = factory.GetTableObject(TableName);
        }

        /// <summary>
        /// Writes the given object to the DynamoDB table.  Property name are case sensitive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="tbl"></param>
        private Task<Document> WriteToTableAsync(EntType item)
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var doc = Document.FromJson(itemJson);
            return DynamoTable.PutItemAsync(doc);
        }

        /// <summary>
        /// Updates or Inserts the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<Document> Update(EntType item)
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var doc = Document.FromJson(itemJson);
            return DynamoTable.UpdateItemAsync(doc);
        }

        /// <summary>
        /// Inserts the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Document Insert(EntType item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return WriteToTableAsync(item).Result;
        }

        public EntType GetByKey(Dictionary<string, DynamoDBEntry> key)
        {
            var result = DynamoTable.GetItemAsync(key);
            if (result?.Result == null) return default(EntType);

            var movie = JsonConvert.DeserializeObject<EntType>(result.Result.ToJson());
            return movie;

        }

    }
}

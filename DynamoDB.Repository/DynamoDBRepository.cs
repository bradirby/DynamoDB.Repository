using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace DynamoDB.Repository
{
    public abstract class DynamoDBRepository<EntType> : IDynamoDBRepository<EntType>
    {
        private Table DynamoTable { get; set; }


        private DynamoDBFactory Factory { get; set; }

        private string TableName { get; }



        protected DynamoDBRepository(string tableName, IDynamoDBConfigProvider configProvider)
        {
            TableName = tableName;

            Factory = new DynamoDBFactory(configProvider);
            DynamoTable = Factory.GetTableObject(TableName);
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
        public Task<Document> UpdateAsync(EntType item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var itemJson = JsonConvert.SerializeObject(item);
            var doc = Document.FromJson(itemJson);
            return DynamoTable.UpdateItemAsync(doc); 
        }

        /// <summary>
        /// Updates or Inserts the given item
        /// </summary>
        public void Update(EntType item)
        {
            var result = UpdateAsync(item).Result;  //this forces a wait until the operation returns
        }

        /// <summary>
        /// Inserts the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public Task<Document> InsertAsync(EntType item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return WriteToTableAsync(item);
        }

        /// <summary>
        /// Inserts the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void Insert(EntType item)
        {
            var result = InsertAsync(item).Result;
        }

        public Task<Document> GetByKeyAsync(Dictionary<string, DynamoDBEntry> key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            return DynamoTable.GetItemAsync(key);
        }

        public EntType GetByKey(Dictionary<string, DynamoDBEntry> key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            var result = GetByKeyAsync(key);
            if (result?.Result == null) return default(EntType);

            var movie = JsonConvert.DeserializeObject<EntType>(result.Result.ToJson());
            return movie;
        }


     
    }
}

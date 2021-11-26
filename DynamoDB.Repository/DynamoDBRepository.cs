using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Newtonsoft.Json;

namespace DynamoDB.Repository
{
    public abstract class DynamoDBRepository<EntType> : IDynamoDBRepository<EntType>
    {
        private Table DynamoTable
        {
            get => _table ?? (_table = TableManager.GetTableObject(TableName));
            set => _table = value;
        }

        private Table _table;

        private DynamoDBTableManager TableManager { get; set; }

        private string TableName { get; }


        public DynamoDBRepository(string tableName, DynamoDBTableManager tableMgr)
        {
            TableName = tableName;
            TableManager = tableMgr;
        }

        public DynamoDBRepository(string tableName, Table table)
        {
            TableName = tableName;
            _table = table;
        }

        
        /// <summary>
        /// Writes the given object to the DynamoDB table.  Property name are case sensitive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="tbl"></param>
        private async Task<Document> WriteToTableAsync(EntType item)
        {
            var itemJson = JsonConvert.SerializeObject(item);
            var doc = Document.FromJson(itemJson);
            return await DynamoTable.PutItemAsync(doc);
        }

        /// <summary>
        /// Updates or Inserts the given item
        /// </summary>
        public async Task<Document> UpdateAsync(EntType item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            var itemJson = JsonConvert.SerializeObject(item);
            var doc = Document.FromJson(itemJson);
            return await DynamoTable.UpdateItemAsync(doc); 
        }

        /// <summary>
        /// Updates or Inserts the given item
        /// </summary>
        public Document Update(EntType item)
        {
            return UpdateAsync(item).GetAwaiter().GetResult();
        }


        /// <summary>
        /// Inserts the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<Document> InsertAsync(EntType item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            return await WriteToTableAsync(item);
        }

        /// <summary>
        /// Inserts the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public void Insert(EntType item)
        {
            var result = InsertAsync(item).GetAwaiter().GetResult();
        }

        //public async Task<Document> GetByKeyAsync(Dictionary<string, DynamoDBEntry> key)
        //{
        //    if (key == null) throw new ArgumentNullException(nameof(key));
        //    return await DynamoTable.GetItemAsync(key);
        //}

        public EntType GetByKey(Dictionary<string, DynamoDBEntry> key)
        {
            return GetByKeyAsync(key).GetAwaiter().GetResult();
        }

        public async Task<EntType> GetByKeyAsync(Dictionary<string, DynamoDBEntry> key)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            var item = await DynamoTable.GetItemAsync(key);
            if (item == null) return default(EntType);
            return  JsonConvert.DeserializeObject<EntType>(item.ToJson());
        }


     
    }
}

using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDBRepository
{
    public interface IDynamoDBRepository<EntType>
    {
        /// <summary>
        /// Updates or Inserts the given item
        /// </summary>
        Document Update(EntType item);
        Task<Document> UpdateAsync(EntType item);


        /// <summary>
        /// Inserts the given item
        /// </summary>
        void Insert(EntType item);
        Task<Document> InsertAsync(EntType item);


        /// <summary>
        /// Gets the appropriate record according to the key 
        /// </summary>
        Task<EntType> GetByKeyAsync(Dictionary<string, DynamoDBEntry> key);
        EntType GetByKey(Dictionary<string, DynamoDBEntry> key);
    }
}
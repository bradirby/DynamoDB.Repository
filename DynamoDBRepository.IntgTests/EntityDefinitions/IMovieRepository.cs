using System.Collections.Generic;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDB.Repository.IntgTests
{
    public interface IMovieRepository
    {
        Movie GetById(int year, string title);

        /// <summary>
        /// Updates or Inserts the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<Document> Update(Movie item);

        /// <summary>
        /// Inserts the given item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        Document Insert(Movie item);

        Movie GetByKey(Dictionary<string, DynamoDBEntry> key);
    }
}
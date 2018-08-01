using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;
using DynamoDB.Repository.TestWebApp.Models;

namespace DynamoDB.Repository.TestWebApp.DataAccess
{
    public class MovieRepository : DynamoDBRepository<Movie>, IMovieRepository
    {

        public MovieRepository(IDynamoDBConfigProvider configProvider) : base("Movies", configProvider)
        {
            AddKeyDescriptor("year", DynamoDBKeyType.Hash, DynamoDBDataType.Number);
            AddKeyDescriptor("title", DynamoDBKeyType.Range, DynamoDBDataType.String);
        }


        public Movie GetById(int year, string title)
        {
            var rowKey = GetRowKey(year, title);
            var movie = GetByKey(rowKey);
            return movie;
        }

        public Dictionary<string, DynamoDBEntry> GetRowKey(int year, string title)
        {
            var key = new Dictionary<string, DynamoDBEntry> { { "year", year }, { "title", title } };
            return key;
        }


    }
}

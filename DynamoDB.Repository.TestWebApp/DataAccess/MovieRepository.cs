using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;
using DynamoDB.Repository.TestWebApp.Models;

namespace DynamoDB.Repository.TestWebApp.DataAccess
{
    public class MovieRepository : DynamoDBRepository<Movie>, IMovieRepository
    {
        /// <summary>
        /// The only thing required for the repository is the name of the table and
        /// a Configuration Provider that can be resolved
        /// </summary>
        public MovieRepository(IDynamoDBConfigProvider configProvider) : base("Movies", configProvider)
        {
        }

        /// <summary>
        /// Sample custom method to get a row by its key properties.  This is not required in a repository.
        /// </summary>
        public Movie GetById(int year, string title)
        {
            var rowKey = new Dictionary<string, DynamoDBEntry> { { "year", year }, { "title", title } };
            var movie = GetByKey(rowKey);
            return movie;
        }


    }
}

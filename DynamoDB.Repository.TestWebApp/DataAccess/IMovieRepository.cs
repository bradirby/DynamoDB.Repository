using DynamoDB.Repository.TestWebApp.Models;

namespace DynamoDB.Repository.TestWebApp.DataAccess
{
    public interface IMovieRepository : IDynamoDBRepository<Movie>
    {
        /// <summary>
        /// This is a custom method - no method declarations are required here.
        /// </summary>
        Movie GetById(int year, string title);
    }
}
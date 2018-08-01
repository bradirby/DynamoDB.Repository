
using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;

namespace DynamoDB.Repository.IntgTests
{
    public class MovieRepository : BaseDynamoDBRepository<Movie>, IMovieRepository
    {
        private DynamoDBTable<Movie> MovieTable { get; set; }

        public MovieRepository(IDynamoDBFactory factory) : base(factory)
        {
            MovieTable = new DynamoDBTable<Movie>("Movies");
            DynamoTable = factory.GetTableObject(MovieTable.TableName);
        }

        public override void SetupKeyDescriptors()
        { 
            MovieTable.KeyDescriptors.Add(new DynamoDBKeyDescriptor("year", DynamoDBKeyType.Hash, DynamoDBDataType.Number));
            MovieTable.KeyDescriptors.Add(new DynamoDBKeyDescriptor("title", DynamoDBKeyType.Range, DynamoDBDataType.String));
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

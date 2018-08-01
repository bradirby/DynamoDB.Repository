
namespace DynamoDB.Repository.IntgTests
{
    public class MovieRepository : BaseDynamoDBRepository<Movie>, IMovieRepository
    {
        private IMovieDynTbl MovieTable { get; set; }
        private IDynamoDBUpdateDescriptorBuilder Builder { get; set; }

        public MovieRepository(IDynamoDBFactory factory, IDynamoDBUpdateDescriptorBuilder bldr, IMovieDynTbl tbl ) : base(factory)
        {
            Builder = bldr;
            MovieTable = tbl;
            DynamoTable = factory.GetTableObject(MovieTable.TableName);
        }

        public Movie GetById(int year, string title)
        {
            var rowKey = MovieTable.GetRowKey(year, title);
            var movie = GetByKey(rowKey);
            return movie;
        }

    }
}

using System.Collections.Generic;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;

namespace DynamoDB.Repository.IntgTests
{
    public class  MovieDynTbl : BaseDynamoDBTable<Movie>, IMovieDynTbl
    {
        public MovieDynTbl() : base("Movie2")
        {
            KeyDescriptors.Add(new DynamoDBKeyDescriptor("year", DynamoDBKeyType.Hash, DynamoDBDataType.Number));
            KeyDescriptors.Add(new DynamoDBKeyDescriptor("title", DynamoDBKeyType.Range, DynamoDBDataType.String));
        }

        public Dictionary<string, DynamoDBEntry> GetRowKey(int year, string title)
        {
            var key = new Dictionary<string, DynamoDBEntry> {{"year", year}, {"title", title}};
            return key;
        }

        public override UpdateItemRequest GetUpdateRequest(Movie entity)
        {
            var builder = new DynamoDBUpdateDescriptorBuilder ();
            builder.AddUpdateValue("info.directors", entity.Info.Directors);
            builder.AddUpdateValue("info.release_date", entity.Info.ReleaseDate);
            builder.AddUpdateValue("info.rating", entity.Info.Rating);
            builder.AddUpdateValue("info.genres", entity.Info.Genres);
            builder.AddUpdateValue("info.image_url", entity.Info.ImageUrl);
            builder.AddUpdateValue("info.plot", entity.Info.Plot);
            builder.AddUpdateValue("info.movierank", entity.Info.Rank);
            builder.AddUpdateValue("info.running_time_secs", entity.Info.RunningTime);
            builder.AddUpdateValue("info.actors", entity.Info.Actors);

            return builder.GetUpdateItemRequest(TableName, GetUpdateKeys());
        }

    }
}

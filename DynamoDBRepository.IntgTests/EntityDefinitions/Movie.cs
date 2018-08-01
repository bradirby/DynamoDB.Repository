using Newtonsoft.Json;

namespace DynamoDB.Repository.IntgTests
{
    public class  Movie 
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("info")]
        public MovieDetails Info { get; set; } = new MovieDetails();

    }
}

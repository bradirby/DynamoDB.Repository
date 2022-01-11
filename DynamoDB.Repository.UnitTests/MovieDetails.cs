using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DynamoDB.Repository.UnitTests
{
    public class MovieDetails
    {
        [JsonProperty("directors")]
        public List<string> Directors { get; set; } = new List<string>();

        [JsonProperty("release_date")]
        public DateTime ReleaseDate { get; set; }

        [JsonProperty("rating")]
        public double Rating { get; set; }

        [JsonProperty("genres")]
        public List<string> Genres { get; set; } = new List<string>();

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("plot")]
        public string Plot { get; set; }

        [JsonProperty("movierank")]
        public int Rank { get; set; }

        [JsonProperty("running_time_secs")]
        public int RunningTime { get; set; }

        [JsonProperty("actors")]
        public List<string> Actors { get; set; } = new List<string>();

    }
}
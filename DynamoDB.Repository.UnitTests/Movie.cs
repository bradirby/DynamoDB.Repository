using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DynamoDB.Repository.UnitTests
{
    public class Movie
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("info")]
        public MovieDetails Info { get; set; } = new MovieDetails();

    }
}

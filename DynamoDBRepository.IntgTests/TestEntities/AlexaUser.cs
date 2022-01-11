using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DynamoDB.Repository.IntgTests.TestEntities;
using Newtonsoft.Json;

namespace DynamoDB.Repository.IntgTests
{
    public class AlexaUser
    {
        public static string DynamoDBTableName = "AlexaUsers";
        public static string DynamoDBKeyFieldName = "alexa_user_id";


        [JsonProperty("alexa_user_id")]
        public string AlexaUserId{ get; set; }

        [JsonProperty("firstname")]
        public string FirstName { get; set; }

        [JsonProperty("lastname")]
        public string LastName { get; set; }

        [JsonProperty("myschool")]
        public MySchoolSkill MySchool { get; set; }

    }
}

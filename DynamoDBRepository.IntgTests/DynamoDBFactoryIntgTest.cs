using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using NUnit.Framework;

namespace DynamoDB.Repository.IntgTests
{
    public class DynamoDBFactoryIntgTest
    {

        public DynamoDBFactory sut { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var cfg = new DynamoDBConfigProvider();
            sut = new DynamoDBFactory(cfg);
        }


        [Test]
        public void CreateTable()
        {
            var client = sut.GetClient();
            var tblRequest = sut.CreateTableAsync("Movie3", GetMovieTableAttributes(), GetMovieTableSchema(), client);
            Assert.IsNotNull(tblRequest.Result);
        }

        private List<KeySchemaElement> GetMovieTableSchema()
        {
            var KeySchema = new List<KeySchemaElement>()
            {
                new KeySchemaElement
                {
                    AttributeName = "year",
                    KeyType = "HASH"
                },
                new KeySchemaElement
                {
                    AttributeName = "title",
                    KeyType = "RANGE"
                }
            };
            return KeySchema;

        }

        private List<AttributeDefinition> GetMovieTableAttributes()
        {
            var AttributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition
                {
                    AttributeName = "year",
                    AttributeType = "N"
                },
                new AttributeDefinition
                {
                    AttributeName = "title",
                    AttributeType = "S"
                }
            };
            return AttributeDefinitions;
        }

    }
}

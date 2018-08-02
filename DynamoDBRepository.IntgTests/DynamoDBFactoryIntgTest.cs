using System.Collections.Generic;
using Amazon.DynamoDBv2.Model;
using NUnit.Framework;

namespace DynamoDB.Repository.IntgTests
{
    public class DynamoDBFactoryIntgTest
    {
        private DynamoDBFactory Sut { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var cfg = new DynamoDBConfigDefaultUserProvider();
            Sut = new DynamoDBFactory(cfg);
        }


        [Test]
        [Explicit]
        public void CreateTable_ProperParams_CreatesTable()
        {
            var thru = new ProvisionedThroughput(1, 1);
            var keys = new List<DynamoDBKeyDescriptor>
            {
                new DynamoDBKeyDescriptor("year", DynamoDBKeyType.Hash, DynamoDBDataType.Number),
                new DynamoDBKeyDescriptor("title", DynamoDBKeyType.Range, DynamoDBDataType.String)
            };

            Sut.CreateTable("Movies", keys, thru);
        }



    }
}

using System;
using System.Collections.Generic;
using System.Text;
using Amazon.DynamoDBv2.Model;
using Moq;
using NUnit.Framework;

namespace DynamoDB.Repository.UnitTests
{
    public class DynamoDBFactoryUnitTest
    {
        private DynamoDBFactory Sut { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var mockConfig = new Mock<IDynamoDBConfigProvider>();
            Sut = new DynamoDBFactory(mockConfig.Object);
        }


        [Test]
        public void CanGetClient()
        {
            var c = Sut.GetClient();
            Assert.IsNotNull(c);
        }


    }
}

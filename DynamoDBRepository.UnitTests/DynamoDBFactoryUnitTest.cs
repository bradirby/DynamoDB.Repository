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

        public DynamoDBFactory sut { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var mockConfig = new Mock<IDynamoDBConfigProvider>();
            sut = new DynamoDBFactory(mockConfig.Object);
        }


        [Test]
        public void CanGetClient()
        {
            var c = sut.GetClient();
            Assert.IsNotNull(c);
        }


    }
}

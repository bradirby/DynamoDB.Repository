using System;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace DynamoDB.Repository.UnitTests
{
    public class DynamoDBConfigProviderUnitTest
    {
        private DynamoDBConfigDefaultUserProvider sut { get; set; }

        [Test]
        public void CanGetConfig()
        {
            var cfg = sut.GetConfig();
            Assert.IsNotNull(cfg);
        }

    }
}

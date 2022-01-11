﻿using System;
using Amazon.DynamoDBv2.Model;
using System.Collections.Generic;
using Amazon;
using Amazon.DynamoDBv2;
using DynamoDB.Repository.IntgTests.TestEntities;
using NUnit.Framework;

namespace DynamoDB.Repository.IntgTests
{
    
    public class LoggedInUserRepoTest
    {
        private LoggedInUserRepository Sut { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            //https://docs.aws.amazon.com/amazondynamodb/latest/developerguide/CodeSamples.DotNet.html#CodeSamples.DotNet.Credentials
            //setting up a lambda to have permission to access DynamoDB
            //    https://aws.amazon.com/blogs/security/how-to-create-an-aws-iam-policy-to-grant-aws-lambda-access-to-an-amazon-dynamodb-table/
            //By default, the shared AWS credentials file is located in the .aws directory within your home directory and is named 'credentials';
            //to create an access key for this test:  https://aws.amazon.com/premiumsupport/knowledge-center/create-access-key/
            //you can also use teh AWS Explorer tab in VS
            var clientConfig = new AmazonDynamoDBConfig
            {
                RegionEndpoint = RegionEndpoint.USEast1
            };

            var mgr = new DynamoDBTableManager(clientConfig);
            var tables = mgr.GetTableList();
            if (!tables.TableNames.Contains(AlexaUser.DynamoDBTableName)) CreateNewTable(mgr, AlexaUser.DynamoDBTableName);

            Sut = new LoggedInUserRepository(mgr);
        }

        private void CreateNewTable(DynamoDBTableManager mgr, string tableName)
        {
            var thru = new ProvisionedThroughput(1, 1);
            var keys = new List<DynamoDBKeyDescriptor>
            {
                new DynamoDBKeyDescriptor(AlexaUser.DynamoDBKeyFieldName, DynamoDBKeyType.Range, DynamoDBDataType.String)
            };

            mgr.CreateTable(tableName, keys, thru);
        }

        [Test]
        public void GetByKey_ValidKey_ReturnsRow()
        {
            var m = GetTestUserData("Brad","Irby");
            Sut.Insert(m);
            var row = Sut.GetById(m.AlexaUserId);
            Assert.IsNotNull(row);
            Assert.AreEqual(m.FirstName, row.FirstName);
            Assert.AreEqual(m.LastName, row.LastName);
        }

        [Test]
        public void GetByKey_NonExistentKey_ReturnsNull()
        {
            var row = Sut.GetById(Guid.NewGuid().ToString());
            Assert.IsNull(row);
        }

        [Test]
        public void Insert_ValidObject_Inserts()
        {
            var m = GetTestUserData("test","user");
            Sut.Insert(m);
        }

        [Test]
        public void Insert_NullObject_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Sut.Insert(null));
        }

        [Test]
        public void Update_ValidObject_Updates()
        {
            var m = GetTestUserData("Update","user");
            Sut.Insert(m);
            var orig = Sut.GetById(m.AlexaUserId);
            Assert.IsNotNull(orig);

            orig.FirstName= Guid.NewGuid().ToString();
            Sut.Update(orig);
            var after = Sut.GetById(orig.AlexaUserId);
            Assert.AreEqual(orig.FirstName, after.FirstName);
            Assert.AreEqual(orig.LastName, after.LastName);
        }

        [Test]
        public void Update_NullObject_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Sut.Update(null));
        }

        private AlexaUser GetTestUserData(string firstname, string lastname)
        {
            return new AlexaUser
            {
                AlexaUserId = Guid.NewGuid().ToString(),
                FirstName = firstname,
                LastName = lastname
            };
        }



    }
}

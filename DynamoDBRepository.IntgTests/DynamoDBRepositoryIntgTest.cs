using System;
using DynamoDB.Repository.TestWebApp.DataAccess;
using DynamoDB.Repository.TestWebApp.Models;
using NUnit.Framework;

namespace DynamoDB.Repository.IntgTests
{
    public class DynamoDBRepositoryIntgTest
    {
        public MovieRepository Sut { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var cfg = new DynamoDBConfigDefaultUserProvider();
            Sut = new MovieRepository(cfg);
        }

        [Test]
        public void GetByKey_ValidKey_ReturnsRow()
        {
            var m = GetMovie("Brad401");
            var row = Sut.GetById(m.Year, m.Title);
            Assert.IsNotNull(row);
            Assert.AreEqual(m.Title, row.Title);
            Assert.AreEqual(m.Year, row.Year);
        }

        [Test]
        public void GetByKey_NonExistentKey_ReturnsNull()
        {
            var row = Sut.GetById(2013, "Movie Does Not Exist");
            Assert.IsNull(row);
        }

        [Test]
        public void Insert_ValidObject_Inserts()
        {
            var m = GetMovie("Brad401");
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
            var m = GetMovie("Brad201");
            var orig = Sut.GetById(m.Year, m.Title);
            orig.Info.ImageUrl = Guid.NewGuid().ToString();
            Sut.Update(orig);
            var after = Sut.GetById(orig.Year, orig.Title);
            Assert.AreEqual(orig.Title, after.Title);
            Assert.AreEqual(orig.Info.ImageUrl, after.Info.ImageUrl);
        }

        [Test]
        public void Update_NullObject_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Sut.Update(null));
        }

        private Movie GetMovie(string movieName)
        {
            var m = new Movie
            {
                Title = movieName,
                Year = 1943
            };
            m.Info.Actors.Add("actor 1");
            m.Info.Actors.Add("actor 2");
            m.Info.Directors.Add("director 1");
            m.Info.Directors.Add("director 2");
            m.Info.Genres.Add("genre1");
            m.Info.Genres.Add("genre2");
            m.Info.ImageUrl = "image url";
            m.Info.Plot = "plot";
            m.Info.Rank = 2;
            m.Info.Rating = 3.4;
            m.Info.ReleaseDate = DateTime.Now;
            m.Info.RunningTime = 90;
            return m;
        }



    }
}

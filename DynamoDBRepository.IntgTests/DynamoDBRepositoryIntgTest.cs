using System;
using NUnit.Framework;

namespace DynamoDB.Repository.IntgTests
{
    public class DynamoDBRepositoryIntgTest
    {
        public MovieRepository sut { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var fact = new DynamoDBFactory(new DynamoDBConfigProvider());
            sut = new MovieRepository(fact);
        }

        [Test]
        public void GetByKey_ValidKey_ReturnsRow()
        {
            var m = GetMovie("Brad101");
            var row = sut.GetById(m.Year, m.Title);
            Assert.IsNotNull(row);
            Assert.AreEqual(m.Title, row.Title);
            Assert.AreEqual(m.Year, row.Year);
        }

        [Test]
        public void GetByKey_NonExistentKey_ReturnsNull()
        {
            var row = sut.GetById(2013, "Movie Does Not Exist");
            Assert.IsNull(row);
        }

        [Test]
        public void Insert_ValidObject_Inserts()
        {
            var m = GetMovie("Brad201");
            sut.Insert(m);
        }

        [Test]
        public void Insert_NullObject_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => sut.Insert(null));
        }

        [Test]
        public void Update_ValidObject_Updates()
        {
            var m = GetMovie("Brad201");
            var orig = sut.GetById(m.Year, m.Title);
            orig.Info.ImageUrl = Guid.NewGuid().ToString();
            sut.Update(orig);
            var after = sut.GetById(orig.Year, orig.Title);
            Assert.AreEqual(orig.Title, after.Title);
            Assert.AreEqual(orig.Info.ImageUrl, after.Info.ImageUrl);
        }

        [Test]
        public void Update_NullObject_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => sut.Update(null));
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

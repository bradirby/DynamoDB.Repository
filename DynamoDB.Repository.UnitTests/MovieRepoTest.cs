using System;
using System.Threading.Tasks;
using DynamoDBRepository.IntgTests.UnitTests;
using NUnit.Framework;

namespace DynamoDBRepository.UnitTests
{
    
    public class MovieRepoTest
    {
        private MovieRepository Sut { get; set; }

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var mgr = new DynamoDBTableManager(DynamoDbConfigFactory.GetConfigForLocalSimulator());
            var tables = mgr.GetTableList();
            if (!tables.TableNames.Contains(MovieRepository.DynamoDBTableName)) MovieRepository.CreateNewTable(mgr);

            Sut = new MovieRepository(mgr);
        }


        [Test]
        public async Task GetByKey_ValidKey_ReturnsRow()
        {
            var m = GetMovie("GetByKey_ValidKey_ReturnsRow");
            await Sut.InsertAsync(m);
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
        public async Task Insert_ValidObject_Inserts()
        {
            var m = GetMovie("Insert_ValidObject_Inserts" + Guid.NewGuid());
            await Sut.InsertAsync(m);
        }

        [Test]
        public void Insert_NullObject_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Sut.InsertAsync(null).GetAwaiter().GetResult());
        }

        [Test]
        public async Task Update_ValidObject_Updates()
        {
            var m = GetMovie("Update_ValidObject_Updates" + Guid.NewGuid());
            await Sut.InsertAsync(m);
            var orig = Sut.GetById(m.Year, m.Title);
            Assert.IsNotNull(orig);

            orig.Info.ImageUrl = Guid.NewGuid().ToString();
            await Sut.UpdateAsync(orig);
            var after = Sut.GetById(orig.Year, orig.Title);
            Assert.AreEqual(orig.Title, after.Title);
            Assert.AreEqual(orig.Info.ImageUrl, after.Info.ImageUrl);
        }

        [Test]
        public async Task Update_NullObject_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => Sut.UpdateAsync(null).GetAwaiter().GetResult());
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

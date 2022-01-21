using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amazon;
using DynamoDBRepository;
using NUnit.Framework;

namespace SchoolHolidays
{
    public class LoaderTest
    {
        [Test]
        [Explicit]
        public void LoadAllStates()
        {
            var mgr = new DynamoDBTableManager(DynamoDbConfigFactory.GetConfigForEndpoint(RegionEndpoint.USEast1));
            var tables = mgr.GetTableList();
            if (!tables.TableNames.Contains(SchoolCalendarRepository.DynamoDBTableName))

                SchoolCalendarRepository.CreateNewTable(mgr);
            var repo = new SchoolCalendarRepository(mgr);

            var country = new SchoolCountry("D:\\Solutions\\Alexa\\AskMySchool\\school-holidays", repo);
            Assert.AreEqual(50, country.States.Count);
            Assert.AreEqual(0, country.FilesWithErrors.Count);
        }

        [Test]
        public void LoadOneState()
        {
            var alaska = new SchoolState("D:\\Solutions\\Alexa\\AskMySchool\\school-holidays\\alaska", null);
            Assert.AreEqual(36, alaska.Districts.Count);
            foreach (var alaskaDistrict in alaska.Districts)
            {
                foreach (var cal in alaskaDistrict.Calendars)
                {
                    Assert.IsFalse(cal.id.Contains(" "));
                }
            }
        }

        [Test]
        public void LoadOneFile()
        {
            var cal = new SchoolDistrict(
                "D:\\Solutions\\Alexa\\AskMySchool\\school-holidays\\alabama\\alabaster-city-schools\\alabaster-city-schools.htm", null);

            Assert.AreEqual("alabama", cal.StateName);
            Assert.AreEqual("alabaster city schools", cal.SchoolDistrictName);

            Assert.AreEqual(3, cal.Calendars.Count);
            Assert.IsTrue(cal.Calendars.Any(c => c.CalStartYear == 2021));
            Assert.IsTrue(cal.Calendars.Any(c => c.CalStartYear == 2020));
            Assert.IsTrue(cal.Calendars.Any(c => c.CalStartYear == 2019));

            var cal2021 = cal.Calendars.Single(c => c.CalStartYear == 2021);
            Assert.AreEqual(6, cal2021.VacationDays.Count);
            Assert.IsTrue(cal2021.VacationDays.Any(d => d.Name == "First Day of School" && d.StartDate == new DateTime(2021,8,19) && !d.EndDate.HasValue ));
            Assert.IsTrue(cal2021.VacationDays.Any(d => d.Name == "Thanksgiving Break" && d.StartDate == new DateTime(2021,11,24) 
                && d.EndDate.Value == new DateTime(2021,11,26)));
            Assert.IsTrue(cal2021.VacationDays.Any(d => d.Name == "Christmas Break" && d.StartDate == new DateTime(2021,12,20) 
                && d.EndDate.Value == new DateTime(2022,1,3)));
            Assert.IsTrue(cal2021.VacationDays.Any(d => d.Name == "Spring Break" && d.StartDate == new DateTime(2022,3,28) 
                && d.EndDate.Value == new DateTime(2022,4,1)));
            Assert.IsTrue(cal2021.VacationDays.Any(d => d.Name == "Last Day of School" && d.StartDate == new DateTime(2022,5,26)  && !d.EndDate.HasValue ));
            Assert.IsTrue(cal2021.VacationDays.Any(d => d.Name == "Summer Break" && d.StartDate == new DateTime(2022,5,27)  && !d.EndDate.HasValue ));

        }
    }
}

using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SchoolHolidays
{
    public class SchoolDistrict
    {
        public string StateName { get; set; }
        public string SchoolDistrictName { get; set; }

        public List<SchoolCalendar> Calendars { get; set; } = new List<SchoolCalendar>();

        public SchoolDistrict(string fullFilePath, ISchoolCalendarRepository? repo)
        {
            if (!File.Exists(fullFilePath)) throw new ArgumentOutOfRangeException();
            string html = File.ReadAllText(fullFilePath, Encoding.UTF8);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            StateName = fullFilePath.Replace("D:\\Solutions\\Alexa\\AskMySchool\\school-holidays\\", "");
            StateName = StateName.Substring(0, StateName.IndexOf("\\"));

            SchoolDistrictName = fullFilePath.Replace(".htm", "");
            SchoolDistrictName = SchoolDistrictName.Substring(SchoolDistrictName.LastIndexOf("\\")+1).Replace("-"," ");


            var headers = doc.DocumentNode.SelectNodes("//table");
            if (headers == null) return;
            foreach (var header in headers)
            {
                if (header.HasClass("publicholidays")) 
                    Calendars.Add(new SchoolCalendar(header,StateName, SchoolDistrictName, repo));
            }
        }

    }
}

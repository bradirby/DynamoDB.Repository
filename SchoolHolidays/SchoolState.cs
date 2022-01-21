using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace SchoolHolidays
{
    public class SchoolState
    {
        public List<SchoolDistrict> Districts { get; set; } = new List<SchoolDistrict>();
        public List<string> FilesWithErrors{ get; set; } = new List<string>();

        public SchoolState(string folderName, ISchoolCalendarRepository? repo)
        {
            var dir = new DirectoryInfo(folderName);
            foreach (var directoryInfo in dir.GetDirectories())
            {
                foreach (var fileInfo in directoryInfo.GetFiles("*.htm"))
                {
                    try
                    {
                        Debug.WriteLine(fileInfo.FullName);
                        var district = new SchoolDistrict(fileInfo.FullName, repo);
                        if (district.Calendars.Count == 0) FilesWithErrors.Add(fileInfo.FullName);
                        else Districts.Add(district);
                    }
                    catch (Exception ex)
                    {
                        FilesWithErrors.Add(fileInfo.FullName);
                    }
                }
            }
        }
    }
}

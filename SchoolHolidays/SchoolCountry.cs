using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SchoolHolidays
{
    public class SchoolCountry
    {
        public List<SchoolState> States { get; set; } = new List<SchoolState>();
        public List<string> FilesWithErrors{ get; set; } = new List<string>();

        public SchoolCountry(string folderName, ISchoolCalendarRepository? repo)
        {

            var dir = new DirectoryInfo(folderName);
            foreach (var directoryInfo in dir.GetDirectories())
            {
                States.Add(new SchoolState(directoryInfo.FullName, repo));
            }

            foreach (var schoolState in States)
            {
                FilesWithErrors.AddRange(schoolState.FilesWithErrors);
            }
        }
    }
}

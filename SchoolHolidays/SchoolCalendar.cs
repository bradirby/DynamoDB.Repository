using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.Model;

namespace SchoolHolidays
{
    public class SchoolCalendar
    {
        public static string FormatKey(string state, string district, int year)
        {
            return $"{state}{district}{year}".Replace(" ", "");
        }

        [JsonPropertyName("vacationdays")]
        public List<VacationDay> VacationDays { get; set; } = new List<VacationDay>();

        [JsonPropertyName("calstartyear")]
        public int CalStartYear { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("district")]
        public string District { get; set; }
    
        [JsonPropertyName("id")]
        public string id { get; set; }

        public SchoolCalendar()
        {

        }

        public SchoolCalendar(HtmlNode tableNode, string state, string district, ISchoolCalendarRepository? repo )
        {
            State = state;
            District = district;

            var rows = tableNode.Descendants("tr");
            foreach (var calRow in tableNode.Descendants("tr"))
            {
                if (calRow.HasClass("even") || calRow.HasClass("odd"))
                    VacationDays.Add(new VacationDay(calRow));
            }

            CalStartYear = VacationDays.First().StartDate.Year;

            id = FormatKey(State, District, CalStartYear);

            if (repo == null) return;

            var currRow =repo.GetById(id);
            if (currRow == null)
            {
                repo.InsertAsync(this);
                return;
            }

            currRow.VacationDays.Clear();
            currRow.VacationDays.AddRange(VacationDays);
            repo.UpdateAsync(currRow);
        }

    }
}

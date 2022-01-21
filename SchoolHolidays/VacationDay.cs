using System;
using System.Linq;
using System.Text.Json.Serialization;
using System.Xml;
using HtmlAgilityPack;

namespace SchoolHolidays
{
    public class VacationDay
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("startdate")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("enddate")]
        public DateTime? EndDate { get; set; }
        public VacationDay()
        {

        }

        public VacationDay(HtmlNode calRow)
        {
            var fields = calRow.Descendants("td").ToArray();
            Name = fields[0].InnerText;
            
            var startDtTxt = fields[1].InnerText
                .Replace("(Mon)","")
                .Replace("(Tue)","")
                .Replace("(Wed)","")
                .Replace("(Thu)","")
                .Replace("(Fri)","")
                .Replace("(Sat)","")
                .Replace("(Sun)","");

            StartDate = DateTime.Parse(startDtTxt);

            var endDtTxt = fields[2].InnerText
                .Replace("(Mon)","")
                .Replace("(Tue)","")
                .Replace("(Wed)","")
                .Replace("(Thu)","")
                .Replace("(Fri)","")
                .Replace("(Sat)","")
                .Replace("(Sun)","");
            if (!string.IsNullOrEmpty(endDtTxt)) EndDate = DateTime.Parse(endDtTxt);
        }
    }
}

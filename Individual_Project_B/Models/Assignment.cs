using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Models
{
    internal class Assignment
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Submission { get; set; }
        public int? SubmissionWeek
        {
            get
            {
                if (Submission != null)
                {
                    DateTimeFormatInfo dfi = new DateTimeFormatInfo();
                    Calendar calendar = dfi.Calendar;

                    dfi.FirstDayOfWeek = DayOfWeek.Monday;
                    dfi.CalendarWeekRule = CalendarWeekRule.FirstFullWeek;

                    int week = calendar.GetWeekOfYear(Submission.Value, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);

                    return week;
                }
                else
                {
                    return null;
                }

            }
        }
        public double OralMarkPercent { get; set; }
        public double TotalMarkPercent { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
        public string InfoLong
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"\nDisplaying information for Assignment with ID : {ID}\n");
                sb.AppendLine($"Title: {Title}");
                sb.AppendLine($"Description: {Description}");
                sb.AppendLine($"Submission Date: {Submission?.Date} (Week {SubmissionWeek})");
                sb.AppendLine($"Oral Mark: {OralMarkPercent}%");
                sb.AppendLine($"Total Mark: {TotalMarkPercent}%");

                return sb.ToString();
            }
        }

        public string InfoShort
        {
            get
            {
                return $"{ID,-5} {Title,-15} {Submission?.Date,-25} {SubmissionWeek,-20} {OralMarkPercent,-15} {TotalMarkPercent,-15}\n";
            }
        }

        // Construtor
        public Assignment(string title, string description, DateTime? subDateTime, double oralMark, double totalMark)
        {
            Title = title;
            Description = description;
            Submission = subDateTime;
            OralMarkPercent = oralMark;
            TotalMarkPercent = totalMark;
            Students = new List<Student> { };
            Courses = new List<Course> { };
        }

        public Assignment(int id, string title, string description, DateTime? subDateTime, double oralMark, double totalMark)
        {
            ID = id;
            Title = title;
            Description = description;
            Submission = subDateTime;
            OralMarkPercent = oralMark;
            TotalMarkPercent = totalMark;
            Students = new List<Student> { };
            Courses = new List<Course> { };
        }

        public Assignment()
        {
            Students = new List<Student> { };
            Courses = new List<Course> { };
        }

    }
}

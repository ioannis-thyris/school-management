using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Models
{
    internal class Course
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public Stream Stream { get; set; }
        public Type Type { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Student> Students { get; set; }
        public List<Trainer> Trainers { get; set; }
        public List<Assignment> Assignments { get; set; }
        public string Info
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"\nDisplaying information for Course with ID : {ID}\n");
                sb.AppendLine($"Title: {Title}");
                sb.AppendLine($"Stream: {Stream}");
                sb.AppendLine($"Type: {Type}");
                sb.AppendLine($"Start Date: {StartDate?.ToShortDateString()}");
                sb.AppendLine($"End Date: {EndDate?.ToShortDateString()}");

                return sb.ToString();
            }
        }

        public string InfoDetailed
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(Info)
                  .AppendLine("=====================================================\n");

                sb.AppendLine($"Students: ({Students.Count})\n")
                  .AppendLine($"{"ID",-5} {"First Name",-15} {"Last Name",-15} {"Date Of Birth",-20} {"Fees",-15}\n")
                  .AppendLine(PrintStudents())
                  .AppendLine("=====================================================\n");

                sb.AppendLine($"Trainers: ({Trainers.Count})\n")
                  .AppendLine($"{"ID", -5 } {"First Name",-15} {"Last Name",-15} {"Subject",-15}\n")
                  .AppendLine(PrintTrainers())
                  .AppendLine("=====================================================\n");

                sb.AppendLine($"Assignments: ({Assignments.Count})\n")
                  .AppendLine(PrintAssignments())
                  .AppendLine("=====================================================\n");

                return sb.ToString();
            }
        }

        public Course(string title, Stream stream, Type type, DateTime? startDate, DateTime? endDate)
        {
            Title = title;
            Stream = stream;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Students = new List<Student> { };
            Trainers = new List<Trainer> { };
            Assignments = new List<Assignment> { };
        }

        public Course(int id, string title, Stream stream, Type type, DateTime? startDate, DateTime? endDate)
        {
            ID = id;
            Title = title;
            Stream = stream;
            Type = type;
            StartDate = startDate;
            EndDate = endDate;
            Students = new List<Student> { };
            Trainers = new List<Trainer> { };
            Assignments = new List<Assignment> { };
        }

        public Course()
        {
            Students = new List<Student> { };
            Trainers = new List<Trainer> { };
            Assignments = new List<Assignment> { };
        }

        private string PrintStudents()
        {
            StringBuilder sb = new StringBuilder();
            Students.ForEach(stu => sb.AppendLine(stu.InfoShort));

            return sb.ToString();
        }

        private string PrintTrainers()
        {
            StringBuilder sb = new StringBuilder();
            Trainers.ForEach(tr => sb.AppendLine(tr.InfoShort));

            return sb.ToString();
        }

        private string PrintAssignments()
        {
            StringBuilder sb = new StringBuilder();

            foreach (Assignment assignment in Assignments)
            {
                sb.Append("\n")
                  .AppendLine($"{"ID",-5} {"Title",-15} {"Submission DateTime",-25} {"Submission Week",-20} {"OralMark",-15} {"TotalMark",-15}\n")
                  .AppendLine(assignment.InfoShort)
                  .AppendLine($"\t\tStudents With Assignment: ({assignment.Students.Count})\n")
                  .AppendLine($"\t\t{"ID",-5} {"First Name",-15} {"Last Name",-15} {"Date Of Birth",-20} {"Fees",-15}\n");

                foreach (Student student in assignment.Students)
                {
                    sb.Append("\t\t").AppendLine(student.InfoShort);
                }
            }

            return sb.ToString();
        }

    }
}

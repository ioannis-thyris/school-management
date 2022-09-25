using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Models
{
    internal class Trainer
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Subject? Subject { get; set; }
        public List<Course> Courses { get; set; }

        public string InfoLong
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"\nDisplaying information for Trainer with ID : {ID}\n");
                sb.AppendLine($"First Name: {FirstName}");
                sb.AppendLine($"Last Name: {LastName}");
                sb.AppendLine($"Subject: {Subject}");

                return sb.ToString();
            }
        }

        public string InfoShort
        {
            get
            {
                return $"{ID,-5} {FirstName,-15} {LastName,-15} {Subject,-15}";
            }
        }


        public Trainer(string firstName, string lastName, Subject? subject)
        {
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
            Courses = new List<Course>();
        }

        public Trainer(int id, string firstName, string lastName, Subject? subject)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            Subject = subject;
            Courses = new List<Course>();
        }

        public Trainer()
        {
            Courses = new List<Course>();
        }
    }
}

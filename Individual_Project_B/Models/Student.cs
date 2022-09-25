using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Models
{
    internal class Student
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public decimal TuitionFees { get; set; }
        public List<Course> Courses { get; set; }
        public List<Assignment> Assignments { get; set; }
        public string InfoLong
        {
            get
            {
                Console.OutputEncoding = Encoding.Default; // To get the euro symbol.

                StringBuilder sb = new StringBuilder();
                sb.AppendLine($"\nDisplaying information for Student with ID : {ID}\n");
                sb.AppendLine($"First Name: {FirstName}");
                sb.AppendLine($"Last Name: {LastName}");
                sb.AppendLine($"Date of Birth: {DateOfBirth?.ToShortDateString()}");
                sb.AppendLine($"Fees: \u20AC {TuitionFees}\n");

                return sb.ToString();
            }
        }

        public string InfoShort
        {
            get
            {
                Console.OutputEncoding = Encoding.Default; // To get the euro symbol.
                return $"{ID,-5} {FirstName,-15} {LastName,-15} {DateOfBirth?.ToShortDateString(),-20} \u20AC{TuitionFees,-15}";
            }
        }


        // Constructor
        public Student(int id, string firstName, string lastName, DateTime? dateOfBirth, decimal fees)
        {
            ID = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            TuitionFees = fees;
            Courses = new List<Course>();
            Assignments = new List<Assignment>();
        }

        public Student(string firstName, string lastName, DateTime? dateOfBirth, decimal fees)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            TuitionFees = fees;
            Courses = new List<Course>();
            Assignments = new List<Assignment>();
        }

        public Student()
        {
            Courses = new List<Course>();
            Assignments = new List<Assignment>();
        }

    }
}

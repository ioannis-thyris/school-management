using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Factory
{
    internal class EntityFactory
    {
        public Student CreateStudent(int id, string firstName, string lastName, DateTime? dateOfBirth, decimal? fees)
        {
            return new Student(id, firstName, lastName, dateOfBirth.Value, fees.Value);
        }

        public Student CreateStudent(string firstName, string lastName, DateTime? dateOfBirth, decimal? fees)
        {
            return new Student(firstName, lastName, dateOfBirth.Value, fees.Value);
        }

        public Trainer CreateTrainer(string firstName, string lastName, Subject? subject)
        {
            return new Trainer(firstName, lastName, subject.Value);
        }

        public Trainer CreateTrainer(int id, string firstName, string lastName, Subject? subject)
        {
            return new Trainer(id, firstName, lastName, subject.Value);
        }

        public Course CreateCourse(string title, Stream? stream, Type? type, DateTime? startDate, DateTime? endDate)
        {
            return new Course(title, stream.Value, type.Value, startDate.Value, endDate.Value);
        }

        public Course CreateCourse(int id, string title, Stream? stream, Type? type, DateTime? startDate, DateTime? endDate)
        {
            return new Course(title, stream.Value, type.Value, startDate.Value, endDate.Value);
        }

        public Assignment CreateAssignment(int id, string title, string description, DateTime? subDateTime, double? oralMark, double? totalMark)
        {
            return new Assignment(id, title, description, subDateTime.Value, oralMark.Value, totalMark.Value);
        }    

        public Assignment CreateAssignment(string title, string description, DateTime? subDateTime, double? oralMark, double? totalMark)
        {
            return new Assignment(title, description, subDateTime.Value, oralMark.Value, totalMark.Value);
        }

    }
}

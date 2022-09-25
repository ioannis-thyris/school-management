using Individual_Project_B.Factory;
using Individual_Project_B.Repository;
using Individual_Project_B.Services;
using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Individual_Project_B.Repository.StudentRepository;

namespace Individual_Project_B.Controllers.StudentController
{
    internal class StudentController : IStudentController
    {
        private EntityFactory factory;
        private IStudentRepository repository;

        public StudentController(EntityFactory factory, IStudentRepository repository)
        {
            this.factory = factory;
            this.repository = repository;
        }

        public string Add(Dictionary<string, string> userInput)
        {
            StringBuilder sb = new StringBuilder();

            string firstName = Validation.Name(userInput["FirstName"]);
            if (firstName == null)
                sb.AppendLine("Wrong First Name input.");

            string lastName = Validation.Name(userInput["LastName"]);
            if (lastName == null)
                sb.AppendLine("Wrong Last Name input.");

            DateTime? birthDate = Validation.Date(userInput["BirthDate"]);
            if (birthDate == null)
                sb.AppendLine("Wrong Birth Date input.");

            decimal? fees = Validation.Fees(userInput["Fees"]);
            if (fees == null)
                sb.AppendLine("Wrong Fees input.");

            if (firstName != null && lastName != null && birthDate != null && fees != null)
            {
                var student = factory.CreateStudent(firstName, lastName, birthDate, fees);
                return repository.Insert(student);
            }

            return sb.ToString();
        }

        public string ReadAll() => repository.GetAll();

        public string ReadByID(string idInput)
        {
            return IDValid(idInput, out int id) ? repository.GetByID(id) : "Invalid ID input.";
        }


        public string Update(string idInput, Dictionary<string, string> userInput)
        {
            StringBuilder sb = new StringBuilder();

            string firstName = Validation.Name(userInput["FirstName"]);
            if (firstName == null)
                sb.AppendLine("Wrong First Name input.");

            string lastName = Validation.Name(userInput["LastName"]);
            if (lastName == null)
                sb.AppendLine("Wrong Last Name input.");

            DateTime? birthDate = Validation.Date(userInput["BirthDate"]);
            if (birthDate == null)
                sb.AppendLine("Wrong Birth Date input.");

            decimal? fees = Validation.Fees(userInput["Fees"]);
            if (fees == null)
                sb.AppendLine("Wrong Fees input.");

            if (firstName != null && lastName != null && birthDate != null && fees != null)
            {
                var student = factory.CreateStudent(firstName, lastName, birthDate, fees);
                return IDValid(idInput, out int id) ? repository.Update(id, student) : "Invalid ID input.";
            }

            return sb.ToString();
        }

        public string Delete(string idInput)
        {
            if (IDValid(idInput, out int id))
            {
                return ExistsInDB(id, out string existanceMessage) ? repository.Delete(id) : existanceMessage;
            }
            else
                return "Invalid ID input.";
        }

        public bool ExistsInDB(int id, out string existanceMessage)
        {
            bool exists = repository.Exists(id, out string message);
            existanceMessage = message;
            return exists;
        }

        public bool IDValid(string idInput, out int id)
        {
            bool idValid = int.TryParse(idInput, out int ValidID);
            id = ValidID;
            return idValid;
        }

        public string ReadAllInMultipleCourses() => repository.GetAllInMultipleCourses();
    }
}

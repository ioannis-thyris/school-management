using Individual_Project_B.Controllers.StudentController;
using Individual_Project_B.Factory;
using Individual_Project_B.Models;
using Individual_Project_B.Repository;
using Individual_Project_B.Repository.TrainerRepository;
using Individual_Project_B.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Controllers.TrainerController
{
    internal class TrainerController : ITrainerController
    {
        private EntityFactory factory;
        private IGenericRepository<Trainer> repository;

        public TrainerController(EntityFactory factory, IGenericRepository<Trainer> repository)
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

            Subject? subject = Validation.Subject(userInput["Subject"]);
            if (subject == null)
                sb.AppendLine("Wrong Subject input.");

            if (firstName != null && lastName != null && subject != null)
            {
                var trainer = factory.CreateTrainer(firstName, lastName, subject);
                return repository.Insert(trainer);
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

            Subject? subject = Validation.Subject(userInput["Subject"]);
            if (subject == null)
                sb.AppendLine("Wrong Subject input.");

            if (firstName != null && lastName != null && subject != null)
            {
                var trainer = factory.CreateTrainer(firstName, lastName, subject);
                return IDValid(idInput, out int id) ? repository.Update(id, trainer) : "Invalid ID input.";
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
    }
}

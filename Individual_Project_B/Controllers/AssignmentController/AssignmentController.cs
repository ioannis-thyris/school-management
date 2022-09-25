using Individual_Project_B.Factory;
using Individual_Project_B.Models;
using Individual_Project_B.Repository;
using Individual_Project_B.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Controllers.AssignmentController
{
    internal class AssignmentController : IAssignmentController
    {
        private EntityFactory factory;
        private IGenericRepository<Assignment> repository;

        public AssignmentController(EntityFactory factory, IGenericRepository<Assignment> repository)
        {
            this.factory = factory;
            this.repository = repository;
        }


        public string Add(Dictionary<string, string> userInput)
        {
            StringBuilder sb = new StringBuilder();

            string title = userInput["Title"];
            if (string.IsNullOrEmpty(title))
                sb.AppendLine("Wrong Title input.");

            string description = userInput["Description"];

            DateTime? submissionDate = Validation.Date(userInput["SubmissionDate"]);
            if (submissionDate == null)
                sb.AppendLine("Wrong Submission Date input.");

            double? totalMark = Validation.TotalMark(userInput["TotalMark"]);
            if (totalMark == null)
                sb.AppendLine("Wrong Total Mark input.");

            double? oralMark = Validation.OralMark(userInput["OralMark"], totalMark);
            if (oralMark == null)
                sb.AppendLine("Wrong Oral Mark input.");

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description) && submissionDate != null && oralMark != null && totalMark != null)
            {
                var assignment = factory.CreateAssignment(title, description, submissionDate, oralMark, totalMark);
                return repository.Insert(assignment);
            }

            return sb.ToString();
        }

        public string ReadAll() => repository.GetAll();

        public string ReadByID(string idInput)
        {
            return IDValid(idInput, out int id) ? repository.GetByID(id) : "Invalid ID input.";
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

        public string Update(string idInput, Dictionary<string, string> userInput)
        {
            StringBuilder sb = new StringBuilder();

            string title = userInput["Title"];
            if (string.IsNullOrEmpty(title))
                sb.AppendLine("Wrong First Name input.");

            string description = userInput["Description"];
            if (string.IsNullOrEmpty(description))
                sb.AppendLine("Wrong Last Name input.");

            DateTime? submissionDate = Validation.Date(userInput["SubmissionDate"]);
            if (submissionDate == null)
                sb.AppendLine("Wrong Submission Date input.");

            double? totalMark = Validation.TotalMark(userInput["TotalMark"]);
            if (totalMark == null)
                sb.AppendLine("Wrong Total Mark input.");

            double? oralMark = Validation.OralMark(userInput["OralMark"], totalMark);
            if (oralMark == null)
                sb.AppendLine("Wrong Oral Mark input.");

            if (!string.IsNullOrEmpty(title) && !string.IsNullOrEmpty(description) && submissionDate != null && oralMark != null && totalMark != null)
            {
                var assignment = factory.CreateAssignment(title, description, submissionDate, oralMark, totalMark);
                return IDValid(idInput, out int id) ? repository.Update(id, assignment) : "Invalid ID input.";
            }

            return sb.ToString();
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

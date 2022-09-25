using Individual_Project_B.Factory;
using Individual_Project_B.Models;
using Individual_Project_B.Repository;
using Individual_Project_B.Repository.CourseRepository;
using Individual_Project_B.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Controllers.CourseController
{
    internal class CourseController : ICourseController
    {
        private EntityFactory factory;
        private ICourseRepository repository;

        public CourseController(EntityFactory factory, ICourseRepository repository)
        {
            this.factory = factory;
            this.repository = repository;
        }

        public string Add(Dictionary<string, string> userInput)
        {
            StringBuilder sb = new StringBuilder();

            string title = userInput["Title"];

            Stream? stream = Validation.Stream(userInput["Stream"]);
            if (stream == null)
                sb.AppendLine("Wrong Stream input.");

            Type? type = Validation.Type(userInput["Type"]);
            if (type == null)
                sb.AppendLine("Wrong Type input.");

            DateTime? startDate = Validation.Date(userInput["StartDate"]);
            if (startDate == null)
                sb.AppendLine("Wrong Start Date input.");

            DateTime? endDate = Validation.Date(userInput["EndDate"]);
            if (endDate == null)
                sb.AppendLine("Wrong End Date input.");

            bool? endDateAfterStartDate = Validation.EndAfterStart(startDate, endDate);
            if (endDateAfterStartDate == false)
                sb.AppendLine("The End Date must be after the Start Date.");

            if (!string.IsNullOrEmpty(title) && stream != null && type != null)
            {
                var student = factory.CreateCourse(title, stream, type, startDate, endDate);
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

            string title = userInput["Title"];

            Stream? stream = Validation.Stream(userInput["Stream"]);
            if (stream == null)
                sb.AppendLine("Wrong Stream input.");

            Type? type = Validation.Type(userInput["Type"]);
            if (type == null)
                sb.AppendLine("Wrong Type input.");

            DateTime? startDate = Validation.Date(userInput["StartDate"]);
            if (startDate == null)
                sb.AppendLine("Wrong Start Date input.");

            DateTime? endDate = Validation.Date(userInput["EndDate"]);
            if (endDate == null)
                sb.AppendLine("Wrong End Date input.");

            bool? endDateAfterStartDate = Validation.EndAfterStart(startDate, endDate);
            if (endDateAfterStartDate == false)
                sb.AppendLine("The End Date must be after the Start Date.");

            if (!string.IsNullOrEmpty(title) && stream != null && type != null)
            {
                var student = factory.CreateCourse(title, stream, type, startDate, endDate);
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

        public string ReadByIDDetailed(int id) => repository.GetByIDDetailed(id);

        public string StudentsNotInCourse(int id) => repository.GetStudentsNotInCourse(id);

        public string AddStudentToCourse(int courseID, string studentIDInput)
        {
            if (IDValid(studentIDInput, out int studentID))
            {
                return repository.AddStudentToCourse(courseID, studentID);
            }
            else
                return "Invalid ID input.";
        }
        public string RemoveStudentFromCourse(int courseID, string studentIDInput)
        {
            if (IDValid(studentIDInput, out int studentID))
            {
                return repository.RemoveStudentFromCourse(courseID, studentID);
            }
            else
                return "Invalid ID input.";
        }

        public string TrainersNotInCourse(int id) => repository.GetTrainersNotInCourse(id);
        public string AddTrainerToCourse(int courseID, string trainerIDinput)
        {

            if (IDValid(trainerIDinput, out int trainerID))
            {
                return repository.AddTrainerToCourse(courseID, trainerID);
            }
            else
                return "Invalid ID input.";

        }

        public string AssignmentsNotInCourse(int id) => repository.GetAssignmentsNotInCourse(id);
        public string AddAssignmentToCourse(int courseID, string assignmentID)
        {

            if (IDValid(assignmentID, out int trainerID))
            {
                return repository.AddAssignmentToCourse(courseID, trainerID);
            }
            else
                return "Invalid ID input.";
        }

        public string RemoveTrainerFromCourse(int courseID, string trainerIDInput)
        {
            if (IDValid(trainerIDInput, out int trainerID))
            {
                return repository.RemoveTrainerFromCourse(courseID, trainerID);
            }
            else
                return "Invalid ID input.";
        }

        public string RemoveAssignmentFromCourse(int courseID, string assignmentIDInput)
        {
            if (IDValid(assignmentIDInput, out int assignmentID))
            {
                return repository.RemoveAssignmentFromCourse(courseID, assignmentID);
            }
            else
                return "Invalid ID input.";
        }

        public string AddStudentToAssignment(int courseID, string assignmentIDInput, string studentIDInput)
        {
            if (IDValid(assignmentIDInput, out int assignmentID))
            {
                if (IDValid(studentIDInput, out int studentID))
                {
                    return repository.AddStudentToAssignment(courseID, assignmentID, studentID);
                }
                else
                    return "Invalid Student ID input.";
            }
            else
                return "Invalid Assignment ID input.";
        }

        public string RemoveStudentFromAssignment(int courseID, string assignmentIDInput, string studentIDInput)
        {
            if (IDValid(assignmentIDInput, out int assignmentID))
            {
                if (IDValid(studentIDInput, out int studentID))
                {
                    return repository.RemoveStudentFromAssignment(courseID, assignmentID, studentID);
                }
                else
                    return "Invalid Student ID input.";
            }
            else
                return "Invalid Assignment ID input.";
        }
        
    }
}

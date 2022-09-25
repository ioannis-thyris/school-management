using Individual_Project_B.Controllers.CourseController;
using Individual_Project_B.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views.CourseView
{
    internal class CourseView : ICourseView
    {
        private SharedMessages sharedMessages = new SharedMessages();
        private CourseMessages messages = new CourseMessages();

        public ICourseController CourseController { get; }

        public CourseView(ICourseController courseController)
        {
            CourseController = courseController;
        }

        public void Menu()
        {
            Console.Clear();

            Console.WriteLine(sharedMessages.Welcome);
            Console.WriteLine(messages.Menu);

            ConsoleKey input = Console.ReadKey(true).Key;

            switch (input)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    AddEntity();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    DisplayEntityByID();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    DisplayAllEntities();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    EditEntity();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    DeleteEntity();
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    ManageCourse();
                    break;
                case ConsoleKey.Backspace:
                    Console.Clear();
                    throw new Exception();
                default:
                    Console.WriteLine(messages.WrongInput);
                    Menu();
                    break;
            }
        }

        public Dictionary<string, string> EntityInfoInput()
        {
            Dictionary<string, string> userInput = new Dictionary<string, string>();

            Console.WriteLine(messages.CourseInputHeader);

            Console.WriteLine(messages.Title);
            string titleInput = Console.ReadLine();
            userInput.Add("Title", titleInput);

            Console.WriteLine(messages.Stream);
            string streamInput = Console.ReadLine();
            userInput.Add("Stream", streamInput);

            Console.WriteLine(messages.Type);
            string typeInput = Console.ReadLine();
            userInput.Add("Type", typeInput);

            Console.WriteLine(messages.StartDate);
            string startDateInput = Console.ReadLine();
            userInput.Add("StartDate", startDateInput);

            Console.WriteLine(messages.EndDate);
            string endDateInput = Console.ReadLine();
            userInput.Add("EndDate", endDateInput);

            return userInput;
        }

        public void ContinueInput()
        {
            Console.WriteLine(messages.AddPrompt);
            ConsoleKey input = Console.ReadKey(true).Key;

            if (input == ConsoleKey.Y)
            {
                Console.Clear();
                Console.WriteLine(sharedMessages.Welcome);
                Console.WriteLine(messages.Menu);
                AddEntity();
            }
            else if (input == ConsoleKey.N)
            {
                Menu();
            }
            else
            {
                ContinueInput();
            }
        }

        public void ReturnPrompt()
        {
            Console.WriteLine(messages.ReturnPromptMenu);

            ConsoleKey input = Console.ReadKey(true).Key;

            if (input == ConsoleKey.Enter)
            {
                Menu();
            }
            else if (input == ConsoleKey.Backspace)
            {
                Console.Clear();
                throw new Exception();
            }
            else
            {
                ReturnPrompt();
            }
        }

        public void AddEntity()
        {
            Dictionary<string, string> userInput = EntityInfoInput();

            string response = CourseController.Add(userInput);
            Console.WriteLine(response);

            ContinueInput();
        }

        public void DisplayEntityByID()
        {
            Console.WriteLine(messages.IdToShow);
            string idInput = Console.ReadLine();

            string response = CourseController.ReadByID(idInput);
            Console.WriteLine(response);

            ReturnPrompt();
        }

        public void DisplayAllEntities()
        {
            Console.Clear();

            string response = CourseController.ReadAll();
            Console.WriteLine(response);

            ReturnPrompt();
        }

        public void EditEntity()
        {
            Console.WriteLine(messages.IdToUpdate);
            string idInput = Console.ReadLine();

            if (CourseController.IDValid(idInput, out int id))
            {
                if (CourseController.ExistsInDB(id, out string existanceMessage))
                {
                    Dictionary<string, string> userInput = EntityInfoInput();

                    string response = CourseController.Update(idInput, userInput);
                    Console.WriteLine(response);
                }
                else
                    Console.WriteLine(existanceMessage);

                ReturnPrompt();
            }
            else
            {
                Console.WriteLine(messages.InvalidID);
                ReturnPrompt();
            }
        }

        public void DeleteEntity()
        {
            Console.WriteLine(messages.IdToDelete);
            string idInput = Console.ReadLine();

            string response = CourseController.Delete(idInput);
            Console.WriteLine(response);

            ReturnPrompt();
        }


        private void ManageCourse()
        {
            Console.WriteLine(messages.IdToManage);
            string idInput = Console.ReadLine();

            if (CourseController.IDValid(idInput, out int id))
            {
                if (CourseController.ExistsInDB(id, out string existanceMessage))
                {
                    ManageMenu(id);
                }
                else
                    Console.WriteLine(existanceMessage);

                ReturnPrompt();
            }
            else
            {
                Console.WriteLine(messages.InvalidID);
                ReturnPrompt();
            }
        }

        public void ManageMenu(int courseID)
        {
            Console.Clear();

            string response = CourseController.ReadByIDDetailed(courseID);
            Console.WriteLine(response);

            Console.WriteLine(messages.ManageMenu);
            Console.WriteLine(messages.ReturnPromptMenu);

            ConsoleKey input = Console.ReadKey(true).Key;

            switch (input)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    AddStudentToCourse(courseID);
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    RemoveStudentFromCourse(courseID);
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    AddTrainerToCourse(courseID);
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    RemoveTrainerFromCourse(courseID);
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    AddAssignmentToCourse(courseID);
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    RemoveAssignmentFromCourse(courseID);
                    break;
                case ConsoleKey.D7:
                case ConsoleKey.NumPad7:
                    AddStudentToAssignment(courseID);
                    break;
                case ConsoleKey.D8:
                case ConsoleKey.NumPad8:
                    RemoveStudentFromAssignment(courseID);
                    break;
                case ConsoleKey.Enter:
                    Menu();
                    break;
                case ConsoleKey.Backspace:
                    Console.Clear();
                    throw new Exception();
                default:
                    ManageMenu(courseID);
                    break;
            }
        }


        public void AddStudentToCourse(int courseID)
        {
            string responseStudents = CourseController.StudentsNotInCourse(courseID);
            Console.WriteLine(responseStudents);

            Console.WriteLine(messages.StudentIdToAdd);
            string studentIDInput = Console.ReadLine();

            string responseAdd = CourseController.AddStudentToCourse(courseID, studentIDInput);
            Console.WriteLine(responseAdd);

            Console.ReadLine();

            ManageMenu(courseID);
        }

        public void RemoveStudentFromCourse(int courseID)
        {
            Console.WriteLine(messages.StudentIdToRemove);
            string idInput = Console.ReadLine();

            string response = CourseController.RemoveStudentFromCourse(courseID, idInput);
            Console.WriteLine(response);

            Console.ReadLine();

            ManageMenu(courseID);
        }

        public void AddTrainerToCourse(int courseID)
        {
            string responseTrainers = CourseController.TrainersNotInCourse(courseID);
            Console.WriteLine(responseTrainers);

            Console.WriteLine(messages.TrainerIdToAdd);
            string trainerIDInput = Console.ReadLine();

            string responseAdd = CourseController.AddTrainerToCourse(courseID, trainerIDInput);
            Console.WriteLine(responseAdd);

            Console.ReadLine();

            ManageMenu(courseID);
        }

        public void RemoveTrainerFromCourse(int courseID)
        {
            Console.WriteLine(messages.TrainertIdToRemove);
            string idInput = Console.ReadLine();

            string response = CourseController.RemoveTrainerFromCourse(courseID, idInput);
            Console.WriteLine(response);

            Console.ReadLine();

            ManageMenu(courseID);
        }

        public void AddAssignmentToCourse(int courseID)
        {
            string responseAssignments = CourseController.AssignmentsNotInCourse(courseID);
            Console.WriteLine(responseAssignments);

            Console.WriteLine(messages.AssignmentIdToAdd);
            string assignmentIDInput = Console.ReadLine();

            string responseAdd = CourseController.AddAssignmentToCourse(courseID, assignmentIDInput);
            Console.WriteLine(responseAdd);

            Console.ReadLine();

            ManageMenu(courseID);
        }

        public void RemoveAssignmentFromCourse(int courseID)
        {
            Console.WriteLine(messages.AssignmentIdToRemove);
            string idInput = Console.ReadLine();

            string response = CourseController.RemoveAssignmentFromCourse(courseID, idInput);
            Console.WriteLine(response);

            Console.ReadLine();

            ManageMenu(courseID);
        }

        private void AddStudentToAssignment(int courseID)
        {
            Console.WriteLine(messages.AssignmentIdToAddStudent);
            string assignmentIDInput = Console.ReadLine();

            Console.WriteLine(messages.StudentIdToAddToAssignment);
            string studentIDInput = Console.ReadLine();

            string responseAdd = CourseController.AddStudentToAssignment(courseID, assignmentIDInput, studentIDInput);
            Console.WriteLine(responseAdd);

            Console.ReadLine();

            ManageMenu(courseID);
        }

        private void RemoveStudentFromAssignment(int courseID)
        {
            Console.WriteLine(messages.AssignmentIdToRemoveStudent);
            string assignmentIDInput = Console.ReadLine();

            Console.WriteLine(messages.StudentIdToRemoveFromAssignment);
            string studentIDInput = Console.ReadLine();

            string responseAdd = CourseController.RemoveStudentFromAssignment(courseID, assignmentIDInput, studentIDInput);
            Console.WriteLine(responseAdd);

            Console.ReadLine();

            ManageMenu(courseID);
        }



    }
}


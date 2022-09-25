using Individual_Project_B.Controllers.AssignmentController;
using Individual_Project_B.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views.AssignmentView
{
    internal class AssignmentView : IAssignmentView
    {
        private SharedMessages sharedMessages = new SharedMessages();
        private AssignmentMessages messages = new AssignmentMessages();

        public IAssignmentController AssignmentController { get; }

        public AssignmentView(IAssignmentController assignmentController)
        {
            AssignmentController = assignmentController;
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

            Console.WriteLine(messages.AssignmentInputHeader);

            Console.WriteLine(messages.Title);
            string titleInput = Console.ReadLine();
            userInput.Add("Title", titleInput);

            Console.WriteLine(messages.Description);
            string descriptionInput = Console.ReadLine();
            userInput.Add("Description", descriptionInput);

            Console.WriteLine(messages.SubmissionDate);
            string submissionDateInput = Console.ReadLine();
            userInput.Add("SubmissionDate", submissionDateInput);

            Console.WriteLine(messages.OralMark);
            string oralMarkInput = Console.ReadLine();
            userInput.Add("OralMark", oralMarkInput);

            Console.WriteLine(messages.TotalMark);
            string totalMarkInput = Console.ReadLine();
            userInput.Add("TotalMark", totalMarkInput);

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

            string response = AssignmentController.Add(userInput);
            Console.WriteLine(response);

            ContinueInput();
        }

        public void DisplayEntityByID()
        {
            Console.WriteLine(messages.IdToShow);
            string idInput = Console.ReadLine();

            string response = AssignmentController.ReadByID(idInput);
            Console.WriteLine(response);

            ReturnPrompt();
        }

        public void DisplayAllEntities()
        {
            Console.Clear();

            string response = AssignmentController.ReadAll();
            Console.WriteLine(response);

            ReturnPrompt();
        }

        public void EditEntity()
        {
            Console.WriteLine(messages.IdToUpdate);
            string idInput = Console.ReadLine();

            if (AssignmentController.IDValid(idInput, out int id))
            {
                if (AssignmentController.ExistsInDB(id, out string existanceMessage))
                {
                    Dictionary<string, string> userInput = EntityInfoInput();

                    string response = AssignmentController.Update(idInput, userInput);
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

            string response = AssignmentController.Delete(idInput);
            Console.WriteLine(response);

            ReturnPrompt();
        }
    }
}

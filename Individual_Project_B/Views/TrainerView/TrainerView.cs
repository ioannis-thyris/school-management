using Individual_Project_B.Controllers.TrainerController;
using Individual_Project_B.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views.TrainerView
{
    internal class TrainerView : ITrainerView
    {
        private SharedMessages sharedMessages = new SharedMessages();
        private TrainerMessages messages = new TrainerMessages();

        public ITrainerController TrainerController { get; set; }

        public TrainerView(ITrainerController trainerController)
        {
            TrainerController = trainerController;
        }

        public void Menu()
        {
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
        }

        public Dictionary<string, string> EntityInfoInput()
        {
            Dictionary<string, string> userInput = new Dictionary<string, string>();

            Console.WriteLine(messages.TrainerInputHeader);

            Console.WriteLine(messages.FirstName);
            string firstNameInput = Console.ReadLine();
            userInput.Add("FirstName", firstNameInput);

            Console.WriteLine(messages.LastName);
            string lastNameInput = Console.ReadLine();
            userInput.Add("LastName", lastNameInput);

            Console.WriteLine(messages.Subject);
            string subjectInput = Console.ReadLine();
            userInput.Add("Subject", subjectInput);

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

            string response = TrainerController.Add(userInput);
            Console.WriteLine(response);

            ContinueInput();
        }

        public void DisplayEntityByID()
        {
            Console.WriteLine(messages.IdToShow);
            string idInput = Console.ReadLine();

            string response = TrainerController.ReadByID(idInput);
            Console.WriteLine(response);

            ReturnPrompt();
        }

        public void DisplayAllEntities()
        {
            Console.Clear();

            string response = TrainerController.ReadAll();
            Console.WriteLine(response);

            ReturnPrompt();
        }

        public void EditEntity()
        {
            Console.WriteLine(messages.IdToUpdate);
            string idInput = Console.ReadLine();

            if (TrainerController.IDValid(idInput, out int id))
            {
                if (TrainerController.ExistsInDB(id, out string existanceMessage))
                {
                    Dictionary<string, string> userInput = EntityInfoInput();

                    string response = TrainerController.Update(idInput, userInput);
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

            string response = TrainerController.Delete(idInput);
            Console.WriteLine(response);

            ReturnPrompt();
        }
    }
}

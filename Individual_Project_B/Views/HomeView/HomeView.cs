using Individual_Project_B.Views.AssignmentView;
using Individual_Project_B.Views.CourseView;
using Individual_Project_B.Views.Shared;
using Individual_Project_B.Views.StudentView;
using Individual_Project_B.Views.TrainerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views.HomeView
{
    internal class HomeView : IView
    {
        private SharedMessages sharedMessages = new SharedMessages();
        private HomeMessages homeMessages = new HomeMessages();

        public IAssignmentView Assignment { get; set; }
        public ICourseView Course { get; set; }
        public IStudentView StudentView { get; set; }
        public ITrainerView Trainer { get; set; }

        public HomeView(IAssignmentView assignment, ICourseView course, IStudentView student, ITrainerView trainer)
        {
            Assignment = assignment;
            Course = course;
            StudentView = student;
            Trainer = trainer;
        }

        public void Menu()
        {
            Console.WriteLine(sharedMessages.Welcome);
            Console.WriteLine(homeMessages.HomeMenu);

            ConsoleKey input = Console.ReadKey(true).Key;

            switch (input)
            {
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    try
                    {
                        StudentView.Menu();
                    }
                    catch (Exception)
                    {
                        Menu();
                    }
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    try
                    {
                        Trainer.Menu();
                    }
                    catch (Exception)
                    {
                        Menu();
                    }
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    try
                    {
                        Course.Menu();
                    }
                    catch (Exception)
                    {
                        Menu();
                    }
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    try
                    {
                        Assignment.Menu();
                    }
                    catch (Exception)
                    {
                        Menu();
                    }
                    break;
                default:
                    Console.WriteLine("Wrong input\n");
                    Console.Clear();
                    Menu();
                    break;
            }



        }
    }
}

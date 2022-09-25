using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views.StudentView
{
    internal class StudentMessages
    {
        public string Menu
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" __________ Student Menu __________\n")
                  .AppendLine(" Add ........................... 1\n")
                  .AppendLine(" Show .......................... 2\n")
                  .AppendLine(" Show all ...................... 3\n")
                  .AppendLine(" Edit .......................... 4\n")
                  .AppendLine(" Delete ........................ 5\n\n")
                  .AppendLine(" Students in Multiple Courses .. 6\n\n")
                  .AppendLine(" Main Menu ...... BackSpace");

                return sb.ToString();
            }
        }

        public string WrongInput { get => "Wrong input\n"; }

        public string RedirectToMenu { get => "Redirecting to Student Menu.\n"; }
        public string StudentInputHeader { get => "===== Enter Student's Info =====\n"; }
        public string FirstName { get => "First Name:"; }
        public string LastName { get => "Last Name:"; }
        public string BirthDate { get => "Birth Date:"; }
        public string Fees { get => "Fees:"; }
        public string AddPrompt { get => "Add new Student? Y/N"; }
        public string InvalidID { get => "Invalid ID input."; }
        public string ReturnPromptMenu 
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("\n Student Menu .................... Enter")
                  .AppendLine(" Main Menu ............... BackSpace");

                return sb.ToString();
            }
        }
        public string IdToShow { get => " Enter the ID of the Student to display."; }
        public string IdToUpdate { get => " Enter the ID of the Student to update."; }
        public string IdToDelete { get => " Enter the ID of the Student for deletion."; }








    }
}

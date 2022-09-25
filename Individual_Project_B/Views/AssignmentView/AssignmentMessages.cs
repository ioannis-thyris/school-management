using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views.AssignmentView
{
    internal class AssignmentMessages
    {
        public string Menu
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" ________ Assignment Menu ________\n")
                  .AppendLine(" Add .......................... 1\n")
                  .AppendLine(" Show ......................... 2\n")
                  .AppendLine(" Show all ..................... 3\n")
                  .AppendLine(" Edit ......................... 4\n")
                  .AppendLine(" Delete ....................... 5\n")
                  .AppendLine(" Students To Submit in Week.... 6\n")
                  .AppendLine(" Main Menu ...... BackSpace");

                return sb.ToString();
            }
        }

        public string WrongInput { get => "Wrong input\n"; }

        public string RedirectToMenu { get => "Redirecting to Assignment Menu.\n"; }
        public string AssignmentInputHeader { get => "===== Enter Assignment's Info =====\n"; }
        public string Title { get => "Title:"; }
        public string Description { get => "Description:"; }
        public string SubmissionDate { get => "Submission Date (dd/mm/yyyy):"; }
        public string OralMark { get => "Oral Mark (%):"; }
        public string TotalMark { get => "Total Mark (%):"; }
        public string AddPrompt { get => "Add new Assignment? Y/N"; }
        public string InvalidID { get => "Invalid ID input."; }
        public string ReturnPromptMenu
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("\n Assignment Menu ............ Enter")
                  .AppendLine(" Main Menu ............. BackSpace");

                return sb.ToString();
            }
        }
        public string IdToShow { get => " Enter the ID of the Assignment to display."; }
        public string IdToUpdate { get => " Enter the ID of the Assignment to update."; }
        public string IdToDelete { get => " Enter the ID of the Assignment for deletion."; }
    }
}

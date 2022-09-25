using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views.CourseView
{
    internal class CourseMessages
    {
        public string Menu
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(" ______ Course Menu ______\n")
                  .AppendLine(" Add .................. 1\n")
                  .AppendLine(" Show ................. 2\n")
                  .AppendLine(" Show all ............. 3\n")
                  .AppendLine(" Edit ................. 4\n")
                  .AppendLine(" Delete ............... 5\n\n")
                  .AppendLine(" Manage Course ........ 6\n\n")
                  .AppendLine(" Main Menu ..... BackSpace");

                return sb.ToString();
            }
        }

        public string WrongInput { get => "Wrong input\n"; }

        public string RedirectToMenu { get => "Redirecting to Course Menu.\n"; }
        public string CourseInputHeader { get => "===== Enter Course's Info =====\n"; }
        public string Title { get => "Title:"; }
        public string Stream { get => "Stream: (Java / JavaScript / Python / CSharp)"; }
        public string Type { get => "Type: (FullTime / PartTime)"; }
        public string StartDate { get => "Start Date (dd/mm/yyyy):"; }
        public string EndDate { get => "End Date (dd/mm/yyyy):"; }
        public string AddPrompt { get => "Add new Course? Y/N"; }
        public string InvalidID { get => "Invalid ID input."; }
        public string ReturnPromptMenu
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("\n Course Menu ............... Enter")
                  .AppendLine(" Main Menu ............... BackSpace");

                return sb.ToString();
            }
        }
        public string IdToShow { get => " Enter the ID of the Course to display."; }
        public string IdToUpdate { get => " Enter the ID of the Course to update."; }
        public string IdToDelete { get => " Enter the ID of the Course for deletion."; }
        public string IdToManage { get => "Enter the ID of Course you would like to manage."; }


        public string ManageMenu
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine(" Add Student ....................... 1\n")
                  .AppendLine(" Delete Student .................... 2\n")
                  .AppendLine(" Add Trainer ....................... 3\n")
                  .AppendLine(" Delete Trainer .................... 4\n")
                  .AppendLine(" Add Assignment .................... 5\n")
                  .AppendLine(" Delete Assignment ................. 6\n")
                  .AppendLine(" Add Student to Assignment ......... 7\n")
                  .AppendLine(" Delete Student from Assignment .... 8\n");

                return sb.ToString();
            }
        }



        public string StudentIdToAdd { get => "Enter the ID of Student you would like to add.\n"; }
        public string StudentIdToRemove { get => "Enter the ID of Student you would like to remove.\n"; }
        public string TrainerIdToAdd { get => "Enter the ID of Trainer you would like to add.\n"; }
        public string TrainertIdToRemove { get => "Enter the ID of Trainer you would like to remove.\n"; }
        public string AssignmentIdToAdd { get => "Enter the ID of Assignment you would like to add.\n"; }
        public string AssignmentIdToRemove { get => "Enter the ID of Assignment you would like to remove.\n"; }
        public string AssignmentIdToAddStudent { get => "Enter the ID of Assignment you would like to add to.\n"; }
        public string StudentIdToAddToAssignment { get => "Enter the ID of Student you would like to add to the Assignment.\n"; }
        public string AssignmentIdToRemoveStudent { get => "Enter the ID of Assignment you would like to remove from.\n"; }
        public string StudentIdToRemoveFromAssignment { get => "Enter the ID of Student you would like to remove from the Assignment.\n"; }


    }
}

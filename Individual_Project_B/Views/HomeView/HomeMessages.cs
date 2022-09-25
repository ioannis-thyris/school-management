using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views.HomeView
{
    internal class HomeMessages
    {
        public string HomeMenu
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.AppendLine("_____________ Main Menu _____________\n\n")
                  .AppendLine(" Students ........................ 1\n")
                  .AppendLine(" Trainers ........................ 2\n")
                  .AppendLine(" Courses ......................... 3\n")
                  .AppendLine(" Assignments ..................... 4\n\n");

                return sb.ToString();
            }

        }
    }
}

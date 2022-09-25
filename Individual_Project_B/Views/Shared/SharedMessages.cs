using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views.Shared
{
    internal class SharedMessages
    {
        public string Welcome
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("===========================================================\n")
                  .Append("\t\tWelcome to our private school\t\t\n")
                  .Append("===========================================================\n");

                return sb.ToString();
            }


        }

    }
}

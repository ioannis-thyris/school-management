using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Controllers
{
    public interface IController
    {
        string Add(Dictionary<string, string> userInput);
        string Delete(string idInput);
        bool ExistsInDB(int id, out string existanceMessage);
        bool IDValid(string idInput, out int id);
        string ReadAll();
        string ReadByID(string idInput);
        string Update(string idInput, Dictionary<string, string> userInput);

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Views
{
    public interface IMenuItem
    {
        void Menu();
        void AddEntity();
        void DisplayEntityByID();
        void DisplayAllEntities();
        void EditEntity();
        void DeleteEntity();
    }
}

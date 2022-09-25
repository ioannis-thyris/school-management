using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Repository
{
    public interface IGenericRepository<T>
    {
        string Insert(T entity);
        string GetByID(int id);
        string GetAll();
        string Update(int id, T entity);
        string Delete(int id);
        bool Exists(int id, out string existanceMessage);
    }
}

using Individual_Project_B.Models;
using System.Collections.Generic;

namespace Individual_Project_B.DataAccess
{
    interface IDataAccess<T> where T : class
    {
        string connectionString { get; }

        bool Insert(T entity);
        T GetByID(int entityID);
        List<T> GetAll();
        bool Update(int entityID, T entity);
        bool Delete(int entityID);
        bool Exists(int entityID);
    }
}
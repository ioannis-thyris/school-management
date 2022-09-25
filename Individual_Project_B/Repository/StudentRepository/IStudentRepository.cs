using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Repository.StudentRepository
{
    interface IStudentRepository : IGenericRepository<Student>
    {
        string GetAllInMultipleCourses();
    }
}

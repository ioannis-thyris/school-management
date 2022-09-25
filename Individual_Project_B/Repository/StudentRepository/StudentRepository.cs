using Individual_Project_B.DataAccess;
using Individual_Project_B.DataAccess.StudentDataAccess;
using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Repository.StudentRepository
{
    internal class StudentRepository : IStudentRepository
    {
        public IStudentDataAccess StudentDB { get; }

        public StudentRepository(IStudentDataAccess studentDB)
        {
            StudentDB = studentDB;
        }

        public string Insert(Student entity)
        {
            try
            {
                bool addSuccessful = StudentDB.Insert(entity);
                string successMessage = "\nSuccessfully inserted new Student to the Database.";
                string failMessage = "\nThe Student was not inserted successfully.";

                return addSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetByID(int id)
        {
            try
            {
                Student student = StudentDB.GetByID(id);
                string idNotFoundMessage = $" A Student with ID -{id}- does not exist in the Database\n";

                return student is null ? idNotFoundMessage : student.InfoLong;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetAll()
        {
            List<Student> students = StudentDB.GetAll();
            StringBuilder sb = new StringBuilder();

            students.ForEach(s => sb.AppendLine(s.InfoLong));

            return sb.ToString();
        }

        public string Update(int id, Student entity)
        {
            try
            {
                bool updateSuccessful = StudentDB.Update(id, entity);
                string successMessage = "\nSuccessfully updated Student's info in the Database.";
                string failMessage = $"\nThe Student's info were not updated successfully.";

                return updateSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Delete(int id)
        {
            try
            {
                bool deleteSuccessful = StudentDB.Delete(id);
                string successMessage = $"\nSuccessfully deleted Student with ID -{id}-";
                string failMessage = $"\nThe Student was not deleted successfully.";

                return deleteSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool Exists(int id, out string existanceMessage)
        {
            bool entityExists = StudentDB.Exists(id);
            existanceMessage = entityExists ? "\nEntity exists" : $" A Student with ID -{id}- does not exist in the Database\n";

            return entityExists;
        }

        public string GetAllInMultipleCourses()
        {
            List<Student> students = StudentDB.GetAllInMultipleCourses();
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(" Students In Multiple Courses:\n");
            students.ForEach(s => sb.AppendLine(s.InfoLong));

            return sb.ToString();
        }
    }
}

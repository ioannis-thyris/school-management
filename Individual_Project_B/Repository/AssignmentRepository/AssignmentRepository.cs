using Individual_Project_B.DataAccess;
using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Repository.AssignmentRepository
{
    internal class AssignmentRepository : IGenericRepository<Assignment>
    {
        public IDataAccess<Assignment> AssignmentDB { get; }

        public AssignmentRepository(IDataAccess<Assignment> assignmentDB)
        {
            AssignmentDB = assignmentDB;
        }

        public string Insert(Assignment entity)
        {
            try
            {
                bool addSuccessful = AssignmentDB.Insert(entity);
                string successMessage = "\nSuccessfully inserted new Assignment to the Database.";
                string failMessage = "\nThe Assignment was not inserted successfully.";

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
                Assignment assignment = AssignmentDB.GetByID(id);
                string idNotFoundMessage = $" An Assignment with ID -{id}- does not exist in the Database\n";

                return assignment is null ? idNotFoundMessage : assignment.InfoLong;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetAll()
        {
            List<Assignment> assignments = AssignmentDB.GetAll();
            StringBuilder sb = new StringBuilder();

            assignments.ForEach(a => sb.AppendLine(a.InfoLong));

            return sb.ToString();
        }

        public string Update(int id, Assignment entity)
        {
            try
            {
                bool updateSuccessful = AssignmentDB.Update(id, entity);
                string successMessage = "\nSuccessfully updated Assignment's info in the Database.";
                string failMessage = $"\nThe Assignment's info were not updated successfully.";

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
                bool deleteSuccessful = AssignmentDB.Delete(id);
                string successMessage = $"\nSuccessfully deleted Assignment with ID -{id}-";
                string failMessage = $"\nThe Assignment was not deleted successfully.";

                return deleteSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool Exists(int id, out string existanceMessage)
        {
            bool entityExists = AssignmentDB.Exists(id);
            existanceMessage = entityExists ? "\nEntity exists" : $" An Assignment with ID -{id}- does not exist in the Database\n";

            return entityExists;
        }
    }
}

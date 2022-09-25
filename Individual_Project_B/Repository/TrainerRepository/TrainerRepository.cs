using Individual_Project_B.DataAccess;
using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Repository.TrainerRepository
{
    internal class TrainerRepository : IGenericRepository<Trainer>
    {
        public IDataAccess<Trainer> TrainerDB { get; }

        public TrainerRepository(IDataAccess<Trainer> trainerDB)
        {
            TrainerDB = trainerDB;
        }

        public string Insert(Trainer entity)
        {
            try
            {
                bool addSuccessful = TrainerDB.Insert(entity);
                string successMessage = "\nSuccessfully inserted new Trainer to the Database.";
                string failMessage = "\nThe Trainer was not inserted successfully.";

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
                Trainer trainer = TrainerDB.GetByID(id);
                string idNotFoundMessage = $" A Trainer with ID -{id}- does not exist in the Database\n";

                return trainer is null ? idNotFoundMessage : trainer.InfoLong;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetAll()
        {
            List<Trainer> trainers = TrainerDB.GetAll();
            StringBuilder sb = new StringBuilder();

            trainers.ForEach(s => sb.AppendLine(s.InfoLong));

            return sb.ToString();
        }

        public string Update(int id, Trainer entity)
        {
            try
            {
                bool updateSuccessful = TrainerDB.Update(id, entity);
                string successMessage = "\nSuccessfully updated Trainer's info in the Database.";
                string failMessage = $"\nThe Trainer's info were not updated successfully.";

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
                bool deleteSuccessful = TrainerDB.Delete(id);
                string successMessage = $"\nSuccessfully deleted Trainer with ID -{id}-";
                string failMessage = $"\nThe Trainer was not deleted successfully.";

                return deleteSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool Exists(int id, out string existanceMessage)
        {
            bool entityExists = TrainerDB.Exists(id);
            existanceMessage = entityExists ? "\nEntity exists" : $" A Trainer with ID -{id}- does not exist in the Database\n";

            return entityExists;
        }
    }
}

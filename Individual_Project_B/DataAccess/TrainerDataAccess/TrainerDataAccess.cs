using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.DataAccess.TrainerDataAccess
{
    internal class TrainerDataAccess : IDataAccess<Trainer>
    {
        public string connectionString { get; } = ConfigurationManager.ConnectionStrings["PartB_Database"].ConnectionString;

        public bool Insert(Trainer trainer)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryInsertTrainer = $"INSERT INTO Trainer(FirstName, LastName, Subject)\n" +
                                                $"VALUES (@FirstName, @LastName, @Subject)";

                    SqlCommand cmdInsertTrainer = new SqlCommand(queryInsertTrainer, sqlConnection);
                    cmdInsertTrainer.Parameters.AddWithValue("@FirstName", trainer.FirstName);
                    cmdInsertTrainer.Parameters.AddWithValue("@LastName", trainer.LastName);
                    cmdInsertTrainer.Parameters.AddWithValue("@Subject", (int)trainer.Subject);

                    int trainersAdded = cmdInsertTrainer.ExecuteNonQuery();

                    sqlConnection.Close();

                    return trainersAdded == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Trainer GetByID(int trainerID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                Trainer trainer;

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdGetTrainerByID = new SqlCommand("SELECT * FROM Trainer WHERE ID = @ID", sqlConnection);
                    cmdGetTrainerByID.Parameters.AddWithValue("@ID", trainerID);

                    SqlDataReader reader = cmdGetTrainerByID.ExecuteReader();

                    if (!reader.HasRows)
                        trainer = null;
                    else
                    {
                        reader.Read();

                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("LastName"));

                        Subject? subject;

                        if (reader.IsDBNull(reader.GetOrdinal("Subject")))
                            subject = null;
                        else
                        {
                            subject = (Subject)reader.GetInt32(reader.GetOrdinal("Subject"));
                        }

                        trainer = new Trainer(id, firstName, lastName, subject);
                    }

                    reader.Close();
                    sqlConnection.Close();

                    return trainer;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Trainer> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                List<Trainer> allTrainers = new List<Trainer>();

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdGetAllTrainers = new SqlCommand("SELECT * FROM Trainer", sqlConnection);

                    SqlDataReader reader = cmdGetAllTrainers.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("LastName"));

                        Subject? subject;

                        if (reader.IsDBNull(reader.GetOrdinal("Subject")))
                            subject = null;
                        else
                        {
                            subject = (Subject)reader.GetInt32(reader.GetOrdinal("Subject"));
                        }

                        Trainer trainer = new Trainer(id, firstName, lastName, subject);

                        allTrainers.Add(trainer);
                    }
                    reader.Close();

                    sqlConnection.Close();

                    return allTrainers;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool Update(int trainerID, Trainer trainer)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryUpdateTrainer = $"UPDATE Trainer\n" +
                                                $"SET FirstName = @FirstName, LastName = @LastName, Subject = @Subject\n" +
                                                $"WHERE ID = @ID";

                    SqlCommand cmdUpdateTrainer = new SqlCommand(queryUpdateTrainer, sqlConnection);
                    cmdUpdateTrainer.Parameters.AddWithValue("@FirstName", trainer.FirstName);
                    cmdUpdateTrainer.Parameters.AddWithValue("@LastName", trainer.LastName);
                    cmdUpdateTrainer.Parameters.AddWithValue("@Subject", (int)trainer.Subject);
                    cmdUpdateTrainer.Parameters.AddWithValue("@ID", trainerID);

                    int trainersUpdated = cmdUpdateTrainer.ExecuteNonQuery();

                    sqlConnection.Close();

                    return trainersUpdated == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Delete(int trainerID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdDeleteTrainer = new SqlCommand($"DELETE FROM Trainer WHERE ID = @ID", sqlConnection);
                    cmdDeleteTrainer.Parameters.AddWithValue("@ID", trainerID);

                    int trainersDeleted = cmdDeleteTrainer.ExecuteNonQuery();

                    sqlConnection.Close();

                    return trainersDeleted == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Exists(int trainerID)
        {
            {
                using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                {
                    try
                    {
                        sqlConnection.Open();

                        SqlCommand cmdGetTrainerByID = new SqlCommand("SELECT * FROM Trainer WHERE ID = @ID", sqlConnection);
                        cmdGetTrainerByID.Parameters.AddWithValue("@ID", trainerID);

                        bool trainerExists = cmdGetTrainerByID.ExecuteScalar() != null;

                        sqlConnection.Close();

                        return trainerExists;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}

using Individual_Project_B.DataAccess;
using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.DataAccess.AssignmentDataAccess
{
    internal class AssignmentDataAccess : IDataAccess<Assignment>
    {
        public string connectionString { get; } = ConfigurationManager.ConnectionStrings["PartB_Database"].ConnectionString;

        public bool Insert(Assignment a)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryInsertAssignment = "INSERT INTO Assignment(Title, Description, Submission, OralMarkPercent, TotalMarkPercent)\n" +
                                                   "VALUES (@Title, @Description, @Submission, @OralMark, @TotalMark)";

                    SqlCommand cmdInsertAssignment = new SqlCommand(queryInsertAssignment, sqlConnection);
                    cmdInsertAssignment.Parameters.AddWithValue("@Title", a.Title);
                    cmdInsertAssignment.Parameters.AddWithValue("@Description", a.Description);
                    cmdInsertAssignment.Parameters.AddWithValue("@Submission", a.Submission?.ToString("yyyy/MM/dd"));
                    cmdInsertAssignment.Parameters.AddWithValue("@OralMark", a.OralMarkPercent);
                    cmdInsertAssignment.Parameters.AddWithValue("@TotalMark", a.TotalMarkPercent);


                    int assignmentsAdded = cmdInsertAssignment.ExecuteNonQuery();

                    sqlConnection.Close();

                    return assignmentsAdded == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Assignment GetByID(int assignmentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                Assignment assignment;

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdGetAssignmentByID = new SqlCommand("SELECT * FROM Assignment WHERE ID = @ID", sqlConnection);
                    cmdGetAssignmentByID.Parameters.AddWithValue("@ID", assignmentID);

                    SqlDataReader reader = cmdGetAssignmentByID.ExecuteReader();

                    if (!reader.HasRows)
                        assignment = null;
                    else
                    {
                        reader.Read();

                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string title = reader.GetString(reader.GetOrdinal("Title"));
                        string description = reader.GetString(reader.GetOrdinal("Description"));

                        DateTime? submissionDateTime;

                        if (reader.IsDBNull(reader.GetOrdinal("Submission")))
                            submissionDateTime = null;
                        else
                            submissionDateTime = reader.GetDateTime(reader.GetOrdinal("Submission"));

                        double oralMark = (double)reader.GetDecimal(reader.GetOrdinal("OralMarkPercent"));
                        double totalMark = (double)reader.GetDecimal(reader.GetOrdinal("TotalMarkPercent"));

                        assignment = new Assignment(id, title, description, submissionDateTime, oralMark, totalMark);
                    }

                    reader.Close();
                    sqlConnection.Close();

                    return assignment;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Assignment> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                List<Assignment> allAssignments = new List<Assignment>();

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdGetAllAssignments = new SqlCommand("SELECT * FROM Assignment", sqlConnection);

                    SqlDataReader reader = cmdGetAllAssignments.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string title = reader.GetString(reader.GetOrdinal("Title"));
                        string description = reader.GetString(reader.GetOrdinal("Description"));

                        DateTime? submissionDateTime;

                        if (reader.IsDBNull(reader.GetOrdinal("Submission")))
                            submissionDateTime = null;
                        else
                            submissionDateTime = reader.GetDateTime(reader.GetOrdinal("Submission"));

                        double oralMark = (double)reader.GetDecimal(reader.GetOrdinal("OralMarkPercent"));
                        double totalMark = (double)reader.GetDecimal(reader.GetOrdinal("TotalMarkPercent"));

                        Assignment assignment = new Assignment(id, title, description, submissionDateTime, oralMark, totalMark);

                        allAssignments.Add(assignment);
                    }
                    reader.Close();

                    sqlConnection.Close();

                    return allAssignments;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Update(int assignmentID, Assignment a)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryUpdateAssignment = "UPDATE Assignment \n" +
                                                   "SET Title = @Title, Description = @Description, Submission = @Submission, OralMarkPercent = @OralMark, TotalMarkPercent = @TotalMark \n" +
                                                   "WHERE ID = @AssignmentID";

                    SqlCommand cmdUpdateAssignment = new SqlCommand(queryUpdateAssignment, sqlConnection);
                    cmdUpdateAssignment.Parameters.AddWithValue("@Title", a.Title);
                    cmdUpdateAssignment.Parameters.AddWithValue("@Description", a.Description);
                    cmdUpdateAssignment.Parameters.AddWithValue("@Submission", a.Submission?.ToString("yyyy/MM/dd"));
                    cmdUpdateAssignment.Parameters.AddWithValue("@OralMark", a.OralMarkPercent);
                    cmdUpdateAssignment.Parameters.AddWithValue("@TotalMark", a.TotalMarkPercent);
                    cmdUpdateAssignment.Parameters.AddWithValue("@AssignmentID", assignmentID);

                    int assignmentsUpdated = cmdUpdateAssignment.ExecuteNonQuery();

                    sqlConnection.Close();

                    return assignmentsUpdated == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Delete(int assignmentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryDeleteFromCompositeTables = "DELETE FROM Course_Student_Assignment WHERE AssignmentID = @AssignmentID\n" +
                                                            "DELETE FROM AssignmentInCourse WHERE AssignmentID = @AssignmentID";
                    string queryDeleteFromStudentTable = "DELETE FROM Assignment WHERE ID = @AssignmentID";

                    SqlCommand cmdDeleteAssignment = new SqlCommand($"{queryDeleteFromCompositeTables}\n{queryDeleteFromStudentTable}", sqlConnection);
                    cmdDeleteAssignment.Parameters.AddWithValue("@AssignmentID", assignmentID);

                    int assignmentDeleted = cmdDeleteAssignment.ExecuteNonQuery();

                    sqlConnection.Close();

                    return assignmentDeleted > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }

        public bool Exists(int assignmentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdGetAssignmentByID = new SqlCommand("SELECT * FROM Assignment WHERE ID = @ID", sqlConnection);
                    cmdGetAssignmentByID.Parameters.AddWithValue("@ID", assignmentID);

                    bool assignmentExists = cmdGetAssignmentByID.ExecuteScalar() != null;

                    sqlConnection.Close();

                    return assignmentExists;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
    }
}

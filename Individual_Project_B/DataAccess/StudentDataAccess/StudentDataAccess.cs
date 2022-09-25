using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Individual_Project_B.DataAccess.StudentDataAccess
{

    internal class StudentDataAccess : IStudentDataAccess
    {
        public string connectionString { get; } = ConfigurationManager.ConnectionStrings["PartB_Database"].ConnectionString;

        public bool Insert(Student student)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryInsertStudent = "INSERT INTO Student(FirstName, LastName, DateOfBirth, TuitionFees)\n" +
                                                "VALUES (@FirstName, @LastName, @DateOfBirth, @TuitionFees)";

                    SqlCommand cmdInsertStudent = new SqlCommand(queryInsertStudent, sqlConnection);
                    cmdInsertStudent.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmdInsertStudent.Parameters.AddWithValue("@LastName", student.LastName);
                    cmdInsertStudent.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth?.ToString("yyyy/MM/dd"));
                    cmdInsertStudent.Parameters.AddWithValue("@TuitionFees", student.TuitionFees);


                    int studentsAdded = cmdInsertStudent.ExecuteNonQuery();

                    sqlConnection.Close();

                    return studentsAdded == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Student GetByID(int studentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                Student student;

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdGetStudentByID = new SqlCommand("SELECT * FROM Student WHERE ID = @ID", sqlConnection);
                    cmdGetStudentByID.Parameters.AddWithValue("@ID", studentID);

                    SqlDataReader reader = cmdGetStudentByID.ExecuteReader();

                    if (!reader.HasRows)
                        student = null;
                    else
                    {
                        reader.Read();

                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("LastName"));

                        DateTime? dateOfBirth;

                        if (reader.IsDBNull(reader.GetOrdinal("DateOfBirth")))
                            dateOfBirth = null;
                        else
                            dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));

                        decimal fees = reader.GetDecimal(reader.GetOrdinal("TuitionFees"));

                        student = new Student(id, firstName, lastName, dateOfBirth, fees);
                    }

                    reader.Close();
                    sqlConnection.Close();

                    return student;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Student> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                List<Student> allStudents = new List<Student>();

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdGetAllStudents = new SqlCommand("SELECT * FROM Student", sqlConnection);

                    SqlDataReader reader = cmdGetAllStudents.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("LastName"));

                        DateTime? dateOfBirth;

                        if (reader.IsDBNull(reader.GetOrdinal("DateOfBirth")))
                            dateOfBirth = null;
                        else
                            dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));

                        decimal fees = reader.GetDecimal(reader.GetOrdinal("TuitionFees"));

                        Student student = new Student(id, firstName, lastName, dateOfBirth, fees);

                        allStudents.Add(student);
                    }
                    reader.Close();

                    sqlConnection.Close();

                    return allStudents;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool Update(int studentID, Student student)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryUpdateStudent = "UPDATE Student\n" +
                                                "SET FirstName = @FirstName, LastName = @LastName, DateOfBirth = @DateOfBirth, TuitionFees = @TuitionFees\n" +
                                                "WHERE ID = @ID";

                    SqlCommand cmdUpdateStudent = new SqlCommand(queryUpdateStudent, sqlConnection);
                    cmdUpdateStudent.Parameters.AddWithValue("@FirstName", student.FirstName);
                    cmdUpdateStudent.Parameters.AddWithValue("@LastName", student.LastName);
                    cmdUpdateStudent.Parameters.AddWithValue("@DateOfBirth", student.DateOfBirth?.ToString("yyyy/MM/dd"));
                    cmdUpdateStudent.Parameters.AddWithValue("@TuitionFees", student.TuitionFees);
                    cmdUpdateStudent.Parameters.AddWithValue("@ID", studentID);

                    int studentsUpdated = cmdUpdateStudent.ExecuteNonQuery();

                    sqlConnection.Close();

                    return studentsUpdated == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Delete(int studentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryDeleteFromCompositeTables = "DELETE FROM Course_Student_Assignment WHERE StudentID = @ID\n" +
                                                            "DELETE FROM StudentInCourse WHERE StudentID = @ID";

                    string queryDeleteFromStudentTable = "DELETE FROM Student WHERE ID = @ID";

                    SqlCommand cmdDeleteStudent = new SqlCommand($"{queryDeleteFromCompositeTables}\n{queryDeleteFromStudentTable}", sqlConnection);
                    cmdDeleteStudent.Parameters.AddWithValue("@ID", studentID);

                    int studentsDeleted = cmdDeleteStudent.ExecuteNonQuery();

                    sqlConnection.Close();

                    return studentsDeleted > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Exists(int studentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdGetStudentByID = new SqlCommand("SELECT * FROM Student WHERE ID = @ID", sqlConnection);
                    cmdGetStudentByID.Parameters.AddWithValue("@ID", studentID);

                    bool studentExists = cmdGetStudentByID.ExecuteScalar() != null;

                    sqlConnection.Close();

                    return studentExists;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Student> GetAllInMultipleCourses()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                List<Student> students = new List<Student>();

                try
                {
                    sqlConnection.Open();

                    string getAllStudentWithMultipleCourses =
                        "SELECT StudentID, FirstName, LastName, DateOfBirth, TuitionFees \n" +
                        "FROM StudentInCourse INNER JOIN Student ON StudentID = Student.ID \n" +
                        "GROUP BY StudentID, FirstName, LastName, DateOfBirth, TuitionFees \n" +
                        "HAVING COUNT(*) > 1";

                    SqlCommand cmdGetAllStudentsWithMultipleCourses = new SqlCommand(getAllStudentWithMultipleCourses, sqlConnection);

                    SqlDataReader reader = cmdGetAllStudentsWithMultipleCourses.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("StudentID"));
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("LastName"));

                        DateTime? dateOfBirth;

                        if (reader.IsDBNull(reader.GetOrdinal("DateOfBirth")))
                            dateOfBirth = null;
                        else
                            dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));

                        decimal fees = reader.GetDecimal(reader.GetOrdinal("TuitionFees"));

                        Student student = new Student(id, firstName, lastName, dateOfBirth, fees);

                        students.Add(student);
                    }
                    reader.Close();

                    sqlConnection.Close();

                    return students;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }
    }
}

using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.DataAccess.CourseDataAccess
{
    internal class CourseDataAccess : ICourseDataAccess
    {
        public string connectionString { get; } = ConfigurationManager.ConnectionStrings["PartB_Database"].ConnectionString;

        public bool Insert(Course course)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryInsertCourse = "INSERT INTO Course(Title, Stream, Type, StartDate, EndDate) \n" +
                                               "VALUES (@Title, @Stream, @Type, @StartDate, @EndDate)";

                    SqlCommand cmdInsertCourse = new SqlCommand(queryInsertCourse, sqlConnection);
                    cmdInsertCourse.Parameters.AddWithValue("@Title", course.Title);
                    cmdInsertCourse.Parameters.AddWithValue("@Stream", (int)course.Stream);
                    cmdInsertCourse.Parameters.AddWithValue("@Type", (int)course.Type);
                    cmdInsertCourse.Parameters.AddWithValue("@StartDate", course.StartDate?.ToString("yyyy/MM/dd"));
                    cmdInsertCourse.Parameters.AddWithValue("@EndDate", course.EndDate?.ToString("yyyy/MM/dd"));

                    int coursesAdded = cmdInsertCourse.ExecuteNonQuery();

                    sqlConnection.Close();

                    return coursesAdded == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Course GetByID(int courseID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                Course course;

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdGetCourseByID = new SqlCommand("SELECT * FROM Course WHERE ID = @ID", sqlConnection);
                    cmdGetCourseByID.Parameters.AddWithValue("@ID", courseID);

                    SqlDataReader reader = cmdGetCourseByID.ExecuteReader();

                    if (!reader.HasRows)
                        course = null;
                    else
                    {
                        reader.Read();

                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string title = reader.GetString(reader.GetOrdinal("Title"));
                        Stream stream = (Stream)reader.GetInt32(reader.GetOrdinal("Stream"));
                        Type type = (Type)reader.GetInt32(reader.GetOrdinal("Type"));

                        DateTime? startDate;

                        if (reader.IsDBNull(reader.GetOrdinal("StartDate")))
                            startDate = null;
                        else
                            startDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));

                        DateTime? endDate;

                        if (reader.IsDBNull(reader.GetOrdinal("EndDate")))
                            endDate = null;
                        else
                            endDate = reader.GetDateTime(reader.GetOrdinal("EndDate"));

                        course = new Course(id, title, stream, type, startDate, endDate);
                    }

                    reader.Close();
                    sqlConnection.Close();

                    return course;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Course> GetAll()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                List<Course> allCourses = new List<Course>();

                try
                {
                    sqlConnection.Open();
                    SqlCommand cmdGetAllCourses = new SqlCommand("SELECT * FROM Course", sqlConnection);

                    SqlDataReader reader = cmdGetAllCourses.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = reader.GetInt32(reader.GetOrdinal("ID"));
                        string title = reader.GetString(reader.GetOrdinal("Title"));
                        Stream stream = (Stream)reader.GetInt32(reader.GetOrdinal("Stream"));
                        Type type = (Type)reader.GetInt32(reader.GetOrdinal("Type"));

                        DateTime? startDate;

                        if (reader.IsDBNull(reader.GetOrdinal("StartDate")))
                            startDate = null;
                        else
                            startDate = reader.GetDateTime(reader.GetOrdinal("StartDate"));

                        DateTime? endDate;

                        if (reader.IsDBNull(reader.GetOrdinal("EndDate")))
                            endDate = null;
                        else
                            endDate = reader.GetDateTime(reader.GetOrdinal("EndDate"));

                        Course course = new Course(id, title, stream, type, startDate, endDate);

                        allCourses.Add(course);
                    }
                    reader.Close();

                    sqlConnection.Close();

                    return allCourses;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool Update(int courseID, Course course)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryUpdateCourse = "UPDATE Course\n" +
                                               "SET Title = @Title, Stream = @Stream, Type = @Type, StartDate = @StartDate, EndDate = @EndDate \n" +
                                               "WHERE ID = @ID";

                    SqlCommand cmdUpdateCourse = new SqlCommand(queryUpdateCourse, sqlConnection);
                    cmdUpdateCourse.Parameters.AddWithValue("@Title", course.Title);
                    cmdUpdateCourse.Parameters.AddWithValue("@Stream", (int)course.Stream);
                    cmdUpdateCourse.Parameters.AddWithValue("@Type", (int)course.Type);
                    cmdUpdateCourse.Parameters.AddWithValue("@StartDate", course.StartDate?.ToString("yyyy/MM/dd"));
                    cmdUpdateCourse.Parameters.AddWithValue("@EndDate", course.EndDate?.ToString("yyyy/MM/dd"));
                    cmdUpdateCourse.Parameters.AddWithValue("@ID", courseID);

                    int coursesUpdated = cmdUpdateCourse.ExecuteNonQuery();

                    sqlConnection.Close();

                    return coursesUpdated == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Delete(int courseID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryDeleteFromCompositeTables = "DELETE FROM Course_Student_Assignment WHERE CourseID = @ID\n" +
                                                            "DELETE FROM AssignmentInCourse WHERE CourseID = @ID\n" +
                                                            "DELETE FROM StudentInCourse WHERE CourseID = @ID\n";
                    string queryDeleteFromCourseTable = "DELETE FROM Course WHERE ID = @ID";

                    SqlCommand cmdDeleteCourse = new SqlCommand($"{queryDeleteFromCompositeTables}\n{queryDeleteFromCourseTable}", sqlConnection);
                    cmdDeleteCourse.Parameters.AddWithValue("@ID", courseID);

                    int coursesDeleted = cmdDeleteCourse.ExecuteNonQuery();

                    sqlConnection.Close();

                    return coursesDeleted > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Exists(int courseID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdGetCourseByID = new SqlCommand("SELECT * FROM Course WHERE ID = @ID", sqlConnection);
                    cmdGetCourseByID.Parameters.AddWithValue("@ID", courseID);

                    bool courseExists = cmdGetCourseByID.ExecuteScalar() != null;

                    sqlConnection.Close();

                    return courseExists;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public Course GetByIDDetailed(int courseID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                Course course;
                Trainer trainer;
                Student student;
                Assignment assignment;

                try
                {
                    sqlConnection.Open();

                    SqlCommand cmdGetCourseByID = new SqlCommand("SELECT * FROM Course WHERE ID = @ID", sqlConnection);
                    cmdGetCourseByID.Parameters.AddWithValue("@ID", courseID);

                    string queryGetTrainersInCourse =
                        "SELECT * " +
                        "FROM TrainerInCourse INNER JOIN Trainer ON TrainerID = Trainer.ID " +
                        "WHERE CourseID = @ID";
                    SqlCommand cmdGetTrainersInCourse = new SqlCommand(queryGetTrainersInCourse, sqlConnection);
                    cmdGetTrainersInCourse.Parameters.AddWithValue("@ID", courseID);
                    SqlDataReader trainerReader;

                    string queryGetStudentsInCourse =
                        "SELECT * " +
                        "FROM StudentInCourse INNER JOIN Student ON StudentID = Student.ID " +
                        "WHERE CourseID = @ID";
                    SqlCommand cmdGetStudentsInCourse = new SqlCommand(queryGetStudentsInCourse, sqlConnection);
                    cmdGetStudentsInCourse.Parameters.AddWithValue("@ID", courseID);
                    SqlDataReader studentReader;

                    string queryGetAssignmentsInCourse =
                        "SELECT * " +
                        "FROM AssignmentInCourse INNER JOIN Assignment ON AssignmentID = Assignment.ID " +
                        "WHERE CourseID = @ID";
                    SqlCommand cmdGetAssignmentsInCourse = new SqlCommand(queryGetAssignmentsInCourse, sqlConnection);
                    cmdGetAssignmentsInCourse.Parameters.AddWithValue("@ID", courseID);
                    SqlDataReader assignmentReader;

                    string queryGetStudentsWithAssignment =
                        "SELECT * " +
                        "FROM Course_Student_Assignment INNER JOIN Student ON StudentID = Student.ID " +
                        "WHERE CourseID = @CourseID AND AssignmentID = @AssignmentID";
                    SqlCommand cmdGetStudentsWithAssignment = new SqlCommand(queryGetStudentsWithAssignment, sqlConnection);
                    cmdGetStudentsWithAssignment.Parameters.AddWithValue("@CourseID", courseID);
                    cmdGetStudentsWithAssignment.Parameters.Add("@AssignmentID", System.Data.SqlDbType.Int);
                    SqlDataReader studentWithAssignmentsReader;


                    using (SqlDataReader courseReader = cmdGetCourseByID.ExecuteReader())
                    {
                        if (!courseReader.HasRows)
                            course = null;
                        else
                        {
                            courseReader.Read();

                            int idCourse = courseReader.GetInt32(courseReader.GetOrdinal("ID"));
                            string titleCourse = courseReader.GetString(courseReader.GetOrdinal("Title"));
                            Stream stream = (Stream)courseReader.GetInt32(courseReader.GetOrdinal("Stream"));
                            Type type = (Type)courseReader.GetInt32(courseReader.GetOrdinal("Type"));

                            DateTime? startDate;

                            if (courseReader.IsDBNull(courseReader.GetOrdinal("StartDate")))
                                startDate = null;
                            else
                                startDate = courseReader.GetDateTime(courseReader.GetOrdinal("StartDate"));

                            DateTime? endDate;

                            if (courseReader.IsDBNull(courseReader.GetOrdinal("EndDate")))
                                endDate = null;
                            else
                                endDate = courseReader.GetDateTime(courseReader.GetOrdinal("EndDate"));

                            course = new Course(idCourse, titleCourse, stream, type, startDate, endDate);

                            // Read Trainers of the Course
                            using (trainerReader = cmdGetTrainersInCourse.ExecuteReader())
                            {
                                while (trainerReader.Read())
                                {
                                    int idTrainer = trainerReader.GetInt32(trainerReader.GetOrdinal("TrainerID"));
                                    string firstName = trainerReader.GetString(trainerReader.GetOrdinal("FirstName"));
                                    string lastName = trainerReader.GetString(trainerReader.GetOrdinal("LastName"));

                                    Subject? subject;

                                    if (trainerReader.IsDBNull(trainerReader.GetOrdinal("Subject")))
                                        subject = null;
                                    else
                                    {
                                        subject = (Subject)trainerReader.GetInt32(trainerReader.GetOrdinal("Subject"));
                                    }

                                    trainer = new Trainer(idTrainer, firstName, lastName, subject);

                                    course.Trainers.Add(trainer);
                                    trainer.Courses.Add(course);

                                }
                            }

                            // Read Students of the Course
                            using (studentReader = cmdGetStudentsInCourse.ExecuteReader())
                            {
                                while (studentReader.Read())
                                {
                                    int idStudent = studentReader.GetInt32(studentReader.GetOrdinal("StudentID"));
                                    string firstName = studentReader.GetString(studentReader.GetOrdinal("FirstName"));
                                    string lastName = studentReader.GetString(studentReader.GetOrdinal("LastName"));

                                    DateTime? dateOfBirth;

                                    if (studentReader.IsDBNull(studentReader.GetOrdinal("DateOfBirth")))
                                        dateOfBirth = null;
                                    else
                                        dateOfBirth = studentReader.GetDateTime(studentReader.GetOrdinal("DateOfBirth"));

                                    decimal fees = studentReader.GetDecimal(studentReader.GetOrdinal("TuitionFees"));

                                    student = new Student(idStudent, firstName, lastName, dateOfBirth, fees);

                                    course.Students.Add(student);
                                    student.Courses.Add(course);

                                }
                            }

                            // Read Assignments of the Course
                            using (assignmentReader = cmdGetAssignmentsInCourse.ExecuteReader())
                            {
                                while (assignmentReader.Read())
                                {
                                    int idAssignment = assignmentReader.GetInt32(assignmentReader.GetOrdinal("AssignmentID"));
                                    string title = assignmentReader.GetString(assignmentReader.GetOrdinal("Title"));
                                    string description = assignmentReader.GetString(assignmentReader.GetOrdinal("Description"));

                                    DateTime? submissionDateTime;

                                    if (assignmentReader.IsDBNull(assignmentReader.GetOrdinal("Submission")))
                                        submissionDateTime = null;
                                    else
                                        submissionDateTime = assignmentReader.GetDateTime(assignmentReader.GetOrdinal("Submission"));

                                    double oralMark = (double)assignmentReader.GetDecimal(assignmentReader.GetOrdinal("OralMarkPercent"));
                                    double totalMark = (double)assignmentReader.GetDecimal(assignmentReader.GetOrdinal("TotalMarkPercent"));

                                    assignment = new Assignment(idAssignment, title, description, submissionDateTime, oralMark, totalMark);

                                    course.Assignments.Add(assignment);
                                    assignment.Courses.Add(course);

                                    cmdGetStudentsWithAssignment.Parameters["@AssignmentID"].Value = idAssignment;

                                    using (studentWithAssignmentsReader = cmdGetStudentsWithAssignment.ExecuteReader())
                                    {
                                        while (studentWithAssignmentsReader.Read())
                                        {
                                            int idStudent = studentWithAssignmentsReader.GetInt32(studentWithAssignmentsReader.GetOrdinal("StudentID"));
                                            student = course.Students.Find(stu => (stu.ID == idStudent));

                                            assignment.Students.Add(student);
                                            student.Assignments.Add(assignment);
                                        }


                                    }



                                }
                            }
                        }
                    }



                    sqlConnection.Close();

                    return course;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Student> GetStudentsNotInCourse(int courseID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                Student student;
                List<Student> studentsNotInCourse = new List<Student>();

                try
                {
                    sqlConnection.Open();

                    string queryStudentsNotInCourse =
                        "Select * From Student Where Student.ID NOT IN " +
                        "(SELECT StudentID FROM StudentInCourse WHERE CourseID = @ID)";

                    SqlCommand cmdGetStudentsNotInCourse = new SqlCommand(queryStudentsNotInCourse, sqlConnection);
                    cmdGetStudentsNotInCourse.Parameters.AddWithValue("@ID", courseID);

                    SqlDataReader reader = cmdGetStudentsNotInCourse.ExecuteReader();

                    while (reader.Read())
                    {
                        int idStudent = reader.GetInt32(reader.GetOrdinal("ID"));
                        string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                        string lastName = reader.GetString(reader.GetOrdinal("LastName"));

                        DateTime? dateOfBirth;

                        if (reader.IsDBNull(reader.GetOrdinal("DateOfBirth")))
                            dateOfBirth = null;
                        else
                            dateOfBirth = reader.GetDateTime(reader.GetOrdinal("DateOfBirth"));

                        decimal fees = reader.GetDecimal(reader.GetOrdinal("TuitionFees"));

                        student = new Student(idStudent, firstName, lastName, dateOfBirth, fees);

                        studentsNotInCourse.Add(student);

                    }
                    reader.Close();

                    sqlConnection.Close();

                    return studentsNotInCourse;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool AddStudentToCourse(int courseID, int studentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryAddStudentToCourse = "INSERT INTO StudentInCourse(CourseID, StudentID)\n" +
                                                     "VALUES (@CourseID, @StudentID)";

                    SqlCommand cmdAddStudentToCourse = new SqlCommand(queryAddStudentToCourse, sqlConnection);
                    cmdAddStudentToCourse.Parameters.AddWithValue("@CourseID", courseID);
                    cmdAddStudentToCourse.Parameters.AddWithValue("@StudentID", studentID);

                    int studentsAdded = cmdAddStudentToCourse.ExecuteNonQuery();

                    sqlConnection.Close();

                    return studentsAdded == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool RemoveStudentFromCourse(int courseID, int studentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryDeleteStudentFromCourse = 
                        "DELETE FROM Course_Student_Assignment WHERE CourseID = @CourseID AND StudentID = @StudentID\n" +
                        "DELETE FROM StudentInCourse WHERE CourseID = @CourseID AND StudentID = @StudentID\n";

                    SqlCommand cmdDeleteStudentFromCourse = new SqlCommand(queryDeleteStudentFromCourse, sqlConnection);
                    cmdDeleteStudentFromCourse.Parameters.AddWithValue("@CourseID", courseID);
                    cmdDeleteStudentFromCourse.Parameters.AddWithValue("@StudentID", studentID);

                    int studentRowsRemoved = cmdDeleteStudentFromCourse.ExecuteNonQuery();

                    sqlConnection.Close();

                    return studentRowsRemoved > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Trainer> GetTrainersNotInCourse(int courseID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                Trainer trainer;
                List<Trainer> trainersNotInCourse = new List<Trainer>();

                try
                {
                    sqlConnection.Open();

                    string queryTrainersNotInCourse =
                        "Select * From Trainer Where Trainer.ID NOT IN " +
                        "(SELECT TrainerID FROM TrainerInCourse WHERE CourseID = @ID)";

                    SqlCommand cmdGetTrainersNotInCourse = new SqlCommand(queryTrainersNotInCourse, sqlConnection);
                    cmdGetTrainersNotInCourse.Parameters.AddWithValue("@ID", courseID);

                    SqlDataReader reader = cmdGetTrainersNotInCourse.ExecuteReader();

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

                        trainer = new Trainer(id, firstName, lastName, subject);

                        trainersNotInCourse.Add(trainer);

                    }
                    reader.Close();

                    sqlConnection.Close();

                    return trainersNotInCourse;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool AddTrainerToCourse(int courseID, int trainerID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryAddTrainerToCourse = "INSERT INTO TrainerInCourse(CourseID, TrainerID)\n" +
                                                     "VALUES (@CourseID, @TrainerID)";

                    SqlCommand cmdAddTrainerToCourse = new SqlCommand(queryAddTrainerToCourse, sqlConnection);
                    cmdAddTrainerToCourse.Parameters.AddWithValue("@CourseID", courseID);
                    cmdAddTrainerToCourse.Parameters.AddWithValue("@TrainerID", trainerID);

                    int coursesAdded = cmdAddTrainerToCourse.ExecuteNonQuery();

                    sqlConnection.Close();

                    return coursesAdded == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool RemoveTrainerFromCourse(int courseID, int trainerID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryDeleteTrainerFromCourse =
                        "DELETE FROM TrainerInCourse WHERE CourseID = @CourseID AND TrainerID = @TrainerID\n";

                    SqlCommand cmdDeleteTrainerFromCourse = new SqlCommand(queryDeleteTrainerFromCourse, sqlConnection);
                    cmdDeleteTrainerFromCourse.Parameters.AddWithValue("@CourseID", courseID);
                    cmdDeleteTrainerFromCourse.Parameters.AddWithValue("@TrainerID", trainerID);

                    int trainerRowsRemoved = cmdDeleteTrainerFromCourse.ExecuteNonQuery();

                    sqlConnection.Close();

                    return trainerRowsRemoved > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Assignment> GetAssignmentsNotInCourse(int courseID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                Assignment assignment;
                List<Assignment> assignmentsNotInCourse = new List<Assignment>();

                try
                {
                    sqlConnection.Open();

                    string queryAssignmentsNotInCourse =
                        "Select * From Assignment Where Assignment.ID NOT IN " +
                        "(SELECT AssignmentID FROM AssignmentInCourse WHERE CourseID = @ID)";

                    SqlCommand cmdGetAssignmentsNotInCourse = new SqlCommand(queryAssignmentsNotInCourse, sqlConnection);
                    cmdGetAssignmentsNotInCourse.Parameters.AddWithValue("@ID", courseID);

                    SqlDataReader reader = cmdGetAssignmentsNotInCourse.ExecuteReader();

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

                        assignment = new Assignment(id, title, description, submissionDateTime, oralMark, totalMark);

                        assignmentsNotInCourse.Add(assignment);

                    }
                    reader.Close();

                    sqlConnection.Close();

                    return assignmentsNotInCourse;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        public bool AddAssignmentToCourse(int courseID, int assignmentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryAddAssignmentToCourse = "INSERT INTO AssignmentInCourse(CourseID, AssignmentID)\n" +
                                                        "VALUES (@CourseID, @AssignmentID)";

                    SqlCommand cmdAddAssignmentToCourse = new SqlCommand(queryAddAssignmentToCourse, sqlConnection);
                    cmdAddAssignmentToCourse.Parameters.AddWithValue("@CourseID", courseID);
                    cmdAddAssignmentToCourse.Parameters.AddWithValue("@AssignmentID", assignmentID);

                    int assignmentsAdded = cmdAddAssignmentToCourse.ExecuteNonQuery();

                    sqlConnection.Close();

                    return assignmentsAdded == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool RemoveAssignmentFromCourse(int courseID, int assignmentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryDeleteAssignmentFromCourse =
                        "DELETE FROM Course_Student_Assignment WHERE CourseID = @CourseID AND AssignmentID = @AssignmentID\n" +
                        "DELETE FROM AssignmentInCourse WHERE CourseID = @CourseID AND AssignmentID = @AssignmentID\n";

                    SqlCommand cmdDeleteAssignmentFromCourse = new SqlCommand(queryDeleteAssignmentFromCourse, sqlConnection);
                    cmdDeleteAssignmentFromCourse.Parameters.AddWithValue("@CourseID", courseID);
                    cmdDeleteAssignmentFromCourse.Parameters.AddWithValue("@AssignmentID", assignmentID);

                    int assignmentRowsRemoved = cmdDeleteAssignmentFromCourse.ExecuteNonQuery();

                    sqlConnection.Close();

                    return assignmentRowsRemoved > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool AddStudentToAssignment(int courseID, int assignmentID, int studentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryAddStudentToAssignment = "INSERT INTO Course_Student_Assignment(CourseID, AssignmentID, StudentID)\n" +
                                                         "VALUES (@CourseID, @AssignmentID, @StudentID)";

                    SqlCommand cmdAddStudentToAssignment = new SqlCommand(queryAddStudentToAssignment, sqlConnection);
                    cmdAddStudentToAssignment.Parameters.AddWithValue("@CourseID", courseID);
                    cmdAddStudentToAssignment.Parameters.AddWithValue("@AssignmentID", assignmentID);
                    cmdAddStudentToAssignment.Parameters.AddWithValue("@StudentID", studentID);

                    int studentsAdded = cmdAddStudentToAssignment.ExecuteNonQuery();

                    sqlConnection.Close();

                    return studentsAdded == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool RemoveStudentFromAssignment(int courseID, int assignmentID, int studentID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    string queryRemoveStudentFromAssignment = 
                        "DELETE FROM Course_Student_Assignment \n" +
                        "WHERE CourseID = @CourseID AND AssignmentID = @AssignmentID AND StudentID = @StudentID";

                    SqlCommand cmdRemoveStudentFromAssignment = new SqlCommand(queryRemoveStudentFromAssignment, sqlConnection);
                    cmdRemoveStudentFromAssignment.Parameters.AddWithValue("@CourseID", courseID);
                    cmdRemoveStudentFromAssignment.Parameters.AddWithValue("@AssignmentID", assignmentID);
                    cmdRemoveStudentFromAssignment.Parameters.AddWithValue("@StudentID", studentID);

                    int studentsRemoved = cmdRemoveStudentFromAssignment.ExecuteNonQuery();

                    sqlConnection.Close();

                    return studentsRemoved == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
    
}

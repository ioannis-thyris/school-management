using Dapper;
using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.DataAccess.CourseDataAccess
{
    internal class CourseDataAccessDapper : ICourseDataAccess
    {
        public string connectionString { get; } = ConfigurationManager.ConnectionStrings["PartB_Database"].ConnectionString;

        public bool Insert(Course course)
        {
            string queryInsertCourse = "INSERT INTO Course(Title, Stream, Type, StartDate, EndDate) \n" +
                                       "VALUES (@Title, @Stream, @Type, @StartDate, @EndDate)";

            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "@Title", course.Title },
                { "@Stream", (int)course.Stream },
                { "@Type", (int)course.Type },
                { "@StartDate", course.StartDate?.ToString("yyyy/MM/dd") },
                { "@EndDate", course.EndDate?.ToString("yyyy/MM/dd") }
            };

            DynamicParameters parameters = new DynamicParameters(dictionary);

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int coursesAdded = sqlConnection.Execute(queryInsertCourse, parameters);

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
            string queryGetCourseByID = "SELECT * FROM Course WHERE ID = @ID";
            var parameters = new { ID = courseID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    Course course = sqlConnection.QuerySingleOrDefault<Course>(queryGetCourseByID, parameters);

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
            string queryGetAllCourses = "SELECT * FROM Course";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    var allCourses = sqlConnection.Query<Course>(queryGetAllCourses);

                    return allCourses.ToList();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        public bool Update(int courseID, Course course)
        {
            string queryUpdateCourse = "UPDATE Course\n" +
                                       "SET Title = @Title, Stream = @Stream, Type = @Type, StartDate = @StartDate, EndDate = @EndDate \n" +
                                       "WHERE ID = @ID";

            Dictionary<string, object> dictionary = new Dictionary<string, object>
            {
                { "@Title", course.Title },
                { "@Stream", (int)course.Stream },
                { "@Type", (int)course.Type },
                { "@StartDate", course.StartDate?.ToString("yyyy/MM/dd") },
                { "@EndDate", course.EndDate?.ToString("yyyy/MM/dd") },
                { "@ID", courseID }
            };

            DynamicParameters parameters = new DynamicParameters(dictionary);

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int coursesUpdated = sqlConnection.Execute(queryUpdateCourse, parameters);

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
            string queryDeleteFromCompositeTables = "DELETE FROM Course_Student_Assignment WHERE CourseID = @ID\n" +
                                                    "DELETE FROM AssignmentInCourse WHERE CourseID = @ID\n" +
                                                    "DELETE FROM StudentInCourse WHERE CourseID = @ID\n";
            string queryDeleteFromCourseTable = "DELETE FROM Course WHERE ID = @ID";

            string queryDelete = $"{queryDeleteFromCompositeTables}\n{queryDeleteFromCourseTable}";

            var parameters = new { ID = courseID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int courseRowsDeleted = sqlConnection.Execute(queryDelete, parameters);

                    return courseRowsDeleted > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool Exists(int courseID)
        {
            string queryExists = "SELECT * FROM Course WHERE ID = @ID";
            var parameters = new { ID = courseID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    bool courseExists = sqlConnection.QuerySingleOrDefault<Course>(queryExists, parameters) != null;

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
            string queryGetCourse =
                "SELECT * " +
                "FROM Course " +
                "WHERE ID = @CourseID";

            string queryGetTrainersInCourse =
                "SELECT * " +
                "FROM TrainerInCourse INNER JOIN Trainer ON TrainerID = Trainer.ID " +
                "WHERE CourseID = @CourseID";

            string queryGetStudentsInCourse =
                "SELECT * " +
                "FROM StudentInCourse INNER JOIN Student ON StudentID = Student.ID " +
                "WHERE CourseID = @CourseID";

            string queryGetAssignmentsInCourse =
                "SELECT * " +
                "FROM AssignmentInCourse INNER JOIN Assignment ON AssignmentID = Assignment.ID " +
                "WHERE CourseID = @CourseID";

            string queryGetStudentsWithAssignment =
                "SELECT * " +
                "FROM Course_Student_Assignment " +
                "INNER JOIN Student ON StudentID = Student.ID " +
                "INNER JOIN Assignment ON AssignmentID = Assignment.ID " +
                "WHERE CourseID = @CourseID AND AssignmentID IN @AssignmentID ";


            var parameters = new { CourseID = courseID};

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    Course courseToGet = sqlConnection.QuerySingleOrDefault<Course>(queryGetCourse, parameters);

                    if (courseToGet != null)
                    {
                        var trainersOfCourse = sqlConnection.Query<Course, Trainer, Trainer>(queryGetTrainersInCourse, (course, trainer) =>
                        {
                            trainer.Courses.Add(course);
                            return trainer;
                        }, parameters, splitOn: "ID");

                        courseToGet.Trainers = trainersOfCourse.ToList();

                        var studentsOfCourse = sqlConnection.Query<Course, Student, Student>(queryGetStudentsInCourse, (course, student) =>
                        {
                            student.Courses.Add(course);
                            return student;
                        }, parameters, splitOn: "ID");

                        courseToGet.Students = studentsOfCourse.ToList();

                        var assignmentsEmpty = sqlConnection.Query<Course, Assignment, Assignment>(queryGetAssignmentsInCourse, (course, assignment) =>
                        {
                            assignment.Courses.Add(course);
                            return assignment;
                        }, parameters, splitOn: "ID");

                        List<int> iDlist = new List<int>();
                        assignmentsEmpty.ToList().ForEach(a => iDlist.Add(a.ID));

                        var param = new { CourseID = courseID, AssignmentID = iDlist };

                        var assignmentsStudentsEntries = sqlConnection.Query<Student, Assignment, Assignment>(queryGetStudentsWithAssignment, (student, assignment) =>
                        {
                            assignment.Students.Add(student);
                            return assignment;
                        }, param, splitOn: "ID");

                        var assignmentsPopulated = assignmentsStudentsEntries.GroupBy(a => a.ID).Select(g =>
                        {
                            var groupedAssignment = g.First();
                            groupedAssignment.Students = g.Select(a => a.Students.Single()).ToList();
                            return groupedAssignment;
                        });

                        var assignmentsOfCourse = assignmentsPopulated.Union(assignmentsEmpty)
                                                                      .GroupBy(a => a.ID)
                                                                      .Select(g => g.First())
                                                                      .ToList();

                        courseToGet.Assignments = assignmentsOfCourse;
                    }

                    return courseToGet;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public List<Student> GetStudentsNotInCourse(int courseID)
        {
            string queryStudentsNotInCourse =
                "Select * From Student Where Student.ID NOT IN " +
                "(SELECT StudentID FROM StudentInCourse WHERE CourseID = @ID)";
            var parameters = new { ID = courseID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    var studentsNotInCourse = sqlConnection.Query<Student>(queryStudentsNotInCourse, parameters);

                    return studentsNotInCourse.ToList();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        public bool AddStudentToCourse(int courseID, int studentID)
        {
            string queryAddStudentToCourse = "INSERT INTO StudentInCourse(CourseID, StudentID)\n" +
                                             "VALUES (@CourseID, @StudentID)";

            var parameters = new { CourseID = courseID, StudentID = studentID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int studentsAdded = sqlConnection.Execute(queryAddStudentToCourse, parameters);

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
            string queryDeleteStudentFromCourse =
                "DELETE FROM Course_Student_Assignment WHERE CourseID = @CourseID AND StudentID = @StudentID\n" +
                "DELETE FROM StudentInCourse WHERE CourseID = @CourseID AND StudentID = @StudentID\n";

            var parameters = new { CourseID = courseID, StudentID = studentID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int studentRowsRemoved = sqlConnection.Execute(queryDeleteStudentFromCourse, parameters);

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
            string queryTrainersNotInCourse =
                "Select * From Trainer Where Trainer.ID NOT IN " +
                "(SELECT TrainerID FROM TrainerInCourse WHERE CourseID = @ID)";
            var parameters = new { ID = courseID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    var trainersNotInCourse = sqlConnection.Query<Trainer>(queryTrainersNotInCourse, parameters);

                    return trainersNotInCourse.ToList();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        public bool AddTrainerToCourse(int courseID, int trainerID)
        {
            string queryAddTrainerToCourse = "INSERT INTO TrainerInCourse(CourseID, TrainerID)\n" +
                                             "VALUES (@CourseID, @TrainerID)";
            var parameters = new { CourseID = courseID, TrainerID = trainerID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int trainersAdded = sqlConnection.Execute(queryAddTrainerToCourse, parameters);

                    return trainersAdded == 1;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public bool RemoveTrainerFromCourse(int courseID, int trainerID)
        {
            string queryDeleteTrainerFromCourse =
                "DELETE FROM TrainerInCourse WHERE CourseID = @CourseID AND TrainerID = @TrainerID\n";
            var parameters = new { CourseID = courseID, TrainerID = trainerID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int trainerRowsRemoved = sqlConnection.Execute(queryDeleteTrainerFromCourse, parameters);

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
            string queryAssignmentsNotInCourse =
                "Select * From Assignment Where Assignment.ID NOT IN " +
                "(SELECT AssignmentID FROM AssignmentInCourse WHERE CourseID = @ID)";
            var parameters = new { ID = courseID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    var assignmentsNotInCourse = sqlConnection.Query<Assignment>(queryAssignmentsNotInCourse, parameters);

                    return assignmentsNotInCourse.ToList();
                }
                catch (Exception)
                {
                    throw new Exception();
                }
            }
        }

        public bool AddAssignmentToCourse(int courseID, int assignmentID)
        {
            string queryAddAssignmentToCourse = "INSERT INTO AssignmentInCourse(CourseID, AssignmentID)\n" +
                                                "VALUES (@CourseID, @AssignmentID)";
            var parameters = new { CourseID = courseID, AssignmentID = assignmentID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int assignmentsAdded = sqlConnection.Execute(queryAddAssignmentToCourse, parameters);

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
            string queryDeleteAssignmentFromCourse =
                "DELETE FROM Course_Student_Assignment WHERE CourseID = @CourseID AND AssignmentID = @AssignmentID\n" +
                "DELETE FROM AssignmentInCourse WHERE CourseID = @CourseID AND AssignmentID = @AssignmentID\n";
            var parameters = new { CourseID = courseID, AssignmentID = assignmentID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int assignmentRowsRemoved = sqlConnection.Execute(queryDeleteAssignmentFromCourse, parameters);

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
            string queryAddStudentToAssignment = "INSERT INTO Course_Student_Assignment(CourseID, AssignmentID, StudentID)\n" +
                                                 "VALUES (@CourseID, @AssignmentID, @StudentID)";
            var parameters = new { CourseID = courseID, AssignmentID = assignmentID, StudentID = studentID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int studentsAdded = sqlConnection.Execute(queryAddStudentToAssignment, parameters);

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
            string queryRemoveStudentFromAssignment =
                "DELETE FROM Course_Student_Assignment \n" +
                "WHERE CourseID = @CourseID AND AssignmentID = @AssignmentID AND StudentID = @StudentID";
            var parameters = new { CourseID = courseID, AssignmentID = assignmentID, StudentID = studentID };

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                try
                {
                    int studentsRemoved = sqlConnection.Execute(queryRemoveStudentFromAssignment, parameters);

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

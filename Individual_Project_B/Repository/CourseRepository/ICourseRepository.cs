using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Repository.CourseRepository
{
    interface ICourseRepository : IGenericRepository<Course>
    {
        string GetByIDDetailed(int id);
        string GetStudentsNotInCourse(int id);
        string AddStudentToCourse(int courseID, int studentID);
        string RemoveStudentFromCourse(int courseID, int studentID);
        string GetTrainersNotInCourse(int id);
        string AddTrainerToCourse(int courseID, int trainerID);
        string RemoveTrainerFromCourse(int courseID, int trainerID);
        string GetAssignmentsNotInCourse(int id);
        string AddAssignmentToCourse(int courseID, int assignmentID);
        string RemoveAssignmentFromCourse(int courseID, int assignmentID);
        string AddStudentToAssignment(int courseID, int assignmentID, int studentID);
        string RemoveStudentFromAssignment(int courseID, int assignmentID, int studentID);

    }
}

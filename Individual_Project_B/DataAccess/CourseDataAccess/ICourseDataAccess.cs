using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.DataAccess.CourseDataAccess
{
    interface ICourseDataAccess : IDataAccess<Course>
    {
        Course GetByIDDetailed(int courseID);
        List<Student> GetStudentsNotInCourse(int courseID);
        bool AddStudentToCourse(int courseID, int studentID);
        bool RemoveStudentFromCourse(int courseID, int studentID);
        List<Trainer> GetTrainersNotInCourse(int courseID);
        bool AddTrainerToCourse(int courseID, int trainerID);
        bool RemoveTrainerFromCourse(int courseID, int trainerID);
        List<Assignment> GetAssignmentsNotInCourse(int courseID);
        bool AddAssignmentToCourse(int courseID, int assignmentID);
        bool RemoveAssignmentFromCourse(int courseID, int assignmentID);
        bool AddStudentToAssignment(int courseID, int assignmentID, int studentID);
        bool RemoveStudentFromAssignment(int courseID, int assignmentID, int studentID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Controllers.CourseController
{
    public interface ICourseController : IController
    {
        string ReadByIDDetailed(int idInput);
        string StudentsNotInCourse(int id);
        string AddStudentToCourse(int courseID, string studentID);
        string RemoveStudentFromCourse(int courseID, string studentID);
        string TrainersNotInCourse(int id);
        string AddTrainerToCourse(int courseID, string trainerID);
        string RemoveTrainerFromCourse(int courseID, string trainerID);
        string AssignmentsNotInCourse(int id);
        string AddAssignmentToCourse(int courseID, string assignmentID);
        string RemoveAssignmentFromCourse(int courseID, string assignmentID);
        string AddStudentToAssignment(int courseID, string assignmentIDInput, string studentIDInput);
        string RemoveStudentFromAssignment(int courseID, string assignmentIDInput, string studentIDInput);

    }
}

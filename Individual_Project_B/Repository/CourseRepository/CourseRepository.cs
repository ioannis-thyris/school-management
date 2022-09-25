using Individual_Project_B.DataAccess;
using Individual_Project_B.DataAccess.CourseDataAccess;
using Individual_Project_B.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Repository.CourseRepository
{
    internal class CourseRepository : ICourseRepository
    {
        public ICourseDataAccess CourseDB { get; }

        public CourseRepository(ICourseDataAccess courseDB)
        {
            CourseDB = courseDB;
        }

        public string Insert(Course entity)
        {
            try
            {
                bool addSuccessful = CourseDB.Insert(entity);
                string successMessage = "\nSuccessfully inserted new Course to the Database.";
                string failMessage = "\nThe Course was not inserted successfully.";

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
                Course course = CourseDB.GetByID(id);
                string idNotFoundMessage = $" A Course with ID -{id}- does not exist in the Database\n";

                return course is null ? idNotFoundMessage : course.Info;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetAll()
        {
            List<Course> courses = CourseDB.GetAll();
            StringBuilder sb = new StringBuilder();

            courses.ForEach(s => sb.AppendLine(s.Info));

            return sb.ToString();
        }

        public string Update(int id, Course entity)
        {
            try
            {
                bool updateSuccessful = CourseDB.Update(id, entity);
                string successMessage = "\nSuccessfully updated Course's info in the Database.";
                string failMessage = $"\nThe Course's info were not updated successfully.";

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
                bool deleteSuccessful = CourseDB.Delete(id);
                string successMessage = $"\nSuccessfully deleted Course with ID -{id}-";
                string failMessage = $"\nThe Course was not deleted successfully.";

                return deleteSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public bool Exists(int id, out string existanceMessage)
        {
            bool entityExists = CourseDB.Exists(id);
            existanceMessage = entityExists ? "\nEntity exists" : $" A Course with ID -{id}- does not exist in the Database\n";

            return entityExists;
        }

        public string GetByIDDetailed(int id)
        {
            try
            {
                Course course = CourseDB.GetByIDDetailed(id);
                string idNotFoundMessage = $" A Course with ID -{id}- does not exist in the Database\n";

                return course is null ? idNotFoundMessage : course.InfoDetailed;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetStudentsNotInCourse(int id)
        {
            List<Student> studentsNotInCourse = CourseDB.GetStudentsNotInCourse(id);

            StringBuilder sb = new StringBuilder();

            studentsNotInCourse.ForEach(stu => sb.AppendLine(stu.InfoShort));

            return sb.ToString();
        }

        public string AddStudentToCourse(int courseID, int studentID)
        {
            try
            {
                bool addSuccessful = CourseDB.AddStudentToCourse(courseID, studentID);
                string successMessage = "\nSuccessfully added new Student to the Course.";
                string failMessage = "\nThe Student was not added successfully.";

                return addSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public string RemoveStudentFromCourse(int courseID, int studentID)
        {
            try
            {
                bool addSuccessful = CourseDB.RemoveStudentFromCourse(courseID, studentID);
                string successMessage = $"\nSuccessfully deleted Student with ID -{studentID}- from the Course.";
                string failMessage = "\nThe Student was not removed successfully.";

                return addSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetTrainersNotInCourse(int id)
        {
            List<Trainer> trainersNotInCourse = CourseDB.GetTrainersNotInCourse(id);

            StringBuilder sb = new StringBuilder();

            trainersNotInCourse.ForEach(t => sb.AppendLine(t.InfoShort));

            return sb.ToString();
        }

        public string AddTrainerToCourse(int courseID, int trainerID)
        {
            try
            {
                bool addSuccessful = CourseDB.AddTrainerToCourse(courseID, trainerID);
                string successMessage = "\nSuccessfully added new Trainer to the Course.";
                string failMessage = "\nThe Trainer was not added successfully.";

                return addSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string RemoveTrainerFromCourse(int courseID, int trainerID)
        {
            try
            {
                bool addSuccessful = CourseDB.RemoveTrainerFromCourse(courseID, trainerID);
                string successMessage = $"\nSuccessfully removed Trainer with ID -{trainerID}- from the Course.";
                string failMessage = "\nThe Trainer was not removed successfully.";

                return addSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetAssignmentsNotInCourse(int id)
        {
            List<Assignment> assignmentNotInCourse = CourseDB.GetAssignmentsNotInCourse(id);

            StringBuilder sb = new StringBuilder();

            assignmentNotInCourse.ForEach(a => sb.AppendLine(a.InfoShort));

            return sb.ToString();
        }

        public string AddAssignmentToCourse(int courseID, int assignmentID)
        {
            try
            {
                bool addSuccessful = CourseDB.AddAssignmentToCourse(courseID, assignmentID);
                string successMessage = "\nSuccessfully added new Assignment to the Course.";
                string failMessage = "\nThe Assignment was not added successfully.";

                return addSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string RemoveAssignmentFromCourse(int courseID, int assignmentID)
        {
            try
            {
                bool addSuccessful = CourseDB.RemoveAssignmentFromCourse(courseID, assignmentID);
                string successMessage = $"\nSuccessfully deleted Assignment with ID -{assignmentID}- from the Course.";
                string failMessage = "\nThe Assignment was not removed successfully.";

                return addSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string AddStudentToAssignment(int courseID, int assignmentID, int studentID)
        {
            try
            {
                bool addSuccessful = CourseDB.AddStudentToAssignment(courseID, assignmentID, studentID);
                string successMessage = "\nSuccessfully added Student to the Assignment.";
                string failMessage = "\nThe Student was not added successfully to the Assignment.";

                return addSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string RemoveStudentFromAssignment(int courseID, int assignmentID, int studentID)
        {
            try
            {
                bool addSuccessful = CourseDB.RemoveStudentFromAssignment(courseID, assignmentID, studentID);
                string successMessage = "\nSuccessfully removed Student from the Assignment.";
                string failMessage = "\nThe Student was not removed successfully from the Assignment.";

                return addSuccessful ? successMessage : failMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}

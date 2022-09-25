using Individual_Project_B.Controllers.AssignmentController;
using Individual_Project_B.Controllers.CourseController;
using Individual_Project_B.Controllers.StudentController;
using Individual_Project_B.Controllers.TrainerController;
using Individual_Project_B.DataAccess;
using Individual_Project_B.DataAccess.AssignmentDataAccess;
using Individual_Project_B.DataAccess.CourseDataAccess;
using Individual_Project_B.DataAccess.StudentDataAccess;
using Individual_Project_B.DataAccess.TrainerDataAccess;
using Individual_Project_B.Factory;
using Individual_Project_B.Models;
using Individual_Project_B.Repository;
using Individual_Project_B.Repository.AssignmentRepository;
using Individual_Project_B.Repository.CourseRepository;
using Individual_Project_B.Repository.StudentRepository;
using Individual_Project_B.Repository.TrainerRepository;
using Individual_Project_B.Views;
using Individual_Project_B.Views.AssignmentView;
using Individual_Project_B.Views.CourseView;
using Individual_Project_B.Views.HomeView;
using Individual_Project_B.Views.StudentView;
using Individual_Project_B.Views.TrainerView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B
{
    internal class ApplicationFactory
    {

        public void Start()
        {
            IView entryPoint = GetHomeView();
            entryPoint.Menu();
        }


        private EntityFactory GetEntityFactory() => new EntityFactory();


        private IView GetHomeView()
        {
            return new HomeView(GetAssignmentView(), GetCourseView(), GetStudentView(), GetTrainerView());
        }

        #region StudentAppFactory
        private IStudentDataAccess GetStudentDataAccess() => new StudentDataAccess();

        private IStudentRepository GetStudentRepository()
        {
            return new StudentRepository(GetStudentDataAccess());
        }

        private IStudentController GetStudentController()
        {
            return new StudentController(GetEntityFactory(), GetStudentRepository());
        }

        private IStudentView GetStudentView()
        {
            return new StudentView(GetStudentController());
        }
        #endregion

        #region TrainerAppFactory
        private IDataAccess<Trainer> GetTrainerDataAccess() => new TrainerDataAccess();

        private IGenericRepository<Trainer> GetTrainerRepository()
        {
            return new TrainerRepository(GetTrainerDataAccess());
        }

        private ITrainerController GetTrainerController()
        {
            return new TrainerController(GetEntityFactory(), GetTrainerRepository());
        }

        private ITrainerView GetTrainerView()
        {
            return new TrainerView(GetTrainerController());
        }
        #endregion

        #region AssignmentAppFactory
        private IDataAccess<Assignment> GetAssignmentDataAccess() => new AssignmentDataAccess();

        private IGenericRepository<Assignment> GetAssignmentRepository()
        {
            return new AssignmentRepository(GetAssignmentDataAccess());
        }

        private IAssignmentController GetAssignmentController()
        {
            return new AssignmentController(GetEntityFactory(), GetAssignmentRepository());
        }

        private IAssignmentView GetAssignmentView()
        {
            return new AssignmentView(GetAssignmentController());
        }
        #endregion

        #region CourseAppFactory
        private ICourseDataAccess GetCourseDataAccess() => new CourseDataAccess();

        private ICourseRepository GetCourseRepository()
        {
            return new CourseRepository(GetCourseDataAccess());
        }

        private ICourseController GetCourseController()
        {
            return new CourseController(GetEntityFactory(), GetCourseRepository());
        }

        private ICourseView GetCourseView()
        {
            return new CourseView(GetCourseController());
        }
        #endregion

    }
}

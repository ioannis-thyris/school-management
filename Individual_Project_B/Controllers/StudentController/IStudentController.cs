﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Individual_Project_B.Controllers.StudentController
{
    public interface IStudentController : IController
    {
        string ReadAllInMultipleCourses();
    }
}
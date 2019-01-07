using OnlineCourse.Business.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Business.Service.Interfaces
{
    public interface ICourseService
    {
        void Create(CourseDto course);

        List<CourseDto> GetAll();
    }
}

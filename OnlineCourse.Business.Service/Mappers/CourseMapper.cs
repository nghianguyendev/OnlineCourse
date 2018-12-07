using System;

using System.Collections.Generic;
using System.Text;
using OnlineCourse.Business.Model;
using OnlineCourse.Data.Repo.Entity;

namespace OnlineCourse.Business.Service.Mappers
{
    public static class CourseMapper
    {
        public static Course ToEntity(CourseDto dto)
        {
            return new Course()
            {
                Id = dto.Id,
                Name = dto.Name,
                CategoryId = dto.CategoryId
            };
        }
    }
}

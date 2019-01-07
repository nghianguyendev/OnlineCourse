using System;

using System.Collections.Generic;
using System.Linq;
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
                Url = dto.Url,
                CategoryId = dto.CategoryId
            };
        }

        public static CourseDto ToDto(Course dto)
        {
            return new CourseDto()
            {
                Id = dto.Id,
                Name = dto.Name,
                Url = dto.Url,
                CategoryId = dto.CategoryId
            };
        }

        public static List<CourseDto> ToListDto(List<Course> courses)
        {
            return courses.Select(x => ToDto(x)).ToList();
        }
    }
}

using System;
using OnlineCourse.Business.Model.DTO.Base;

namespace OnlineCourse.Business.Model
{
    public class CourseDto : BaseDto
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int CategoryId { get; set; }
    }
}

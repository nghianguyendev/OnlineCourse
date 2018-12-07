using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OnlineCourse.Data.Repo.Entity.Base;

namespace OnlineCourse.Data.Repo.Entity
{
    [Table("OCCourseRegistration")]
    public class CourseRegistration : BaseEntity
    {
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public int CourseId { get; set; }

        [ForeignKey("CourseId")]
        public Course Course { get; set; }
    }
}

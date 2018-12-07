using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OnlineCourse.Data.Repo.Entity.Base;

namespace OnlineCourse.Data.Repo.Entity
{
    [Table("OCCourse")]
    public class Course : BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}

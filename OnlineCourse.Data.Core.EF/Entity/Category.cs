using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OnlineCourse.Data.Repo.Entity.Base;

namespace OnlineCourse.Data.Repo.Entity
{
    [Table("OCCategory")]
    public class Category : BaseEntity
    {
        public string Name { get; set; }
    }
}

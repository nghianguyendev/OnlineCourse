using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using OnlineCourse.Data.Repo.Entity.Base;

namespace OnlineCourse.Data.Repo.Entity
{
    [Table("OCUser")]
    public class User : BaseEntity
    {
        public string Username { get; set; }

        public string  Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Token { get; set; }
    }
}

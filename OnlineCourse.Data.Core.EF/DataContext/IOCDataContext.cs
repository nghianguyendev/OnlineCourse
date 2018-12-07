using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Data.Repo.Entity;

namespace OnlineCourse.Data.Repo.DataContext
{
    public interface IOCDataContext: IDisposable, IDataSession
    {
        DbSet<User> User { get; set; }
        DbSet<Category> Category { get; set; }
        DbSet<Course> Course { get; set; }
        DbSet<CourseRegistration> CourseRegistration{ get; set; }
    }
}

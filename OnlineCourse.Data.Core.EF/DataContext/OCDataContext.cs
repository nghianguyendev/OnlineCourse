using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using OnlineCourse.Data.Repo.Entity;
using Microsoft.IdentityModel.Protocols;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace OnlineCourse.Data.Repo.DataContext
{
    public class OCDataContext : DbContext, IOCDataContext
    {
        public OCDataContext(DbContextOptions<OCDataContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<CourseRegistration> CourseRegistration { get; set; }

        public IDbContextTransaction BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}

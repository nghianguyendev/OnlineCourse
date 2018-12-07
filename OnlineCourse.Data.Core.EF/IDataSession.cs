using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data.Repo
{
    public interface IDataSession : IDisposable
    {

        ChangeTracker ChangeTracker { get; }
        DbSet<T> Set<T>() where T : class;
        
        int SaveChanges();

        IDbContextTransaction BeginTransaction();

        EntityEntry Entry(object entity);

        Task<int> SaveChangesAsync();

    }
}

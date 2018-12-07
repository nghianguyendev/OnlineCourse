using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data.Repo
{
    public interface IRepository : IReadOnlyRepository
    {
        void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class;

        void Delete<TEntity>(TEntity entity)
            where TEntity : class;

        int Save();

        Task SaveAsync();

        int SaveWithTransaction();

        Task SaveWithTransactionAsync();

    }
}

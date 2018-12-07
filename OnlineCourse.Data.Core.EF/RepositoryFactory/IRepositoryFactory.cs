using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Data.Repo.RepositoryFactory
{
    public interface IRepositoryFactory<TContext> where TContext: IDataSession
    {
        IReadOnlyRepository CreateReadOnlyRepository(TContext context);
        IRepository CreateRepository(TContext context);
    }
}

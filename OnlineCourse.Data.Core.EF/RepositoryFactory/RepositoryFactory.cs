using OnlineCourse.Common.Logger;
using OnlineCourse.Common.Logger.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineCourse.Data.Repo.RepositoryFactory
{
    public class RepositoryFactory<TContext> : IRepositoryFactory<TContext> where TContext: IDataSession
    {
        protected static readonly NLogger<RepositoryFactory<TContext>> Logger =
            new NLogger<RepositoryFactory<TContext>>();

        public IReadOnlyRepository CreateReadOnlyRepository(TContext context)
        {
            try
            {
                return new EntityFrameworkReadOnlyRepository<TContext>(context);
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "An error occurred in CreateReadonlyRepo", e));
                throw;
            }
        }

        public IRepository CreateRepository(TContext context)
        {
            try
            {
                return new EntityFrameworkRepository<TContext>(context);
            }      
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "An error occurred in CreateRepo", e));
                throw;
            }
        }
    }
}

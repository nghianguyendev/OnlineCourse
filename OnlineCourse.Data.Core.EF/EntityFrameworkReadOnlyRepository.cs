using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Logger;
using OnlineCourse.Common.Logger.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data.Repo
{
    public class EntityFrameworkReadOnlyRepository<TContext> : IReadOnlyRepository
    where TContext : IDataSession
    {
        protected static readonly Common.Logger.NLogger<EntityFrameworkRepository<TContext>> Logger =
            new NLogger<EntityFrameworkRepository<TContext>>();

        protected readonly TContext Context;
        public EntityFrameworkReadOnlyRepository(TContext context)
        {
            this.Context = context;
        }

        public virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool withTracking = false)
            where TEntity : class
        {
            try
            {
                includeProperties = includeProperties ?? String.Empty;
                IQueryable<TEntity> query = this.Context.Set<TEntity>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    query = orderBy(query);
                }

                if (skip.HasValue)
                {
                    query = query.Skip(skip.Value);
                }

                if (take.HasValue)
                {
                    query = query.Take(take.Value);
                }
                if (withTracking)
                {
                    return query;
                }
                else
                {
                    return query.AsNoTracking();
                }
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while getting the data in GetQueryable;", e));
                throw;
            }
        }

        public virtual IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool withTracking = false)
            where TEntity : class
        {
            try
            {
                return this.GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take, withTracking).ToList();
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while getting the data in GetAll;", e));
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool withTracking = false)
            where TEntity : class
        {
            try
            {
                return await this.GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take, withTracking).ToListAsync();
            }

            catch (AggregateException ae)
            {
                var newex = ae.Flatten();
                StringBuilder sb = new StringBuilder();
                newex.InnerExceptions.ToList().ForEach(t => sb.Append($"::{t} ##"));
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        $"an Error occurred while getting the data in GetAllAsync; {sb}",
                        ae));
                throw;
            }

            catch (Exception e)
            {
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        "an Error occurred in GetAllAsync;",
                        e));
                throw;
            }
        }

        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool withTracking = false)
            where TEntity : class
        {
            try
            {
                return this.GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take, withTracking).ToList();
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while getting the data in Get;", e));
                throw;
            }
        }

        public virtual async Task<IEnumerable<TEntity>> GetAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null,
            bool withTracking = false)
            where TEntity : class
        {
            try
            {
                return await this.GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take, withTracking).ToListAsync();
            }
            catch (AggregateException ae)
            {
                var newex = ae.Flatten();
                StringBuilder sb = new StringBuilder();
                newex.InnerExceptions.ToList().ForEach(t => sb.Append($"::{t} ##"));
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        $"an Error occurred while getting the data in GetAsync; {sb}",
                        ae));
                throw;
            }

            catch (Exception e)
            {
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        "an Error occurred in GetAsync;",
                        e));
                throw;
            }
        }

        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "",
            bool withTracking = false)
            where TEntity : class
        {
            try
            {
                return this.GetQueryable<TEntity>(filter, null, includeProperties, null, null, withTracking).SingleOrDefault();
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while getting the data in GetOne;", e));
                throw;
            }
        }

        public virtual async Task<TEntity> GetOneAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null,
            bool withTracking = false)
            where TEntity : class
        {
            try
            {
                return await this.GetQueryable<TEntity>(filter, null, includeProperties, null, null, withTracking).SingleOrDefaultAsync();
            }
            catch (AggregateException ae)
            {
                var newex = ae.Flatten();
                StringBuilder sb = new StringBuilder();
                newex.InnerExceptions.ToList().ForEach(t => sb.Append($"::{t} ##"));
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        $"an Error occurred while getting the data in GetOneAsync; {sb}",
                        ae));
                throw;
            }

            catch (Exception e)
            {
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        "an Error occurred in GetOneAsync;",
                        e));
                throw;
            }
        }

        public virtual TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",
            bool withTracking = false)
            where TEntity : class
        {
            try
            {
                return this.GetQueryable<TEntity>(filter, orderBy, includeProperties, null, null, withTracking).FirstOrDefault();
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while getting the data in GetFirst;", e));
                throw;
            }
        }

        public virtual async Task<TEntity> GetFirstAsync<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            bool withTracking = false)
            where TEntity : class
        {
            try
            {
                return await this.GetQueryable<TEntity>(filter, orderBy, includeProperties, null, null, withTracking).FirstOrDefaultAsync();
            }
            catch (AggregateException ae)
            {
                var newex = ae.Flatten();
                StringBuilder sb = new StringBuilder();
                newex.InnerExceptions.ToList().ForEach(t => sb.Append($"::{t} ##"));
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        $"an Error occurred while getting the data in GetFirstAsync; {sb}",
                        ae));
                throw;
            }

            catch (Exception e)
            {
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        "an Error occurred in GetFirstAsync;",
                        e));
                throw;
            }
        }

        public virtual TEntity GetById<TEntity>(object id)
            where TEntity : class
        {
            try
            {
                return this.Context.Set<TEntity>().Find(id);
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while getting the data in GetById;", e));
                throw;
            }
        }

        public virtual Task<TEntity> GetByIdAsync<TEntity>(object id)
            where TEntity : class
        {
            try
            {
                return this.Context.Set<TEntity>().FindAsync(id);
            }
            catch (AggregateException ae)
            {
                var newex = ae.Flatten();
                StringBuilder sb = new StringBuilder();
                newex.InnerExceptions.ToList().ForEach(t => sb.Append($"::{t} ##"));
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        $"an Error occurred while getting the data in GetByIdAsync; {sb}",
                        ae));
                throw;
            }

            catch (Exception e)
            {
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        "an Error occurred in GetByIdAsync;",
                        e));
                throw;
            }
        }

        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            try
            {
                return this.GetQueryable<TEntity>(filter).Count();
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while getting the data in Getcount;", e));
                throw;
            }
        }

        public virtual Task<int> GetCountAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            try
            {
                return this.GetQueryable<TEntity>(filter).CountAsync();
            }
            catch (AggregateException ae)
            {
                var newex = ae.Flatten();
                StringBuilder sb = new StringBuilder();
                newex.InnerExceptions.ToList().ForEach(t => sb.Append($"::{t} ##"));
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        $"an Error occurred while getting the data in GetcountAsync; {sb}",
                        ae));
                throw;
            }

            catch (Exception e)
            {
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        "an Error occurred in GetCountAsync;",
                        e));
                throw;
            }
        }

        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            try
            {
                return this.GetQueryable<TEntity>(filter).Any();
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while getting the data in GetExists;", e));
                throw;
            }
        }

        public virtual Task<bool> GetExistsAsync<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            try
            {
                return this.GetQueryable<TEntity>(filter).AnyAsync();
            }
            catch (AggregateException ae)
            {
                var newex = ae.Flatten();
                StringBuilder sb = new StringBuilder();
                newex.InnerExceptions.ToList().ForEach(t => sb.Append($"::{t} ##"));
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        $"an Error occurred while getting the data in GetExistsAsync; {sb}",
                        ae));
                throw;
            }

            catch (Exception e)
            {
                Logger.Log(
                    new LogEntry(
                        LoggingEventType.Error,
                        "an Error occurred in GetExistsAsync;",
                        e));
                throw;
            }

        }
    }
}

using Microsoft.EntityFrameworkCore;
using OnlineCourse.Common.Logger.Model;
using OnlineCourse.Data.Repo.Entity.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCourse.Data.Repo
{
    public class EntityFrameworkRepository<TContext> : EntityFrameworkReadOnlyRepository<TContext>, IRepository
            where TContext : IDataSession
    {
        public EntityFrameworkRepository(TContext context)
            : base(context)
        {
        }

        public virtual void Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class
        {
            try
            {
                this.Context.Set<TEntity>().Add(entity);
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while creating the data;", e));
                throw;
            }
        }

        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            try
            {
                var dbSet = this.Context.Set<TEntity>();
                if (this.Context.Entry(entity).State == EntityState.Detached)
                {
                    dbSet.Attach(entity);
                }

                dbSet.Remove(entity);
            }
            catch (Exception e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "an Error occurred while deleting the data;", e));
                throw;
            }
        }

        public virtual int Save()
        {
            int resultValue = -1;

            try
            {
                SetDefaults();
                resultValue = this.Context.SaveChanges();
                Logger.Log(new LogEntry(LoggingEventType.Information, $"saved {resultValue} Entries into database..."));
            }
            catch (InvalidOperationException e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type InvalidOperationException", e));
                throw;
            }
            catch (NotSupportedException e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type NotSupportedException", e));
                throw;
            }
            //catch (System.Data.Entity.Validation.DbEntityValidationException e)
            //{
            //    string errMsg = string.Empty;
            //    foreach (var dbEntityValidationResult in e.EntityValidationErrors)
            //    {
            //        foreach (DbValidationError dbValidationError in dbEntityValidationResult.ValidationErrors)
            //        {
            //            errMsg += dbEntityValidationResult.Entry.Entity.GetType().ToString() + " " + dbValidationError.ErrorMessage;
            //        }
            //    }

            //    Logger.Log(new LogEntry(LoggingEventType.Error, $"Error on SaveChanges type DbEntityValidationException: {errMsg}", e));
            //    throw;
            //}
            catch (DbUpdateConcurrencyException e) // when (e.Entries.Any())
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type DbUpdateConcurrencyException", e));
                throw;
            }
            catch (DbUpdateException e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type DbUpdateException", e));
                throw;
            }

            return resultValue;
        }

        public virtual Task SaveAsync()
        {
            try
            {
                SetDefaults();
                return this.Context.SaveChangesAsync();
            }
            catch (InvalidOperationException e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type InvalidOperationException", e));
                throw;
            }
            catch (NotSupportedException e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type NotSupportedException", e));
                throw;
            }
            //catch (System.Data.Entity.Validation.DbEntityValidationException e)
            //{
            //    string errMsg = string.Empty;
            //    foreach (var dbEntityValidationResult in e.EntityValidationErrors)
            //    {
            //        foreach (DbValidationError dbValidationError in dbEntityValidationResult.ValidationErrors)
            //        {
            //            errMsg += dbEntityValidationResult.Entry.Entity.GetType().ToString() + " " + dbValidationError.ErrorMessage;
            //        }
            //    }

            //    Logger.Log(new LogEntry(LoggingEventType.Error, $"Error on SaveChanges type DbEntityValidationException: {errMsg}", e));
            //    throw;
            //}
            catch (DbUpdateConcurrencyException e) // when (e.Entries.Any())
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type DbUpdateConcurrencyException", e));
                throw;
            }
            catch (DbUpdateException e)
            {
                Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type DbUpdateException", e));
                throw;
            }
        }

        public virtual int SaveWithTransaction()
        {
            int savedRecs = 0;
            using (var trans = this.Context.BeginTransaction())
            {
                try
                {
                    SetDefaults();
                    savedRecs = this.Context.SaveChanges();
                    trans.Commit();
                    Logger.Log(
                        new LogEntry(
                            LoggingEventType.Information,
                            $"saved {savedRecs} Entries into database with commited transaction..."));
                }
                catch (InvalidOperationException e)
                {
                    Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type InvalidOperationException", e));
                    trans.Rollback();
                    throw;
                }
                catch (NotSupportedException e)
                {
                    Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type NotSupportedException", e));
                    trans.Rollback();
                    throw;
                }
                //catch (DbEntityValidationException e)
                //{
                //    string errMsg = string.Empty;
                //    foreach (var dbEntityValidationResult in e.EntityValidationErrors)
                //    {
                //        foreach (DbValidationError dbValidationError in dbEntityValidationResult.ValidationErrors)
                //        {
                //            errMsg += dbEntityValidationResult.Entry.Entity.GetType().ToString() + " " + dbValidationError.ErrorMessage;
                //        }
                //    }

                //    Logger.Log(new LogEntry(LoggingEventType.Error, $"Error on SaveChanges type DbEntityValidationException: {errMsg}", e));
                //    trans.Rollback();
                //    throw;
                //}
                catch (DbUpdateConcurrencyException e) // when (e.Entries.Any())
                {
                    Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type DbUpdateConcurrencyException", e));
                    trans.Rollback();
                    throw;
                }
                catch (DbUpdateException e)
                {
                    Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type DbUpdateException", e));
                    trans.Rollback();
                    throw;
                }
                catch (Exception e)
                {
                    Logger.Log(
                        new LogEntry(
                            LoggingEventType.Error,
                            "an Error occurred while saving the data; rolling back transaction...",
                            e));
                    trans.Rollback();
                    throw;
                }
            }

            return savedRecs;
        }

        public virtual async Task SaveWithTransactionAsync()
        {
            using (var transaction = this.Context.BeginTransaction())
            {
                try
                {
                    SetDefaults();
                    await this.Context.SaveChangesAsync();
                    transaction.Commit();
                }
                catch (InvalidOperationException e)
                {
                    Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type InvalidOperationException", e));
                    transaction.Rollback();
                    throw;
                }
                catch (NotSupportedException e)
                {
                    Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type NotSupportedException", e));
                    transaction.Rollback();
                    throw;
                }
                //catch (System.Data.Entity.Validation.DbEntityValidationException e)
                //{
                //    string errMsg = string.Empty;
                //    foreach (var dbEntityValidationResult in e.EntityValidationErrors)
                //    {
                //        foreach (DbValidationError dbValidationError in dbEntityValidationResult.ValidationErrors)
                //        {
                //            errMsg += dbEntityValidationResult.Entry.Entity.GetType().ToString() + " " + dbValidationError.ErrorMessage;
                //        }
                //    }

                //    Logger.Log(new LogEntry(LoggingEventType.Error, $"Error on SaveChanges type DbEntityValidationException: {errMsg}", e));
                //    transaction.Rollback();
                //    throw;
                //}
                catch (DbUpdateConcurrencyException e) // when (e.Entries.Any())
                {
                    Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type DbUpdateConcurrencyException", e));
                    transaction.Rollback();
                    throw;
                }
                catch (DbUpdateException e)
                {
                    Logger.Log(new LogEntry(LoggingEventType.Error, "Error on SaveChanges type DbUpdateException", e));
                    transaction.Rollback();
                    throw;
                }
                catch (Exception e)
                {
                    Logger.Log(
                        new LogEntry(
                            LoggingEventType.Error,
                            "an Error occurred while saving the data; rolling back transaction...",
                            e));
                    transaction.Rollback();
                    throw;
                }
            }
        }
        private void SetDefaults()
        {
            var now = DateTime.Now;
            var usr = Environment.UserName;

            foreach (var entry in this.Context.ChangeTracker.Entries<IBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedDate = now;
                        entry.Entity.CreatedBy = usr;
                        entry.Entity.ModifiedDate = now;
                        entry.Entity.ModifiedBy = usr;

                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedDate = now;
                        entry.Entity.ModifiedBy = usr;
                        entry.Property(e => e.CreatedDate).IsModified = false;
                        entry.Property(e => e.CreatedBy).IsModified = false;
                        break;

                    case EntityState.Deleted:
                        Logger.Log(new LogEntry(LoggingEventType.Debug, "SetDefaults for case Deleted"));
                        break;

                    default: break;
                }
            }
        }
    }
}

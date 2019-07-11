using Agilisium.TalentManager.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Abstract
{
    public abstract class RepositoryBase<T> : IDisposable where T : EntityBase
    {
        // for Postgres DB Access
        private PostgresModel.TalentManagerDataContext dataContext;
        protected PostgresModel.TalentManagerDataContext DataContext => dataContext ?? (dataContext = new PostgresModel.TalentManagerDataContext());

        // for SQL Server DB Access
        //private TalentManagerDataContext dataContext;
        //protected TalentManagerDataContext DataContext => dataContext ?? (dataContext = new TalentManagerDataContext());

        public DbSet<T> Entities => DataContext.Set<T>();


        public void Dispose()
        {
            if (dataContext != null)
            {
                dataContext.Dispose();
            }
        }

        public virtual int TotalRecordsCount()
        {
            return Entities.Count(e => e.IsDeleted == false);
        }

        public virtual bool CanBeDeleted(int id)
        {
            return true;
        }
    }
}

﻿using Agilisium.TalentManager.Model;
using System;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Abstract
{
    public abstract class RepositoryBase<T> : IDisposable where T : EntityBase
    {
        private PostgresModel.TalentManagerDataContext dataContext;

        public DbSet<T> Entities => DataContext.Set<T>();

        protected PostgresModel.TalentManagerDataContext DataContext => dataContext ?? (dataContext = new PostgresModel.TalentManagerDataContext());

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

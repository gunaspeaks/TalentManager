using System;
using System.Collections.Generic;

namespace Agilisium.TalentManager.Repository.Abstract
{
    public interface IRepository<T> : IDisposable where T : class
    {
        bool Exists(string itemName);

        bool Exists(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        T GetByID(int id);

        IEnumerable<T> GetAll(int pageSize = -1, int pageNo = -1);

        int TotalRecordsCount();

        bool CanBeDeleted(int id);
    }
}

using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class DevRequestRepository : RepositoryBase<DevelopmentRequest>, IDevRequestRepository
    {
        public void Add(DevelopmentRequestDto entity)
        {
            DevelopmentRequest request = CreateBusinessEntity(entity, true);
            Entities.Add(request);
            DataContext.Entry(request).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(DevelopmentRequestDto entity)
        {
            DevelopmentRequest buzEntity = Entities.FirstOrDefault(e => e.RequestID == entity.RequestID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.RequestID == id && c.IsDeleted == false);
        }

        private IQueryable<DevelopmentRequestDto> GetAllEntities()
        {
            return from r in Entities
                   join p in DataContext.DropDownSubCategories on r.PriorityID equals p.SubCategoryID into pe
                   from pd in pe.DefaultIfEmpty()
                   join s in DataContext.DropDownSubCategories on r.RequestStatusID equals s.SubCategoryID into se
                   from sd in se.DefaultIfEmpty()
                   join t in DataContext.DropDownSubCategories on r.RequestTypeID equals t.SubCategoryID into te
                   from td in te.DefaultIfEmpty()
                   orderby r.RequestedOn descending
                   where r.IsDeleted == false
                   select new DevelopmentRequestDto
                   {
                       Priority = pd.SubCategoryName,
                       PriorityID = r.PriorityID,
                       Remarks = r.Remarks,
                       RequestedOn = r.RequestedOn,
                       RequestID = r.RequestID,
                       RequestedBy = r.RequestedBy,
                       RequestStatus = sd.SubCategoryName,
                       RequestStatusID = r.RequestStatusID,
                       RequestTitle = r.RequestTitle,
                       RequestType = td.SubCategoryName,
                       RequestTypeID = r.RequestTypeID,
                   };
        }

        public IEnumerable<DevelopmentRequestDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<DevelopmentRequestDto> requests = GetAllEntities();

            if (pageSize <= 0 || pageNo < 1)
            {
                return requests;
            }

            return requests.Skip((pageNo - 1) * pageSize).Take(pageSize);

        }

        public IEnumerable<DevelopmentRequestDto> GetAllByOwner(string ownerName)
        {
            IQueryable<DevelopmentRequestDto> requests = GetAllEntities();

            return requests.Where(e => e.RequestedBy == ownerName);
        }

        public DevelopmentRequestDto GetByID(int id)
        {
            return (from r in Entities
                    where r.RequestID == id
                    select new DevelopmentRequestDto
                    {
                        PriorityID = r.PriorityID,
                        Remarks = r.Remarks,
                        RequestedOn = r.RequestedOn,
                        RequestID = r.RequestID,
                        RequestedBy = r.RequestedBy,
                        RequestStatusID = r.RequestStatusID,
                        RequestTitle = r.RequestTitle,
                        RequestTypeID = r.RequestTypeID,
                    }).FirstOrDefault();
        }

        public void Update(DevelopmentRequestDto entity)
        {
            DevelopmentRequest buzEntity = Entities.FirstOrDefault(e => e.RequestID == entity.RequestID);
            MigrateEntity(entity, buzEntity);
            entity.RequestedBy = buzEntity.RequestedBy;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private DevelopmentRequest CreateBusinessEntity(DevelopmentRequestDto categoryDto, bool isNewEntity = false)
        {
            DevelopmentRequest request = new DevelopmentRequest
            {
                PriorityID = categoryDto.PriorityID,
                RequestedBy = categoryDto.RequestedBy,
                RequestedOn = categoryDto.RequestedOn,
                Remarks = categoryDto.Remarks,
                RequestID = categoryDto.RequestID,
                RequestStatusID = categoryDto.RequestStatusID,
                RequestTitle = categoryDto.RequestTitle,
                RequestTypeID = categoryDto.RequestTypeID,
            };

            request.UpdateTimeStamp(categoryDto.LoggedInUserName, true);

            return request;
        }

        private void MigrateEntity(DevelopmentRequestDto sourceEntity, DevelopmentRequest targetEntity)
        {
            targetEntity.PriorityID = sourceEntity.PriorityID;
            targetEntity.RequestedBy = sourceEntity.RequestedBy;
            targetEntity.RequestedOn = sourceEntity.RequestedOn;
            targetEntity.Remarks = sourceEntity.Remarks;
            targetEntity.RequestID = sourceEntity.RequestID;
            targetEntity.RequestStatusID = sourceEntity.RequestStatusID;
            targetEntity.RequestTitle = sourceEntity.RequestTitle;
            targetEntity.RequestTypeID = sourceEntity.RequestTypeID;
        }

        public bool Exists(string itemName)
        {
            return Entities.Any(c => c.RequestTitle == itemName && c.IsDeleted == false);
        }
    }

    public interface IDevRequestRepository : IRepository<DevelopmentRequestDto>
    {
        IEnumerable<DevelopmentRequestDto> GetAllByOwner(string ownerName);
    }
}

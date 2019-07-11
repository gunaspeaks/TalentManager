using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Repository.Repositories
{
    public class NotificationsTrackerRepository : RepositoryBase<NotificationsTracker>, INotificationsTrackerRepository
    {
        public void Add(NotificationsTrackerDto entity)
        {
            NotificationsTracker tracker = CreateBusinessEntity(entity, true);
            Entities.Add(tracker);
            DataContext.Entry(tracker).State = EntityState.Added;
            DataContext.SaveChanges();
        }

        public void Delete(NotificationsTrackerDto entity)
        {
            NotificationsTracker buzEntity = Entities.FirstOrDefault(e => e.TrackerID == entity.TrackerID);
            buzEntity.IsDeleted = true;
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        public bool Exists(int id)
        {
            return Entities.Any(c => c.TrackerID == id && c.IsDeleted == false);
        }

        public bool Exists(string itemName)
        {
            return false;
        }

        public IEnumerable<NotificationsTrackerDto> GetAll(int pageSize = -1, int pageNo = -1)
        {
            IQueryable<NotificationsTrackerDto> trackers = from p in Entities
                                                           orderby p.TrackerID
                                                           where p.IsDeleted == false
                                                           select new NotificationsTrackerDto
                                                           {
                                                               AllocationEntryID = p.AllocationEntryID,
                                                               FirstAlertSentOn = p.FirstAlertSentOn,
                                                               SecondAlertSentOn = p.SecondAlertSentOn,
                                                               ThirdAlertSentOn = p.ThirdAlertSentOn,
                                                               TrackerID = p.TrackerID
                                                           };

            if (pageSize <= 0 || pageNo < 1)
            {
                return trackers;
            }

            return trackers.Skip((pageNo - 1) * pageSize).Take(pageSize);

        }

        public NotificationsTrackerDto GetByID(int id)
        {
            return (from p in Entities
                    where p.TrackerID == id && p.IsDeleted == false
                    select new NotificationsTrackerDto
                    {
                        AllocationEntryID = p.AllocationEntryID,
                        FirstAlertSentOn = p.FirstAlertSentOn,
                        SecondAlertSentOn = p.SecondAlertSentOn,
                        ThirdAlertSentOn = p.ThirdAlertSentOn,
                        TrackerID = p.TrackerID,
                    }).FirstOrDefault();
        }

        public void Update(NotificationsTrackerDto entity)
        {
            NotificationsTracker buzEntity = Entities.FirstOrDefault(e => e.TrackerID == entity.TrackerID);
            MigrateEntity(entity, buzEntity);
            buzEntity.UpdateTimeStamp(entity.LoggedInUserName);
            Entities.Add(buzEntity);
            DataContext.Entry(buzEntity).State = EntityState.Modified;
            DataContext.SaveChanges();
        }

        private NotificationsTracker CreateBusinessEntity(NotificationsTrackerDto trackerDto, bool isNewEntity = false)
        {
            NotificationsTracker tracker = new NotificationsTracker
            {
                AllocationEntryID = trackerDto.AllocationEntryID,
                FirstAlertSentOn = trackerDto.FirstAlertSentOn,
                SecondAlertSentOn = trackerDto.SecondAlertSentOn,
                ThirdAlertSentOn = trackerDto.ThirdAlertSentOn,
                TrackerID = trackerDto.TrackerID
            };

            tracker.UpdateTimeStamp(trackerDto.LoggedInUserName, true);

            return tracker;
        }

        private void MigrateEntity(NotificationsTrackerDto sourceEntity, NotificationsTracker targetEntity)
        {
            targetEntity.AllocationEntryID = sourceEntity.AllocationEntryID;
            targetEntity.FirstAlertSentOn = sourceEntity.FirstAlertSentOn;
            targetEntity.SecondAlertSentOn = sourceEntity.SecondAlertSentOn;
            targetEntity.ThirdAlertSentOn = sourceEntity.ThirdAlertSentOn;
            targetEntity.TrackerID = sourceEntity.TrackerID;
            targetEntity.UpdateTimeStamp(sourceEntity.LoggedInUserName);
        }
    }

    public interface INotificationsTrackerRepository : IRepository<NotificationsTrackerDto>
    {
    }
}

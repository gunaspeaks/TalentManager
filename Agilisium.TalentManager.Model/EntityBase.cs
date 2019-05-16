using System;

namespace Agilisium.TalentManager.Model
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            IsDeleted = false;
        }

        public string CreatedBy { get; set; }

        public string UpdatedBy { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public bool IsDeleted { get; set; }

        public virtual void UpdateTimeStamp(string userName, bool isNewEntity = false)
        {
            if (isNewEntity)
            {
                CreatedBy = userName;
                CreatedOn = DateTime.Now;
            }
            else
            {
                UpdatedBy = userName;
                UpdatedOn = DateTime.Now;
            }
        }
    }
}

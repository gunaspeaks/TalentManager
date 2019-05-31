using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agilisium.TalentManager.Service.Abstract
{
    public interface IDevRequestService
    {
        void Add(DevelopmentRequestDto entity);

        void Delete(DevelopmentRequestDto entity);

        bool Exists(int id);

        List<DevelopmentRequestDto> GetAll(int pageSize = -1, int pageNo = -1);

        List<DevelopmentRequestDto> GetAllByOwner(string ownerName);

        DevelopmentRequestDto GetByID(int id);

        void Update(DevelopmentRequestDto entity);

        bool Exists(string itemName);

        int TotalRecordsCount();
    }
}

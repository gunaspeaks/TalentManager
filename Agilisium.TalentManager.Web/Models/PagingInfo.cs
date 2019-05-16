using System;

namespace Agilisium.TalentManager.Web.Models
{
    public class PagingInfo
    {
        public int TotalRecordsCount { get; set; }

        public int RecordsPerPage { get; set; }

        public int CurentPageNo { get; set; }

        public int TotalPageCount => RecordsPerPage == 0 ? 0 : (int)Math.Ceiling(((decimal)TotalRecordsCount / (decimal)RecordsPerPage));
    }
}
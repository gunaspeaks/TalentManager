﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Models
{
    public class PagingViewModel
    {
        public int TotalRecords { get; set; }

        public int RecordsPerPage { get; set; }
    }
}
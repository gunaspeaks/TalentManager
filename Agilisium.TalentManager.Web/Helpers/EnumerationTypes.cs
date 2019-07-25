using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Agilisium.TalentManager.Web.Helpers
{
    public enum CategoryType
    {
        BusinessUnit = 1,
        UtilizationCode,
        ProjectType,
        EmploymentType,
        SpecializedPartner,
        ContractPeriod,
        ServiceRequestType,
        Country,
        DevelopmentRequestType,
        DevelopmentPriority,
        DevelopmentRequestStatus,
        VisaCategory,
    }

    public enum PracticeType
    {
        BusinessDelevopment = 1,
        Operations,
        Delivery
    }

    public enum EmployementType
    {
        Permanent = 1,
        Internship,
        Contract
    }
}
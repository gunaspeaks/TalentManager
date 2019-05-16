using Agilisium.TalentManager.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Agilisium.TalentManager.Model
{
    public class TalentManagerSeedData : DropCreateDatabaseIfModelChanges<TalentManagerDataContext>
    {
        protected override void Seed(TalentManagerDataContext context)
        {
            GetSystemSettings().ForEach(s => context.SystemSettings.Add(s));

            GetCategories().ForEach(c => context.DropDownCategories.Add(c));
            context.SaveChanges();

            GetSubCategories(context).ForEach(c => context.DropDownSubCategories.Add(c));
            GetSubCategories_New01(context).ForEach(c => context.DropDownSubCategories.Add(c));
            context.SaveChanges();

            GetPractices(context).ForEach(p => context.Practices.Add(p));
            context.SaveChanges();

            GetEmployeeIDTrackers(context).ForEach(e => context.EmployeeIDTrackers.Add(e));

            GetSubPractices(context).ForEach(e => context.SubPractices.Add(e));
            context.SaveChanges();
        }

        private static List<SystemSetting> GetSystemSettings()
        {
            return new List<SystemSetting>
            {
                new SystemSetting
                {
                    SettingName = "Email Proxy Server",
                    SettingValue = "smtp.office365.com"
                },
                new SystemSetting
                {
                    SettingName = "Email Proxy Port",
                    SettingValue = "587"
                },
                new SystemSetting
                {
                    SettingName = "Contractor Request Email Owner",
                    SettingValue = "Gunasekaran.R@agilisium.com"
                },
                new SystemSetting
                {
                    SettingName = "Contractor Request Email BCC Email IDs",
                    SettingValue = "Sriram.Balakrishnan@agilisium.com; satish.srinivasan@agilisium.com"
                },
                new SystemSetting
                {
                    SettingName = "Owner's Outlook EMAIL Password",
                    SettingValue = "Saba@dec2018"
                },
            };
        }

        private static List<DropDownCategory> GetCategories()
        {
            return new List<DropDownCategory>() {
                new DropDownCategory
                {
                     CategoryName = "Business Unit",
                     Description = "Business Unit",
                     ShortName = "BU",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Utilization Type",
                     Description = "Resource utilization types",
                     ShortName = "UT",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Project Type",
                     Description = "Project Types",
                     ShortName = "PT",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Employment Type",
                     Description = "Employment Types",
                     ShortName = "ET",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Specialized Partner",
                     Description = "Specialized Partner",
                     ShortName = "SP",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Contract Period",
                     Description = "Contract Period",
                     ShortName = "CP",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Service Request Status",
                     Description = "Service Request Status",
                     ShortName = "SR",
                     IsReserved = true
                },
                new DropDownCategory
                {
                     CategoryName = "Country",
                     Description = "Country",
                     ShortName = "CN",
                     IsReserved = true
                }
            };
        }

        private static List<DropDownSubCategory> GetSubCategories_New01(TalentManagerDataContext context)
        {
            //int etCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Employment Type").CategoryID;
            int cnCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Country").CategoryID;

            return new List<DropDownSubCategory>
            {
                #region Employment Type

                //new DropDownSubCategory
                //{
                //    SubCategoryName = "Yet to Join",
                //    ShortName = "YTJ",
                //    CategoryID = etCID,
                //    Description = "Yet to Join",
                //    IsReserved = true
                //},
 
                #endregion

                #region Country

                new DropDownSubCategory
                {
                    SubCategoryName = "United States",
                    ShortName = "US",
                    CategoryID = cnCID,
                    Description = "United States",
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "United Kingdom",
                    ShortName = "UK",
                    CategoryID = cnCID,
                    Description = "United Kingdom",
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "France",
                    ShortName = "FR",
                    CategoryID = cnCID,
                    Description = "France",
                }          ,
                new DropDownSubCategory
                {
                    SubCategoryName = "India",
                    ShortName = "IN",
                    CategoryID = cnCID,
                    Description = "India",
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "China",
                    ShortName = "CH",
                    CategoryID = cnCID,
                    Description = "China",
                }

                #endregion
            };
        }

        private static List<DropDownSubCategory> GetSubCategories(TalentManagerDataContext context)
        {
            int buCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Business Unit").CategoryID;
            int utCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Utilization Type").CategoryID;
            int ptCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Project Type").CategoryID;
            int etCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Employment Type").CategoryID;
            int spCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Specialized Partner").CategoryID;
            int cpCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Contract Period").CategoryID;
            int srCID = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Service Request Status").CategoryID;

            return new List<DropDownSubCategory>
            {
                #region BU Sub Categories

                new DropDownSubCategory
                {
                    SubCategoryName = "Business Development",
                    ShortName = "BD",
                    CategoryID = buCID,
                    Description = "Business Development",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Business Operations",
                    ShortName = "BO",
                    CategoryID = buCID,
                    Description = "Business Operations",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Delivery",
                    ShortName = "DL",
                    CategoryID = buCID,
                    Description = "Project Delivery",
                    IsReserved = true
                },

                #endregion

                #region Utilization Codes

                new DropDownSubCategory
                {
                    SubCategoryName = "Billable",
                    ShortName = "BL",
                    CategoryID = utCID,
                    Description = "Billable",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Management Overheads",
                    ShortName = "MNO",
                    CategoryID = utCID,
                    Description = "Management Overheads",
                    IsReserved = true,
                    IsDeleted = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Non-Committed Buffer",
                    ShortName = "BCH",
                    CategoryID = utCID,
                    Description = "Bench",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Committed Buffer",
                    ShortName = "CMB",
                    CategoryID = utCID,
                    Description = "Committed buffer",
                    IsReserved = true
                },

                #endregion

                #region Project Type

                new DropDownSubCategory
                {
                    SubCategoryName = "Client Billable Project",
                    ShortName = "CBL",
                    CategoryID = ptCID,
                    Description = "Client Billable Project",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Internal Project",
                    ShortName = "IPR",
                    CategoryID = ptCID,
                    Description = "Internal Project",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Lab",
                    ShortName = "Lab",
                    CategoryID = ptCID,
                    Description = "Lab",
                    IsReserved = true
                },

                #endregion

                #region Employment Type

                new DropDownSubCategory
                {
                    SubCategoryName = "Permanent",
                    ShortName = "PMT",
                    CategoryID = etCID,
                    Description = "Permanent employee",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Internship",
                    ShortName = "INT",
                    CategoryID = etCID,
                    Description = "Internship employee",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Contract",
                    ShortName = "CNT",
                    CategoryID = etCID,
                    Description = "Contract employee",
                    IsReserved = true
                },

                #endregion

                #region Specialized Partner

                new DropDownSubCategory
                {
                    SubCategoryName = "Microsoft",
                    ShortName = "MFT",
                    CategoryID = spCID,
                    Description = "Microsoft",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Google",
                    ShortName = "GGL",
                    CategoryID = spCID,
                    Description = "Google",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "AWS",
                    ShortName = "AWS",
                    CategoryID = spCID,
                    Description = "AWS",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Databricks",
                    ShortName = "DBK",
                    CategoryID = spCID,
                    Description = "Databricks",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Snaplogic",
                    ShortName = "SNP",
                    CategoryID = spCID,
                    Description = "Snaplogic",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Others",
                    ShortName = "OTH",
                    CategoryID = spCID,
                    Description = "Others",
                    IsReserved = true
                },

                #endregion

                #region Contract Period

                new DropDownSubCategory
                {
                    SubCategoryName = "1 Month",
                    ShortName = "OMN",
                    CategoryID = cpCID,
                    Description = "1 Month",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "2 Months",
                    ShortName = "TMN",
                    CategoryID = cpCID,
                    Description = "2 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "3 Months",
                    ShortName = "THM",
                    CategoryID = cpCID,
                    Description = "3 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "4 Months",
                    ShortName = "FMN",
                    CategoryID = cpCID,
                    Description = "4 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "5 Months",
                    ShortName = "FVM",
                    CategoryID = cpCID,
                    Description = "5 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "6 Months",
                    ShortName = "SMN",
                    CategoryID = cpCID,
                    Description = "6 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "7 Months",
                    ShortName = "SVM",
                    CategoryID = cpCID,
                    Description = "7 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "8 Months",
                    ShortName = "EVM",
                    CategoryID = cpCID,
                    Description = "8 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "9 Months",
                    ShortName = "NVM",
                    CategoryID = cpCID,
                    Description = "9 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "10 Months",
                    ShortName = "TNM",
                    CategoryID = cpCID,
                    Description = "10 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "11 Months",
                    ShortName = "ELM",
                    CategoryID = cpCID,
                    Description = "11 Months",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "One Year",
                    ShortName = "OYM",
                    CategoryID = cpCID,
                    Description = "One Year",
                    IsReserved = true
                },

                #endregion

                #region Service Request Status

                new DropDownSubCategory
                {
                    SubCategoryName = "Email to be Sent",
                    ShortName = "ES",
                    CategoryID = srCID,
                    Description = "Email to be Sent",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Email Sent",
                    ShortName = "RS",
                    CategoryID = srCID,
                    Description = "Request Sent",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Accepted By Vendor",
                    ShortName = "AV",
                    CategoryID = srCID,
                    Description = "Accepted By Vendor",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Rejected By Vendor",
                    ShortName = "AV",
                    CategoryID = srCID,
                    Description = "Rejected By Vendor",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Waiting for Vendor",
                    ShortName = "AV",
                    CategoryID = srCID,
                    Description = "Waiting for Vendor",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Cancelled",
                    ShortName = "CL",
                    CategoryID = srCID,
                    Description = "Cancelled",
                    IsReserved = true
                },
                new DropDownSubCategory
                {
                    SubCategoryName = "Fulfilled",
                    ShortName = "FF",
                    CategoryID = srCID,
                    Description = "Fulfilled",
                    IsReserved = true
                },

                #endregion
            };
        }

        private static List<Practice> GetPractices(TalentManagerDataContext context)
        {
            int bdID = context.DropDownSubCategories.FirstOrDefault(s => s.SubCategoryName == "Business Development").SubCategoryID;
            int boID = context.DropDownSubCategories.FirstOrDefault(s => s.SubCategoryName == "Business Operations").SubCategoryID;
            int dlID = context.DropDownSubCategories.FirstOrDefault(s => s.SubCategoryName == "Delivery").SubCategoryID;

            return new List<Practice>
            {
                #region Business Development - Practices

                new Practice
                {
                    PracticeName = "Marketing",
                    ShortName = "MKT",
                    BusinessUnitID = bdID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Sales",
                    ShortName = "SLS",
                    BusinessUnitID = bdID,
                    IsReserved = true
                },

                #endregion

                #region Operations - Practices

                new Practice
                {
                    PracticeName = "Senior Leadership",
                    ShortName = "SNL",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Admin",
                    ShortName = "ADM",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Finance",
                    ShortName = "FIN",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "People Practice",
                    ShortName = "PPR",
                    BusinessUnitID = boID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "IT Support",
                    ShortName = "ITS",
                    BusinessUnitID = boID,
                    IsReserved = true
                },

                #endregion

                #region Delivery - Practices

                new Practice
                {
                    PracticeName = "AWS DI",
                    ShortName = "AWSI",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "AWS DA",
                    ShortName = "AWSA",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "CDI",
                    ShortName = "CDI",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Product",
                    ShortName = "PROD",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Google",
                    ShortName = "GOGL",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Non-Core",
                    ShortName = "NONC",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },
                new Practice
                {
                    PracticeName = "Non-Tech",
                    ShortName = "NONT",
                    BusinessUnitID = dlID,
                    IsReserved = true
                },

                #endregion
            };
        }

        private static List<EmployeeIDTracker> GetEmployeeIDTrackers(TalentManagerDataContext context)
        {
            int? empTypeCategory = context.DropDownCategories.FirstOrDefault(c => c.CategoryName == "Employment Type")?.CategoryID;
            List<DropDownSubCategory> empTypes = context.DropDownSubCategories.Where(c => c.CategoryID == empTypeCategory).ToList();

            int? contracEmpTypeID = empTypes.FirstOrDefault(c => c.SubCategoryName == "Contract")?.SubCategoryID;
            int? permanentEmpTypeID = empTypes.FirstOrDefault(c => c.SubCategoryName == "Permanent")?.SubCategoryID;
            int? internshipEmpTypeID = empTypes.FirstOrDefault(c => c.SubCategoryName == "Internship")?.SubCategoryID;
            //int? ytjEmpTypeID = empTypes.FirstOrDefault(c => c.SubCategoryName == "Yet to Join")?.SubCategoryID;

            return new List<EmployeeIDTracker>
            {
                new EmployeeIDTracker
                {
                    EmploymentTypeID = permanentEmpTypeID.Value,
                    IDPrefix = string.Empty,
                    RunningID = 10001
                },
                new EmployeeIDTracker
                {
                    EmploymentTypeID = internshipEmpTypeID.Value,
                    IDPrefix = "CI",
                    RunningID = 1
                },
                new EmployeeIDTracker
                {
                    EmploymentTypeID = contracEmpTypeID.Value,
                    IDPrefix = "CE",
                    RunningID = 1
                },
                //new EmployeeIDTracker
                //{
                //    EmploymentTypeID = ytjEmpTypeID.Value,
                //    IDPrefix = "YTJ",
                //    RunningID = 1
                //}
            };
        }

        private static List<SubPractice> GetSubPractices(TalentManagerDataContext context)
        {
            int awsDAPID = context.Practices.FirstOrDefault(p => p.PracticeName == "AWS DA").PracticeID;
            int awsDIPID = context.Practices.FirstOrDefault(p => p.PracticeName == "AWS DI").PracticeID;
            int cdiPID = context.Practices.FirstOrDefault(p => p.PracticeName == "CDI").PracticeID;
            int gglPID = context.Practices.FirstOrDefault(p => p.PracticeName == "Google").PracticeID;
            int nonPID = context.Practices.FirstOrDefault(p => p.PracticeName == "Non-Core").PracticeID;
            int prdPID = context.Practices.FirstOrDefault(p => p.PracticeName == "Product").PracticeID;

            return new List<SubPractice>
            {
                #region AWS DA - Sup Practices

                new SubPractice
                {
                    PracticeID = awsDAPID,
                    SubPracticeName = "Machine Learning",
                    ShortName = "ML",
                },
                new SubPractice
                {
                    PracticeID = awsDAPID,
                    SubPracticeName = "Project Management",
                    ShortName = "ML",
                },
                new SubPractice
                {
                    PracticeID = awsDAPID,
                    SubPracticeName = "Quick Sight",
                    ShortName = "QS",
                },

                #endregion

                #region AWS DI - Sup Practices

                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = ".NET",
                    ShortName = "NET",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "Analytics",
                    ShortName = "ANL",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "Business Objects",
                    ShortName = "BOB",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "Database",
                    ShortName = "DB",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "Database Administrator",
                    ShortName = "DBA",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "Developer",
                    ShortName = "DEV",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "DevOps",
                    ShortName = "DOP",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "Java",
                    ShortName = "JVA",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "Machine Learning",
                    ShortName = "ML",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "Redshift",
                    ShortName = "RDS",
                },
                new SubPractice
                {
                    PracticeID = awsDIPID,
                    SubPracticeName = "Project Management",
                    ShortName = "PM",
                },

                #endregion

                #region CDI - Sup Practices

                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Databricks",
                    ShortName = "DBK",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "SnapLogic",
                    ShortName = "SNP",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Informatica",
                    ShortName = "INF",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Big Query",
                    ShortName = "BQ",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Tableau",
                    ShortName = "TAB",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "SQL",
                    ShortName = "SQL",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Project Management",
                    ShortName = "PM",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Manual",
                    ShortName = "MAN",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Manual Testing",
                    ShortName = "MAT",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Developer",
                    ShortName = "DEV",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Fresher",
                    ShortName = "FRS",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Project Management",
                    ShortName = "PM",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "MuleSoft",
                    ShortName = "MS",
                },
                new SubPractice
                {
                    PracticeID = cdiPID,
                    SubPracticeName = "Presales",
                    ShortName = "PRS",
                },

                #endregion

                #region Google - Sup Practices

                new SubPractice
                {
                    PracticeID = gglPID,
                    SubPracticeName = "Analytics",
                    ShortName = "ANL",
                },
                new SubPractice
                {
                    PracticeID = gglPID,
                    SubPracticeName = "Big Query",
                    ShortName = "BQ",
                },
                new SubPractice
                {
                    PracticeID = gglPID,
                    SubPracticeName = "Google",
                    ShortName = "GOG",
                },
                new SubPractice
                {
                    PracticeID = gglPID,
                    SubPracticeName = "Mean",
                    ShortName = "MEA",
                },
                new SubPractice
                {
                    PracticeID = gglPID,
                    SubPracticeName = "Others",
                    ShortName = "OTH",
                },
                new SubPractice
                {
                    PracticeID = gglPID,
                    SubPracticeName = "Project Management",
                    ShortName = "PM",
                },

                #endregion

                #region Non Core - Sup Practices

                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = ".NET",
                    ShortName = "NET",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "Automation",
                    ShortName = "AUT",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "Business Objects",
                    ShortName = "BOB",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "iOS",
                    ShortName = "IOS",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "L1/L2 Support",
                    ShortName = "L1S",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "L2/L3 Support",
                    ShortName = "L2S",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "Manual",
                    ShortName = "MAN",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "Manual Testing",
                    ShortName = "MAT",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "MSTR",
                    ShortName = "MST",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "PHP",
                    ShortName = "PHP",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "Project Management",
                    ShortName = "PM",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "SharePoint",
                    ShortName = "SHP",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "SQL",
                    ShortName = "SQL",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "Strategic Staffing",
                    ShortName = "STF",
                },
                new SubPractice
                {
                    PracticeID = nonPID,
                    SubPracticeName = "Tableau/Looker",
                    ShortName = "TAL",
                },

                #endregion

                #region Product - Sup Practices

                new SubPractice
                {
                    PracticeID = prdPID,
                    SubPracticeName = "BI Bot",
                    ShortName = "BIB",
                },

                #endregion
            };
        }

        //private static List<Project> GetReservedProjects(List<Practice> practices, TalentManagerDataContext context)
        //{
        //    List<Project> projects = null;

        //    foreach (Practice practice in practices)
        //    {
        //        projects = new List<Project>
        //        {
        //            new Project
        //            {
        //                ProjectName = "Lab " + practice.PracticeName.Replace(" ",""),
        //                BusinessUnitID = practice.BusinessUnitID,
        //                EndDate = new DateTime(DateTime.Now.Year, 12, 31),
        //                StartDate = new DateTime(DateTime.Now.Year, 1, 1),
        //                PracticeID = practice.PracticeID
        //            }
        //        };
        //    }

        //    return projects;
        //}
    }
}

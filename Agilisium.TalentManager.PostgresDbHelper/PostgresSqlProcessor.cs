using Agilisium.TalentManager.Dto;
using System;
using System.Collections.Generic;

namespace Agilisium.TalentManager.PostgresDbHelper
{
    public class PostgresSqlProcessor
    {
        public int GetNonAllocatedResourcesCountFromPostgres(bool forDelivery = true)
        {
            int count = 0;
            Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection(PostgresSqlQueries.CONNECTION_STRING);

            try
            {
                con.Open();
                string qry = PostgresSqlQueries.GET_COUNT_OF_NON_ALLOCATED_EMPLOYEES_FROM_DELIVERY;
                if (!forDelivery)
                {
                    qry = PostgresSqlQueries.GET_COUNT_OF_NON_ALLOCATED_EMPLOYEES_FROM_NON_DELIVERY;
                }

                qry = qry.Replace("__CURRENT_DATE__", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
                Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                object res = cmd.ExecuteScalar();
                int.TryParse(res.ToString(), out count);
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return count;
        }

        public IEnumerable<PracticeHeadCountDto> GetPracticeWiseHeadCountPostgres()
        {
            List<PracticeHeadCountDto> records = new List<PracticeHeadCountDto>();
            Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection(PostgresSqlQueries.CONNECTION_STRING);
            try
            {
                con.Open();
                string qry = PostgresSqlQueries.GET_PRACTICE_WISE_HEAD_COUNT;
                qry = qry.Replace("__CURRENT_DATE__", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
                Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                Npgsql.NpgsqlDataReader res = cmd.ExecuteReader();

                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        records.Add(new PracticeHeadCountDto
                        {
                            HeadCount = res.IsDBNull(2) == false ? (int)res.GetInt64(2) : 0,
                            Practice = res.IsDBNull(1) == false ? res.GetString(1) : "",
                            PracticeID = res.IsDBNull(0) == false ? res.GetInt32(0) : -1,
                        });
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return records;
        }

        public IEnumerable<SubPracticeHeadCountDto> GetSubPracticeWiseHeadCountFromPostgres()
        {
            List<SubPracticeHeadCountDto> records = new List<SubPracticeHeadCountDto>();
            Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection(PostgresSqlQueries.CONNECTION_STRING);
            try
            {
                con.Open();
                string qry = PostgresSqlQueries.GET_SUB_PRACTICE_WISE_HEAD_COUNT;
                qry = qry.Replace("__CURRENT_DATE__", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
                Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                Npgsql.NpgsqlDataReader res = cmd.ExecuteReader();
                int? nullInt = null;
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        records.Add(new SubPracticeHeadCountDto
                        {
                            HeadCount = res.IsDBNull(4) == false ? (int)res.GetInt64(4) : nullInt,
                            Practice = res.IsDBNull(1) == false ? res.GetString(1) : "",
                            PracticeID = res.IsDBNull(0) == false ? (int)res.GetInt64(0) : nullInt,
                            SubPractice = res.IsDBNull(3) == false ? res.GetString(3) : "",
                            SubPracticeID = res.IsDBNull(2) == false ? (int)res.GetInt64(2) : nullInt
                        });
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return records;
        }

        public List<BillabilityWiseAllocationDetailDto> GetAllocationEntriesByAllocationTypeFromPostgres(int allocationType)
        {
            List<BillabilityWiseAllocationDetailDto> allocationDetailDtos = new List<BillabilityWiseAllocationDetailDto>();
            Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection(PostgresDbHelper.PostgresSqlQueries.CONNECTION_STRING);
            try
            {
                con.Open();
                string qry = PostgresSqlQueries.GET_ALLOCATION_DETAIL_FOR_VALID_ALLOCATIONS;
                if (allocationType == -1)
                {
                    qry = PostgresSqlQueries.GET_ALLOCATION_DETAIL_FOR_ALL_NON_ALLOCATED_DELIVERY_BU_RESOURCES;
                }
                else if (allocationType == -2)
                {
                    qry = PostgresSqlQueries.GET_ALLOCATION_DETAIL_FOR_ALL_NON_ALLOCATED_OTHER_BU_RESOURCES;
                }
                qry = qry.Replace("__CURRENT_DATE__", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
                qry = qry.Replace("__ALLOCATION_TYPE_ID__", allocationType.ToString());

                Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                Npgsql.NpgsqlDataReader res = cmd.ExecuteReader();
                DateTime? nullDate = null;
                int? nullInt = null;

                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        allocationDetailDtos.Add(new BillabilityWiseAllocationDetailDto
                        {
                            AccountName = res.IsDBNull(15) == false ? res.GetString(15) : "",
                            AllocationEndDate = res.IsDBNull(12) == false ? res.GetDateTime(12) : nullDate,
                            AllocationStartDate = res.IsDBNull(11) == false ? res.GetDateTime(11) : nullDate,
                            AllocationType = res.IsDBNull(6) == false ? res.GetString(6) : "",
                            AllocationTypeID = res.IsDBNull(5) == false ? res.GetInt32(5) : nullInt,
                            BusinessUnit = res.IsDBNull(8) == false ? res.GetString(8) : "",
                            BusinessUnitID = res.IsDBNull(7) == false ? res.GetInt32(7) : nullInt,
                            Comments = res.IsDBNull(18) == false ? res.GetString(18) : "",
                            EmployeeEntryID = res.IsDBNull(0) == false ? (int?)res.GetInt64(0) : nullInt,
                            EmployeeID = res.IsDBNull(1) == false ? res.GetString(1) : "",
                            EmployeeName = res.IsDBNull(2) == false ? res.GetString(2) : "",
                            POD = res.IsDBNull(10) == false ? res.GetString(10) : "",
                            PracticeID = res.IsDBNull(9) == false ? res.GetInt32(9) : nullInt,
                            PrimarySkills = res.IsDBNull(3) == false ? res.GetString(3) : "",
                            ProjectID = res.IsDBNull(13) == false ? (int?)res.GetInt64(13) : nullInt,
                            ProjectManager = res.IsDBNull(17) == false ? res.GetString(17) : "",
                            ProjectManagerID = res.IsDBNull(16) == false ? res.GetInt32(16) : nullInt,
                            ProjectName = res.IsDBNull(14) == false ? res.GetString(14) : "",
                            ProjectType = "",
                            ProjectTypeID = 0,
                            SecondarySkills = res.IsDBNull(4) == false ? res.GetString(4) : ""
                        });
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return allocationDetailDtos;
        }

        public IEnumerable<ManagerWiseAllocationDto> GetManagerWiseAllocationSummaryFromPostgres()
        {
            List<ManagerWiseAllocationDto> records = new List<ManagerWiseAllocationDto>();
            Npgsql.NpgsqlConnection con = new Npgsql.NpgsqlConnection(PostgresSqlQueries.CONNECTION_STRING);
            try
            {
                con.Open();
                string qry = PostgresSqlQueries.GET_MANAGER_WISE_PROJECTS_SUMMARY.Replace("__CURRENT_DATE__", $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}");
                Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                Npgsql.NpgsqlDataReader res = cmd.ExecuteReader();

                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        records.Add(new ManagerWiseAllocationDto
                        {
                            ManagerName = res.IsDBNull(1) == false ? res.GetString(1) : "",
                            ProjectCount = res.IsDBNull(2) == false ? (int)res.GetInt64(2) : 0,
                            ProjectManagerID = res.IsDBNull(0) == false ? res.GetInt32(0) : -1
                        });
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }

            return records;
        }
    }
}

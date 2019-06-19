using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMigrationTool
{
    public partial class MainForm : Form
    {
        private Agilisium.TalentManager.Model.TalentManagerDataContext sqlDataContext = new Agilisium.TalentManager.Model.TalentManagerDataContext();
        private Agilisium.TalentManager.PostgresModel.TalentManagerDataContext postgressDataContext = new Agilisium.TalentManager.PostgresModel.TalentManagerDataContext();
        private Npgsql.NpgsqlConnection con = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MigrateDataButton_Click(object sender, EventArgs e)
        {
            statusList.Items.Clear();
            try
            {
                //UpdateApplicationStatus("Migrating System Settings, please wait...");
                //int res = await MigrateSystemSettings();
                //statusList.Items.Add($"System Settings  - {res} migrated");
                //statusList.Refresh();
                //statusList.Invalidate();

                //UpdateApplicationStatus("Migrating Categories, please wait...");
                //res = await MigrateDropDownCategories();
                //statusList.Items.Add($"Categories - {res} migrated");
                //statusList.Refresh();
                //statusList.Invalidate();

                //UpdateApplicationStatus("Migrating Sub-Categories, please wait...");
                //res = await MigrateDropDownSubCategories();
                //statusList.Items.Add($"Sub-Categories - {res} migrated");
                //statusList.Refresh();
                //statusList.Invalidate();

                //UpdateApplicationStatus("Migrating PODs, please wait...");
                //res = await MigratePractices();
                //statusList.Items.Add($"PODs - {res} migrated");
                //statusList.Refresh();
                //statusList.Invalidate();

                //UpdateApplicationStatus("Migrating Competencies, please wait...");
                //res = await MigrateSubPractices();
                //statusList.Items.Add($"Competencies - {res} migrated");
                //statusList.Refresh();
                //statusList.Invalidate();

                //UpdateApplicationStatus("Migrating Employees, please wait...");
                //res = await MigrateEmployees();
                //statusList.Items.Add($"Employees - {res} migrated");
                //statusList.Refresh();
                //statusList.Invalidate();

                //UpdateApplicationStatus("Migrating Accounts, please wait...");
                //res = await MigrateAccounts();
                //statusList.Items.Add($"Accounts - {res} migrated");
                //statusList.Refresh();
                //statusList.Invalidate();

                UpdateApplicationStatus("Migrating Projects, please wait...");
                int res = await MigrateProjects();
                statusList.Items.Add($"Projects - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();

                //UpdateApplicationStatus("Migrating Project Allocations, please wait...");
                //res = await MigrateAllocations();
                //statusList.Items.Add($"Project Allocations - {res} migrated");
                //statusList.Refresh();
                //statusList.Invalidate();
            }
            catch (Exception exp)
            {
                UpdateApplicationStatus();
                MessageBox.Show(exp.Message);
            }
            finally
            {
                UpdateApplicationStatus();
            }
        }

        private async Task<int> MigrateSystemSettings()
        {
            List<SystemSetting> records = sqlDataContext.SystemSettings.ToList();
            SystemSettingRepository repository = new SystemSettingRepository();
            int recCount = 0;

            try
            {
                // delete all existing records from Postgres Database
                postgressDataContext.SystemSettings.RemoveRange(postgressDataContext.SystemSettings);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (SystemSetting rec in records)
                {
                    postgressDataContext.SystemSettings.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                //await UpdateSystemSettings();
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating System Settings. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            return recCount;
        }

        private async Task<int> MigrateDropDownCategories()
        {
            List<DropDownCategory> records = sqlDataContext.DropDownCategories.ToList();
            DropDownCategoryRepository repository = new DropDownCategoryRepository();
            int recCount = 0;

            try
            {
                // delete all existing records from Postgres Database
                postgressDataContext.DropDownCategories.RemoveRange(postgressDataContext.DropDownCategories);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (DropDownCategory rec in records)
                {
                    postgressDataContext.DropDownCategories.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                //await UpdateDropDownCategories();
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Categories. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            return recCount;
        }

        private async Task<int> MigrateDropDownSubCategories()
        {
            List<DropDownSubCategory> records = sqlDataContext.DropDownSubCategories.ToList();
            DropDownSubCategoryRepository repository = new DropDownSubCategoryRepository();
            int recCount = 0;

            try
            {
                // delete all existing records from Postgres Database
                postgressDataContext.DropDownSubCategories.RemoveRange(postgressDataContext.DropDownSubCategories);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (DropDownSubCategory rec in records)
                {
                    postgressDataContext.DropDownSubCategories.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                //await UpdateDropDownSubCategories();
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Sub-Categories. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            return recCount;
        }

        private async Task<int> MigratePractices()
        {
            List<Practice> records = sqlDataContext.Practices.ToList();
            PracticeRepository repository = new PracticeRepository();
            int recCount = 0;

            try
            {
                postgressDataContext = new Agilisium.TalentManager.PostgresModel.TalentManagerDataContext();
                // delete all existing records from Postgres Database
                postgressDataContext.Practices.RemoveRange(postgressDataContext.Practices);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (Practice rec in records)
                {
                    postgressDataContext.Practices.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                //await UpdatePractices();

                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating PODs. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            return recCount;
        }

        private async Task<int> MigrateSubPractices()
        {
            List<SubPractice> records = sqlDataContext.SubPractices.ToList();
            SubPracticeRepository repository = new SubPracticeRepository();
            int recCount = 0;

            try
            {
                // delete all existing records from Postgres Database
                postgressDataContext.SubPractices.RemoveRange(postgressDataContext.SubPractices);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (SubPractice rec in records)
                {
                    postgressDataContext.SubPractices.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                //await UpdateSubPractices();
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Competencies. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }

            return recCount;
        }

        private async Task<int> MigrateEmployees()
        {
            List<Employee> records = sqlDataContext.Employees.ToList();
            EmployeeRepository repository = new EmployeeRepository();
            int recCount = 0;

            try
            {
                // delete all existing records from Postgres Database
                postgressDataContext.Employees.RemoveRange(postgressDataContext.Employees);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (Employee rec in records)
                {
                    postgressDataContext.Employees.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                //await UpdateEmployees();
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Employees. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }

            return recCount;
        }

        private async Task<int> MigrateAccounts()
        {
            List<ProjectAccount> records = sqlDataContext.ProjectAccounts.ToList();
            ProjectAccountRepository repository = new ProjectAccountRepository();
            int recCount = 0;

            try
            {
                // delete all existing records from Postgres Database
                postgressDataContext.ProjectAccounts.RemoveRange(postgressDataContext.ProjectAccounts);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (ProjectAccount rec in records)
                {
                    postgressDataContext.ProjectAccounts.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                //await UpdateAcounts();
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Accounts. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }

            return recCount;
        }

        private async Task<int> MigrateProjects()
        {
            List<Project> records = sqlDataContext.Projects.ToList();
            ProjectRepository repository = new ProjectRepository();
            int recCount = 0;

            try
            {
                // delete all existing records from Postgres Database
                postgressDataContext.Projects.RemoveRange(postgressDataContext.Projects);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (Project rec in records)
                {
                    postgressDataContext.Projects.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                //await UpdateProjects();
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Projects. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }

            return recCount;
        }

        private async Task<int> MigrateAllocations()
        {
            List<ProjectAllocation> records = sqlDataContext.ProjectAllocations.ToList();
            AllocationRepository repository = new AllocationRepository();
            int recCount = 0;

            try
            {
                // delete all existing records from Postgres Database
                postgressDataContext.ProjectAllocations.RemoveRange(postgressDataContext.ProjectAllocations);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (ProjectAllocation rec in records)
                {
                    postgressDataContext.ProjectAllocations.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                //await UpdateAllocations();
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Project Allocations. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }

            return recCount;
        }

        private async Task UpdateSystemSettings()
        {
            con = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString);

            try
            {
                con.Open();
                sqlDataContext = new Agilisium.TalentManager.Model.TalentManagerDataContext();
                List<SystemSetting> oldSettings = sqlDataContext.SystemSettings.ToList();
                // update primary column id
                postgressDataContext = new Agilisium.TalentManager.PostgresModel.TalentManagerDataContext();
                List<SystemSetting> newSettings = postgressDataContext.SystemSettings.ToList();
                foreach (SystemSetting newRec in newSettings)
                {
                    int oldPracticeID = oldSettings.FirstOrDefault(p => p.SettingName == newRec.SettingName).SettingEntryID;
                    string qry = $"UPDATE \"TalentManager\".\"SystemSettings\" SET \"SettingEntryID\"={oldPracticeID} WHERE \"SettingEntryID\"={newRec.SettingEntryID}";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                    int val = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private async Task UpdateDropDownCategories()
        {
            con = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString);

            try
            {
                con.Open();
                sqlDataContext = new Agilisium.TalentManager.Model.TalentManagerDataContext();
                List<DropDownCategory> oldCategories = sqlDataContext.DropDownCategories.ToList();
                // update primary column id
                List<DropDownCategory> newCategories = postgressDataContext.DropDownCategories.ToList();
                foreach (DropDownCategory newRec in newCategories)
                {
                    int oldPracticeID = oldCategories.FirstOrDefault(p => p.CategoryName == newRec.CategoryName).CategoryID;
                    string qry = $"UPDATE \"TalentManager\".\"DropDownCategory\" SET \"CategoryID\"={oldPracticeID} WHERE \"CategoryID\"={newRec.CategoryID}";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                    int val = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private async Task UpdateDropDownSubCategories()
        {
            con = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString);

            try
            {
                con.Open();
                List<DropDownSubCategory> oldCategories = sqlDataContext.DropDownSubCategories.ToList();
                // update primary column id
                List<DropDownSubCategory> newCategories = postgressDataContext.DropDownSubCategories.ToList();
                foreach (DropDownSubCategory newRec in newCategories)
                {
                    int oldPracticeID = oldCategories.FirstOrDefault(p => p.SubCategoryName == newRec.SubCategoryName).SubCategoryID;
                    string qry = $"UPDATE \"TalentManager\".\"DropDownSubCategory\" SET \"SubCategoryID\"={oldPracticeID} WHERE \"SubCategoryID\"={newRec.SubCategoryID}";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                    int val = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private async Task UpdatePractices()
        {
            con = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString);

            try
            {
                con.Open();
                List<Practice> oldPractices = sqlDataContext.Practices.ToList();
                // update primary column id
                List<Practice> newPractices = postgressDataContext.Practices.ToList();
                foreach (Practice newRec in newPractices)
                {
                    int oldPracticeID = oldPractices.FirstOrDefault(p => p.PracticeName == newRec.PracticeName).PracticeID;
                    string qry = $"UPDATE \"TalentManager\".\"Practice\" SET \"PracticeID\"={oldPracticeID} WHERE \"PracticeID\"={newRec.PracticeID}";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                    int val = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private async Task UpdateSubPractices()
        {
            con = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString);

            try
            {
                con.Open();
                List<SubPractice> oldPractices = sqlDataContext.SubPractices.ToList();
                // update primary column id
                List<SubPractice> newPractices = postgressDataContext.SubPractices.ToList();
                foreach (SubPractice newRec in newPractices)
                {
                    int oldPracticeID = oldPractices.FirstOrDefault(p => p.SubPracticeName == newRec.SubPracticeName).SubPracticeID;
                    string qry = $"UPDATE \"TalentManager\".\"SubPractice\" SET \"SubPracticeID\"={oldPracticeID} WHERE \"SubPracticeID\"={newRec.SubPracticeID}";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                    int val = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private async Task UpdateEmployees()
        {
            con = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString);

            try
            {
                con.Open();
                List<Employee> oldPractices = sqlDataContext.Employees.ToList();
                // update primary column id
                List<Employee> newPractices = postgressDataContext.Employees.ToList();
                foreach (Employee newRec in newPractices)
                {
                    int oldPracticeID = oldPractices.FirstOrDefault(p => p.EmployeeID == newRec.EmployeeID).EmployeeEntryID;
                    string qry = $"UPDATE \"TalentManager\".\"Employee\" SET \"EmployeeEntryID\"={oldPracticeID} WHERE \"EmployeeEntryID\"={newRec.EmployeeEntryID}";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                    int val = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private async Task UpdateAcounts()
        {
            con = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString);

            try
            {
                con.Open();
                List<ProjectAccount> oldPractices = sqlDataContext.ProjectAccounts.ToList();
                // update primary column id
                List<ProjectAccount> newPractices = postgressDataContext.ProjectAccounts.ToList();
                foreach (ProjectAccount newRec in newPractices)
                {
                    int oldPracticeID = oldPractices.FirstOrDefault(p => p.AccountName == newRec.AccountName).AccountID;
                    string qry = $"UPDATE \"TalentManager\".\"ProjectAccount\" SET \"AccountID\"={oldPracticeID} WHERE \"AccountID\"={newRec.AccountID}";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                    int val = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private async Task UpdateProjects()
        {
            con = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString);

            try
            {
                con.Open();
                List<Project> oldPractices = sqlDataContext.Projects.ToList();
                // update primary column id
                List<Project> newPractices = postgressDataContext.Projects.ToList();
                foreach (Project newRec in newPractices)
                {
                    int oldPracticeID = oldPractices.FirstOrDefault(p => p.ProjectName == newRec.ProjectName).ProjectID;
                    string qry = $"UPDATE \"TalentManager\".\"Project\" SET \"ProjectID\"={oldPracticeID} WHERE \"ProjectID\"={newRec.ProjectID}";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                    int val = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private async Task UpdateAllocations()
        {
            con = new Npgsql.NpgsqlConnection(ConfigurationManager.ConnectionStrings["TalentDataContextPostgres"].ConnectionString);

            try
            {
                con.Open();
                List<ProjectAllocation> oldPractices = sqlDataContext.ProjectAllocations.ToList();
                // update primary column id
                List<ProjectAllocation> newPractices = postgressDataContext.ProjectAllocations.ToList();
                foreach (ProjectAllocation newRec in newPractices)
                {
                    int oldPracticeID = oldPractices.FirstOrDefault(p => p.EmployeeID == newRec.EmployeeID && p.ProjectID == newRec.ProjectID).AllocationEntryID;
                    string qry = $"UPDATE \"TalentManager\".\"ProjectAllocation\" SET \"AllocationEntryID\"={oldPracticeID} WHERE \"AllocationEntryID\"={newRec.AllocationEntryID}";
                    Npgsql.NpgsqlCommand cmd = new Npgsql.NpgsqlCommand(qry, con);
                    int val = await cmd.ExecuteNonQueryAsync();
                }
            }
            catch (Exception) { }
            finally
            {
                con.Close();
                con.Dispose();
            }
        }

        private void UpdateApplicationStatus(string message = "")
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                statusLabel.Text = "Ready";
                statusLabel.ForeColor = System.Drawing.Color.Black;
                Cursor = Cursors.Default;
                migrateDataButton.Enabled = true;
                statusLabel.BackColor = System.Drawing.Color.LightGreen;
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                statusLabel.ForeColor = System.Drawing.Color.Yellow;
                migrateDataButton.Enabled = false;
                statusLabel.BackColor = System.Drawing.Color.Red;
                statusLabel.Text = message;
            }
            Refresh();
            Invalidate();
        }
    }
}

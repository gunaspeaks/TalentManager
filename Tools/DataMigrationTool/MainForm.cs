using Agilisium.TalentManager.Model.Entities;
using Agilisium.TalentManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataMigrationTool
{
    public partial class MainForm : Form
    {
        private readonly Agilisium.TalentManager.Model.TalentManagerDataContext sqlDataContext = new Agilisium.TalentManager.Model.TalentManagerDataContext();
        private readonly Agilisium.TalentManager.PostgresModel.TalentManagerDataContext postgressDataContext = new Agilisium.TalentManager.PostgresModel.TalentManagerDataContext();

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MigrateDataButton_Click(object sender, EventArgs e)
        {
            statusList.Items.Clear();
            try
            {
                UpdateApplicationStatus("Migrating System Settings, please wait...");
                int res = await MigrateSystemSettings();
                statusList.Items.Add($"System Settings  - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();

                UpdateApplicationStatus("Migrating Categories, please wait...");
                res = await MigrateDropDownCategories();
                statusList.Items.Add($"Categories - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();

                UpdateApplicationStatus("Migrating Sub-Categories, please wait...");
                res = await MigrateDropDownSubCategories();
                statusList.Items.Add($"Sub-Categories - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();

                UpdateApplicationStatus("Migrating PODs, please wait...");
                res = await MigratePractices();
                statusList.Items.Add($"PODs - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();

                UpdateApplicationStatus("Migrating Competencies, please wait...");
                res = await MigrateSubPractices();
                statusList.Items.Add($"Competencies - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();

                UpdateApplicationStatus("Migrating Employees, please wait...");
                res = await MigrateEmployees();
                statusList.Items.Add($"Employees - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();

                UpdateApplicationStatus("Migrating Accounts, please wait...");
                res = await MigrateAccounts();
                statusList.Items.Add($"Accounts - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();

                UpdateApplicationStatus("Migrating Projects, please wait...");
                res = await MigrateProjects();
                statusList.Items.Add($"Projects - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();

                UpdateApplicationStatus("Migrating Project Allocations, please wait...");
                res = await MigrateAllocations();
                statusList.Items.Add($"Project Allocations - {res} migrated");
                statusList.Refresh();
                statusList.Invalidate();
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
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Sub-Categories. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            finally
            {
                repository.Dispose();
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
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Sub-Categories. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            finally
            {
                repository.Dispose();
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
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Sub-Categories. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            finally
            {
                repository.Dispose();
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
                // delete all existing records from Postgres Database
                postgressDataContext.Practices.RemoveRange(postgressDataContext.Practices);
                await postgressDataContext.SaveChangesAsync();

                // Migrage SQL Server data into Postgress database
                foreach (Practice rec in records)
                {
                    postgressDataContext.Practices.Add(rec);
                }
                await postgressDataContext.SaveChangesAsync();
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating PODs. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            finally
            {
                repository.Dispose();
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
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Competencies. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            finally
            {
                repository.Dispose();
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
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Employees. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            finally
            {
                repository.Dispose();
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
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Accounts. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            finally
            {
                repository.Dispose();
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
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Projects. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            finally
            {
                repository.Dispose();
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
                recCount = records.Count;
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while migrating Project Allocations. The error is {Environment.NewLine}Main Exception: {exp.Message}{Environment.NewLine}Inner Exception: {exp.InnerException?.Message}");
            }
            finally
            {
                repository.Dispose();
            }
            return recCount;
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

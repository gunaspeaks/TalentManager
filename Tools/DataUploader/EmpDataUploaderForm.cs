using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DataUploader
{
    public partial class EmpDataUploaderForm : Form
    {
        private DataTable empDataTable = null;

        public EmpDataUploaderForm()
        {
            InitializeComponent();
        }

        private void BrowseButton_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                DialogResult res = dlg.ShowDialog();
                dlg.Title = "Excel File Dialog";
                dlg.InitialDirectory = @"c:\";
                dlg.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                dlg.FilterIndex = 2;
                if (res != DialogResult.OK)
                {
                    return;
                }

                fileNameText.Text = dlg.FileName;
                rawDataGrid.DataSource = null;
                rawDataGrid.Refresh();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void ReadFileButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(fileNameText.Text))
                {
                    MessageBox.Show("Please select a Excel file containing Employee data");
                    return;
                }

                if (!fileNameText.Text.ToLower().Contains(".xlsx"))
                {
                    MessageBox.Show("The selected file is not in a appropriate Excel format(.xlsx)");
                    return;
                }

                msgStatusLabel.Text = "Reading Excel document... Please wait.";
                msgStatusLabel.BackColor = Color.Red;
                msgStatusLabel.ForeColor = Color.Yellow;
                Cursor = Cursors.WaitCursor;
                Refresh();

                System.Threading.Thread.Sleep(100);
                ReadExcel();
                rawDataGrid.Refresh();

                msgStatusLabel.Text = "Preparing data for upload ready... Please wait.";
                ProcessExcelData();
                //PrepareGridDesign();
                //await ReadCsvFileAsync(fileNameText.Text);
            }
            catch (Exception exp)
            {
                msgStatusLabel.Text = "Error while reading the file";
                Cursor = Cursors.Default;
                Refresh();
                MessageBox.Show(exp.Message);
            }
            finally
            {
                msgStatusLabel.Text = "Ready";
                Cursor = Cursors.Default;
                msgStatusLabel.BackColor = Color.SpringGreen;
                msgStatusLabel.ForeColor = Color.Black;
                Refresh();
            }
        }

        private void PrepareGridDesign()
        {
            extractedDataGrid.DataSource = null;
            empDataTable = new DataTable();
            List<DataColumn> cols = new List<DataColumn>
            {
                new DataColumn
                {
                    ColumnName="RowNo",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="EmpID",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="FirstName",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="LastName",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="BU",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="DoJ",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="LWD",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="POD",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="Competency",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="PM",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="PrimarySkills",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="SecondarySkills",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="UploadStatus",
                    DataType=typeof(string)
                },
                new DataColumn
                {
                    ColumnName="StatusMessage",
                    DataType=typeof(string)
                },
            };

            empDataTable.Columns.AddRange(cols.ToArray());
            extractedDataGrid.DataSource = empDataTable;
        }

        private async Task ReadCsvFileAsync(string filepath)
        {
            FileStream fs = null;
            StreamReader sr = null;
            int rowNo = 0;
            StringBuilder strErrors = new StringBuilder();
            try
            {
                fs = File.OpenRead(fileNameText.Text);
                sr = new StreamReader(fs);

                while (!sr.EndOfStream)
                {
                    try
                    {
                        rowNo += 1;
                        string line = await sr.ReadLineAsync();
                        DataRow row = ConvertCsvStringToDataRow(line, rowNo);
                        empDataTable.Rows.Add(row);
                        rawDataGrid.Refresh();
                    }
                    catch (Exception exp1)
                    {
                        strErrors.AppendLine($"Error while processing excel row: {rowNo}. The error is {exp1.Message}");
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                    sr.Dispose();
                }
                if (fs != null)
                {
                    fs.Close();
                    fs.Dispose();
                }
                if (strErrors.Length > 0)
                {
                    MessageBox.Show(strErrors.ToString());
                }
            }
        }

        private DataRow ConvertCsvStringToDataRow(string excelRow, int rowNo)
        {
            DataRow row = empDataTable.NewRow();

            string[] splitedStr = excelRow.Split(',');
            row["RowNo"] = rowNo;
            int colsCount = splitedStr.Length;

            if (colsCount > 0)
            {
                row["EmpID"] = splitedStr[0];

                string[] empName = splitedStr[1].Split(' ');
                row["FirstName"] = empName[0];
                if (empName.Length > 0)
                {
                    row["LastName"] = empName[1];
                }

                row["BU"] = splitedStr[2];
                row["DoJ"] = splitedStr[3] + ", " + splitedStr[4];
                int nextColIndex = 7;
                if (string.IsNullOrWhiteSpace(splitedStr[5]))
                {
                    row["LWD"] = "";
                    nextColIndex = 6;
                }
                else
                {
                    row["LWD"] = splitedStr[5] + ", " + splitedStr[6];
                }

                row["POD"] = splitedStr[nextColIndex];
                row["Competency"] = splitedStr[++nextColIndex];
                row["PM"] = splitedStr[++nextColIndex];
                row["PrimarySkills"] = splitedStr[++nextColIndex];
                row["SecondarySkills"] = splitedStr[++nextColIndex];
            }

            return row;
        }

        private void ProcessExcelData()
        {
            PrepareGridDesign();

            for (int rowNo = 1; rowNo < rawDataGrid.Rows.Count; rowNo++)
            {
                try
                {
                    DataRow row = empDataTable.NewRow();
                    row["RowNo"] = rowNo;

                    string empID = rawDataGrid.Rows[rowNo].Cells[0].Value?.ToString();
                    if (string.IsNullOrWhiteSpace(empID))
                    {
                        break;
                    }
                    row["EmpID"] = empID;

                    string fullName = rawDataGrid.Rows[rowNo].Cells[1].Value == null ? "," : rawDataGrid.Rows[rowNo].Cells[1].Value.ToString();
                    string[] empName = fullName.Split(' ');
                    row["FirstName"] = empName[0];
                    row["LastName"] = fullName.Replace(empName[0], "");

                    row["BU"] = rawDataGrid.Rows[rowNo].Cells[2].Value?.ToString();

                    DateTime dtDOJ = DateTime.Now;
                    if (rawDataGrid.Rows[rowNo].Cells[3].Value != null)
                    {
                        string dtStr = rawDataGrid.Rows[rowNo].Cells[3].Value.ToString();
                        if (double.TryParse(dtStr, out double dblDOJ))
                        {
                            dtDOJ = DateTime.FromOADate(dblDOJ);
                        }
                    }
                    row["DoJ"] = dtDOJ.ToShortDateString();

                    DateTime? dtLWD = null;
                    if (rawDataGrid.Rows[rowNo].Cells[4].Value != null)
                    {
                        string dtStr = rawDataGrid.Rows[rowNo].Cells[4].Value.ToString();
                        if (double.TryParse(dtStr, out double dblLWD))
                        {
                            dtLWD = DateTime.FromOADate(dblLWD);
                        }
                    }

                    if (dtLWD.HasValue)
                    {
                        row["LWD"] = dtLWD.Value.ToShortDateString();
                    }

                    row["POD"] = rawDataGrid.Rows[rowNo].Cells[5].Value?.ToString();
                    row["Competency"] = rawDataGrid.Rows[rowNo].Cells[6].Value?.ToString();
                    row["PM"] = rawDataGrid.Rows[rowNo].Cells[7].Value?.ToString();
                    row["PrimarySkills"] = rawDataGrid.Rows[rowNo].Cells[9].Value?.ToString();
                    row["SecondarySkills"] = rawDataGrid.Rows[rowNo].Cells[10].Value?.ToString();
                    row["UploadStatus"] = "Not Uploaded Yet";
                    row["StatusMessage"] = "";
                    empDataTable.Rows.Add(row);
                    extractedDataGrid.Refresh();
                }
                catch (Exception exp)
                {
                    DialogResult res = MessageBox.Show($"Error while processing Row {rowNo}. The error is:\"{exp.Message}\". Would you like to proceed with the remaining rows?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (res == DialogResult.No)
                    {
                        break;
                    }
                }
            }
        }

        private void ReadExcel()
        {
            string fileName = fileNameText.Text;
            try
            {
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook xlWorkbook = xlApp.Workbooks.Open(fileName);
                Microsoft.Office.Interop.Excel._Worksheet xlWorksheet = xlWorkbook.Sheets[1];
                Microsoft.Office.Interop.Excel.Range xlRange = xlWorksheet.UsedRange;

                int rowCount = xlRange.Rows.Count;
                int colCount = xlRange.Columns.Count;

                // dt.Column = colCount;  
                rawDataGrid.ColumnCount = colCount;
                rawDataGrid.RowCount = rowCount;

                int emptyCellsCount = 0;
                for (int i = 1; i <= rowCount; i++)
                {
                    for (int j = 1; j <= colCount; j++)
                    {
                        //write the value to the Grid  
                        if (xlRange.Cells[i, j] != null && xlRange.Cells[i, j].Value2 != null)
                        {
                            string cellVal = xlRange.Cells[i, j].Value2.ToString();
                            if (string.IsNullOrWhiteSpace(cellVal)) emptyCellsCount++;
                            rawDataGrid.Rows[i - 1].Cells[j - 1].Value = cellVal;

                            if (emptyCellsCount > 5) break;
                        }
                    }
                }

                msgStatusLabel.Text = "Reading complete. Cleaning up memory which is occupied for Excel document... Please wait.";

                //cleanup  
                GC.Collect();
                GC.WaitForPendingFinalizers();

                //rule of thumb for releasing com objects:  
                //  never use two dots, all COM objects must be referenced and released individually  
                //  ex: [somthing].[something].[something] is bad  

                //release com objects to fully kill excel process from running in the background  
                Marshal.ReleaseComObject(xlRange);
                Marshal.ReleaseComObject(xlWorksheet);

                //close and release  
                xlWorkbook.Close();
                Marshal.ReleaseComObject(xlWorkbook);

                //quit and release  
                xlApp.Quit();
                Marshal.ReleaseComObject(xlApp);
            }
            catch (Exception exp)
            {
                MessageBox.Show($"Error while readin Excel file. The error is: {exp.Message}");
            }
        }

        private void ExtractDataButton_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessExcelData();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void UploadButton_Click(object sender, EventArgs e)
        {
            if (empDataTable == null || (empDataTable != null && empDataTable.Rows.Count == 0))
            {
                MessageBox.Show("No data to upload");
                return;
            }

            try
            {
                EmployeeRepository repository = new EmployeeRepository();
                DropDownSubCategoryRepository subCategoryRepository = new DropDownSubCategoryRepository();
                PracticeRepository practiceRepo = new PracticeRepository();
                SubPracticeRepository subPracticeRepo = new SubPracticeRepository();

                foreach (DataRow row in empDataTable.Rows)
                {
                    try
                    {
                        if(repository.IsDuplicateEmployeeID(row["EmpID"].ToString().Trim()))
                        {
                            row["UploadStatus"] = "Failed";
                            row["StatusMessage"] = "Duplicate Employee ID";
                            continue;
                        }

                        if (repository.IsDuplicateName(row["FirstName"].ToString().Trim(), row["LastName"].ToString().Trim()))
                        {
                            row["UploadStatus"] = "Failed";
                            row["StatusMessage"] = "Duplicate Employee Name";
                            continue;
                        }

                        DropDownSubCategoryDto buType = subCategoryRepository.GetByName(row["BU"].ToString(), (int)CategoryType.BusinessUnit);

                        string empID = row["EmpID"].ToString();
                        string empType = "Permanent";
                        if (empID.StartsWith("CE"))
                        {
                            empType = "Contractor";
                        }
                        else if (empID.StartsWith("CI"))
                        {
                            empType = "Internship";
                        }
                        else if (empID.StartsWith("YTJ"))
                        {
                            empType = "Yet to Join";
                        }
                        DropDownSubCategoryDto employmentType = subCategoryRepository.GetByName(empType, (int)CategoryType.EmploymentType);
                        PracticeDto practiceDto = practiceRepo.GetByNameOrDefault(row["POD"].ToString());
                        SubPracticeDto subPracticeDto = subPracticeRepo.GetByName(row["Competency"].ToString(), practiceDto.PracticeID);
                        EmployeeDto entry = new EmployeeDto
                        {
                            BusinessUnitID = buType.SubCategoryID,
                            DateOfJoin = DateTime.Parse(row["DoJ"].ToString()),
                            EmployeeID = empID,
                            EmploymentTypeID = employmentType.SubCategoryID,
                            FirstName = row["FirstName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            PracticeID = practiceDto.PracticeID,
                            PrimarySkills = row["PrimarySkills"].ToString(),
                            SecondarySkills = row["SecondarySkills"].ToString(),
                            SubPracticeID = subPracticeDto.SubPracticeID,
                        };

                        if (string.IsNullOrWhiteSpace(row["LWD"].ToString()) == false)
                        {
                            entry.LastWorkingDay = DateTime.Parse(row["LWD"].ToString());
                        }

                        repository.Add(entry);
                        row["UploadStatus"] = "Successfully Uploaded";
                        row["StatusMessage"] = "";
                    }
                    catch (Exception exp)
                    {
                        row["UploadStatus"] = "Failed";
                        row["StatusMessage"] = exp.Message;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }
    }

}

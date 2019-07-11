using Agilisium.TalentManager.ReportingService;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsServiceExecutor
{
    public partial class MainForm : Form
    {

        private readonly ILog logger;

        public MainForm()
        {
            InitializeComponent();
            logger = log4net.LogManager.GetLogger(typeof(MainForm));
        }

        private void EmailReportingServiceButton_Click(object sender, EventArgs e)
        {
            try
            {
                logger.Info("");
                logger.Info("*********************************************************************************************");
                logger.Info("Email Service has started processing");

                MessageBox.Show(DateTime.Today.DayOfWeek.ToString());

                TalentManagerEmailProcessor processor = new TalentManagerEmailProcessor();
                logger.Info("Generating resource allocation report");

                processor.GenerateResourceAllocationReport();

                MessageBox.Show("Message sent");
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
                logger.Error("Error while generating allocation report");
                logger.Error(exp);
            }
        }
    }
}

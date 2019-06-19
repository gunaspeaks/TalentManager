using Agilisium.TalentManager.ReportingService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Agilisium.TalentManager.WindowServices
{
    public partial class TalentManagerEmailService : ServiceBase
    {
        private Timer serviceTimer;

        public TalentManagerEmailService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                TalentManagerEmailProcessor processor = new TalentManagerEmailProcessor();
                processor.GenerateResourceAllocationReport();

                serviceTimer = new Timer();
                double defaultScheduledMin = 12 * 60 * 60 * 1000;

                if (String.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["serviceTriggerInterval"]) == false)
                {
                    try
                    {
                        defaultScheduledMin = Convert.ToDouble(ConfigurationManager.AppSettings["serviceTriggerInterval"]) * 60 * 60 * 1000;
                    }
                    catch (Exception) { }
                }
                serviceTimer.Interval = defaultScheduledMin;
                serviceTimer.Enabled = true;
                serviceTimer.Start();
                serviceTimer.Elapsed += TimerElapsed;
            }
            catch (Exception exp)
            {
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            TalentManagerEmailProcessor processor = new TalentManagerEmailProcessor();
            processor.GenerateResourceAllocationReport();
        }

        protected override void OnStop()
        {
            serviceTimer.Stop();
            serviceTimer.Dispose();
        }
    }
}

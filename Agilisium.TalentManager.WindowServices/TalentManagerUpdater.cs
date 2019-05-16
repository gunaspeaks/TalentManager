using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace Agilisium.TalentManager.WindowServices
{
    internal partial class TalentManagerUpdater : ServiceBase
    {
        private Timer serviceTimer;

        public TalentManagerUpdater()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                serviceTimer = new Timer();
                double defaultScheduledMin = 12 * 60 * 60 * 1000;

                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["serviceTriggerInterval"]) == false)
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
            catch (Exception)
            {
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            //serviceHelper.SendPeriodicCastExecutionReport();
        }

        protected override void OnStop()
        {
            serviceTimer.Stop();
            serviceTimer.Dispose();
        }
    }
}

using Agilisium.TalentManager.ServiceProcessors;
using log4net;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace Agilisium.TalentManager.WindowServices
{
    public partial class TalentManagerEmailService : ServiceBase
    {
        private Timer serviceTimer;
        private readonly ILog logger;

        public TalentManagerEmailService()
        {
            InitializeComponent();
            logger = log4net.LogManager.GetLogger(typeof(TalentManagerEmailService));
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                logger.Info("");
                logger.Info("*********************************************************************************************");
                logger.Info("Email Service has started processing");

                int serviceExecutionDayOfWeek = 1;
                double defaultScheduledMin = 12 * 60 * 60 * 1000;

                if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["serviceExecutionDayOfWeek"]) == false)
                {
                    try
                    {
                        serviceExecutionDayOfWeek = Convert.ToInt32(ConfigurationManager.AppSettings["serviceExecutionDayOfWeek"]);
                        defaultScheduledMin = Convert.ToDouble(ConfigurationManager.AppSettings["serviceTriggerInterval"]) * 60 * 60 * 1000;
                    }
                    catch (Exception exp)
                    {
                        logger.Error("Error while reading configuration");
                        logger.Error(exp);
                    }
                }

                if (serviceExecutionDayOfWeek != (int)DateTime.Today.DayOfWeek)
                {
                    logger.Info($"Today is not {serviceExecutionDayOfWeek}, so report will not be sent.");
                    return;
                }

                AllocationsMessengerServiceProcessor processor = new AllocationsMessengerServiceProcessor();
                logger.Info("Generating resource allocation report");

                processor.GenerateResourceAllocationReport();

                serviceTimer = new Timer
                {
                    Interval = defaultScheduledMin,
                    Enabled = true
                };
                serviceTimer.Start();
                serviceTimer.Elapsed += TimerElapsed;
            }
            catch (Exception exp)
            {
                logger.Error("Error while generating allocation report");
                logger.Error(exp);
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            AllocationsMessengerServiceProcessor processor = new AllocationsMessengerServiceProcessor();
            processor.GenerateResourceAllocationReport();
        }

        protected override void OnStop()
        {
            serviceTimer.Stop();
            serviceTimer.Dispose();
        }
    }
}

using Agilisium.TalentManager.ServiceProcessors;
using log4net;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace AllocationsUpdaterService
{
    public partial class AllocationsUpdaterService : ServiceBase
    {
        private Timer serviceTimer;
        private readonly ILog logger;


        public AllocationsUpdaterService()
        {
            InitializeComponent();
            logger = log4net.LogManager.GetLogger(typeof(AllocationsUpdaterService));
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                logger.Info("");
                logger.Info("*********************************************************************************************");
                logger.Info("Starting the service");

                int serviceExecutionDayOfWeek = 1;
                double defaultScheduledMin = 23 * 60 * 60 * 1000;

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

                AllocationsUpdaterServiceProcessor processor = new AllocationsUpdaterServiceProcessor();
                logger.Info("Identifying new allocation entries for notification");
                int newEntries = processor.ProcessAllocations();
                logger.Info($"There are {newEntries} entries added for notification");

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
                logger.Error("Error while updating allocation notifications");
                logger.Error(exp);
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            logger.Info("Execution started");
            try
            {
                AllocationsUpdaterServiceProcessor processor = new AllocationsUpdaterServiceProcessor();
                logger.Info("Processing notifications");
                int newEntries = processor.ProcessAllocations();
                logger.Info($"There are {newEntries} entries had been processed");
                logger.Info("Execution completed");
                logger.Info("*********************************************************************************************");
                logger.Info("");
            }
            catch (Exception exp)
            {
                logger.Error("Error while updating allocation notifications");
                logger.Error(exp);
            }
        }

        protected override void OnStop()
        {
            serviceTimer.Stop();
            serviceTimer.Dispose();
        }
    }
}

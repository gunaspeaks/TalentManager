using Agilisium.TalentManager.ServiceProcessors;
using log4net;
using System;
using System.Configuration;
using System.ServiceProcess;
using System.Timers;

namespace AllocationsMessengerService
{
    public partial class AllocationsMessengerService : ServiceBase
    {
        private Timer serviceTimer;
        private readonly ILog logger;

        public AllocationsMessengerService()
        {
            InitializeComponent();
            logger = log4net.LogManager.GetLogger(typeof(AllocationsMessengerService));
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                logger.Info("");
                logger.Info("*********************************************************************************************");
                logger.Info(" Starting the service...");

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
                    logger.Info("Service execution is igored as it not is scheduled on this day");
                    return;
                }

                //AllocationsMessengerServiceProcessor processor = new AllocationsMessengerServiceProcessor();
                //logger.Info("Generating resource allocation report");

                //processor.GenerateResourceAllocationReport();

                serviceTimer = new Timer
                {
                    Interval = defaultScheduledMin,
                    Enabled = true
                };
                serviceTimer.Start();
                serviceTimer.Elapsed += TimerElapsed;
                logger.Info("Started successfully");
            }
            catch (Exception exp)
            {
                logger.Error("Error while initializing the service");
                logger.Error(exp);
            }
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            logger.Info("Execution started");
            try
            {
                AllocationsMessengerServiceProcessor processor = new AllocationsMessengerServiceProcessor();
                processor.GenerateResourceAllocationReport();
                logger.Info("Execution completed");
                logger.Info("*********************************************************************************************");
                logger.Info("");
            }
            catch (Exception exp)
            {
                logger.Error("Error while executing the service");
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

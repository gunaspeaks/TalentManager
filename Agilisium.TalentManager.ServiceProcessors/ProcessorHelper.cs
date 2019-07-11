using Agilisium.TalentManager.Repository.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace Agilisium.TalentManager.ServiceProcessors
{
    public static class ProcessorHelper
    {
        public const string EMAIL_PROXY_SERVER = "Email Proxy Server";
        public const string EMAIL_PROXY_PORT = "Email Proxy Port";
        public const string CONTRACTOR_REQ_EMAIL_OWNER = "Contractor Request Email Owner";
        public const string CONTRACTOR_REQ_BCC_RECEIPIENTS = "Contractor Request Email BCC Email IDs";
        public const string EMAIL_OWNERS_PASSWORD = "Owner's Outlook EMAIL Password";
        public const string TEMPLATE_FOLDER_PATH = "Email Templates Folder Path";
        public const string MANAGERS_EMAIL_GROUP = "Agilisium Managers Email Group";
        public const string MANAGERS_EMAIL_GROUP_BCC = "Agilisium Managers BCC Email Group";

        private static SystemSettingRepository settingsRepo;
        private static List<Dto.SystemSettingDto> systemSettings;

        static ProcessorHelper()
        {
            settingsRepo = new SystemSettingRepository();
            systemSettings = settingsRepo.GetAll().ToList();
        }

        public static string GetSettingsValue(string settingsName)
        {
            return systemSettings.FirstOrDefault(e => e.SettingName == settingsName)?.SettingValue;
        }
    }
}

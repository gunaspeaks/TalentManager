using Agilisium.TalentManager.Dto;
using Agilisium.TalentManager.ServerUtilities;
using Agilisium.TalentManager.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Agilisium.TalentManager.ReportingService
{
    public class ContractorRequestProcessor
    {
        private IServiceRequestService requestService;
        private ISystemSettingsService settingsService;

        public ContractorRequestProcessor(IServiceRequestService requestService, ISystemSettingsService settingsService)
        {
            this.requestService = requestService;
            this.settingsService = settingsService;
        }

        public void ProcessPendingServiceRequests(string templateFilePath)
        {
            try
            {
                string emailTemplateContent = FilesHandler.GetFileContent(templateFilePath);
                List<ServiceRequestDto> requests = requestService.GetAllEmailPendingRequests();

                string emailClientIP = settingsService.GetSystemSettingValue("Email Proxy Server");
                string ownerEmailID = settingsService.GetSystemSettingValue("Contractor Request Email Owner");
                string bccEmailID = settingsService.GetSystemSettingValue("Contractor Request Email BCC Email IDs");
                string outlookPwd = settingsService.GetSystemSettingValue("Owner's Outlook EMAIL Password");
                string emailSubject = "New Contractor Request for Agilisium";
                EmailHandler emailHandler = new EmailHandler(ownerEmailID, outlookPwd);

                foreach (var request in requests)
                {
                    try
                    {
                        StringBuilder vendorEmail = new StringBuilder(emailTemplateContent);
                        vendorEmail.Replace("__EMAIL_BODY__", request.EmailMessage);
                        emailHandler.SendEmail(emailClientIP, request.VendorEmailID, emailSubject, vendorEmail.ToString(), bccEmailID);
                        requestService.UpdateEmailSentStatus(request.ServiceRequestID);
                    }
                    catch (Exception)
                    {}
                }
            }
            catch (Exception)
            {

            }
        }
    }
}

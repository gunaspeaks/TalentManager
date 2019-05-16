using System;
using System.Net;
using System.Net.Mail;

namespace Agilisium.TalentManager.ServerUtilities
{
    public class EmailHandler
    {
        private readonly string outlookPassword;
        private readonly string ownerEmailID;

        public EmailHandler(string ownerEmailID, string outlookPassword)
        {
            this.outlookPassword = outlookPassword;
            this.ownerEmailID = ownerEmailID;
        }

        public void SendEmail(string emailClientIp, string toEmailID, string emailSubject, 
            string emailBody, string bccEmailId = "")
        {
            SmtpClient smtpClient = null;
            MailMessage mailMessage = new MailMessage()
            {
                From = new MailAddress(ownerEmailID),
                Subject = emailSubject,
                IsBodyHtml = true,
                Body = emailBody
            };

            try
            {
                mailMessage.To.Add(toEmailID);
                if (string.IsNullOrWhiteSpace(bccEmailId) == false)
                {
                    string[] mailIDs = bccEmailId.Split(';');
                    foreach (string str in mailIDs)
                    {
                        mailMessage.CC.Add(new MailAddress(str));
                    }
                }
                smtpClient = new SmtpClient(emailClientIp, 587)
                {
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                smtpClient.Credentials = new NetworkCredential(ownerEmailID, outlookPassword);
                smtpClient.Send(mailMessage);
            }
            catch (Exception exp) { throw exp; }
            finally
            {
                if (smtpClient != null)
                {
                    smtpClient.Dispose();
                }

                mailMessage.Dispose();
            }
        }

        //public static string GetEmailIDByEmployeeID(string employeeID)
        //{
        //    return null;
        //    //string emailID = "";
        //    //string ldappUrl = ConfigurationManager.AppSettings["ldapURL"];
        //    //using (DirectoryEntry de = new DirectoryEntry("LDAP://cts.com"))
        //    //{
        //    //    using (DirectorySearcher adSearch = new DirectorySearcher(de))
        //    //    {
        //    //        adSearch.Filter = string.Format("(sAMAccountName={0})", employeeID);
        //    //        adSearch.PropertiesToLoad.Add("mail");
        //    //        SearchResult adSearchResult = adSearch.FindOne();

        //    //        emailID = adSearchResult.Properties["mail"][0].ToString();
        //    //    }
        //    //}
        //    //return emailID;
        //}

        //private void GetEMailAttachment(string pdfFileName)
        //{
        //    //DirectoryInfo directory = new DirectoryInfo(@"D:\PDFTemp\");
        //    //if (directory.Exists)
        //    //{
        //    //    foreach (FileInfo file in directory.GetFiles())
        //    //    {
        //    //        if (file.Name == pdfFileName)
        //    //        {
        //    //            orderPDFFilePath = file.FullName;
        //    //        }
        //    //    }                
        //    //}          
        //}
    }
}

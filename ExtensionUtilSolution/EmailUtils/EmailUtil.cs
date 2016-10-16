using LogUtils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailUtils
{
    public class EmailUtil
    {
        #region Private members
        private static readonly EmailUtil instance = new EmailUtil(true);
        private EmailUtilSection emailUtilSection = null;
        private EmailFromUser emailFromUser = null;
        private SMTPClientConfiguration emailConfiguration = null;
        private String strUsername = String.Empty;
        private String strPassword = String.Empty;
        private String strFromDisplayName = String.Empty;
        private String strHost = String.Empty;
        private int nPort = 0;
        private bool bEnableSSL = false;
        private bool bIsSendMail = false;
        private bool bIncludeAttachment = false;
        #endregion

        #region Constructors
        static EmailUtil()
        {

        }

        public EmailUtil()
        {

        }
        private EmailUtil(bool b)
        {
            emailUtilSection = ConfigurationManager.GetSection("EmailUtilCustomLog") as EmailUtilSection;

            if (emailUtilSection != null)
            {
                bIsSendMail = emailUtilSection.IsSendEmail;
                if (bIsSendMail)
                {
                    emailFromUser = emailUtilSection.EmailFromUserInfo;
                    if (emailFromUser != null)
                    {
                        strUsername = emailFromUser.Username;
                        ApplicationLog.Instance.WriteDebug(String.Format("Username from EmailUtil - '{0}'", strUsername));
                        strPassword = emailFromUser.Password;
                        ApplicationLog.Instance.WriteDebug(String.Format("Password from EmailUtil - '{0}'", strPassword));
                        strFromDisplayName = emailFromUser.FromDisplayName;
                        ApplicationLog.Instance.WriteDebug(String.Format("Display name from EmailUtil - '{0}'", strFromDisplayName));
                    }
                    emailConfiguration = emailUtilSection.SMTPConfigInfo;
                    if (emailConfiguration != null)
                    {
                        strHost = emailConfiguration.Host;
                        ApplicationLog.Instance.WriteDebug(String.Format("Host from EmailUtil - '{0}'", strHost));
                        nPort = emailConfiguration.Port;
                        ApplicationLog.Instance.WriteDebug(String.Format("Port from EmailUtil - '{0}'", nPort));
                        bEnableSSL = emailConfiguration.EnableSSL;
                        ApplicationLog.Instance.WriteDebug(String.Format("EnableSSL from EmailUtil - '{0}'", bEnableSSL));
                        bIncludeAttachment = emailConfiguration.IncludeAttachment;
                        ApplicationLog.Instance.WriteDebug(String.Format("IncludeAttachment from EmailUtil - '{0}'", bIncludeAttachment));
                    }
                }
            }
        }
        #endregion

        #region Properties
        public String ToName { get; set; }
        public String ToEmail { get; set; }
        public String Subject { get; set; }
        public String Body { get; set; }

        public static EmailUtil Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion

        #region MemberFunction
        public bool SendEmail(EmailUtil objEmailUtil, String strUsername = "", String strPassword = "", String strFromDisplayName = "", String strAttchmentFileName = "")
        {
            bool bFlag = false;
            SmtpClient smtpClient = null;
            MailMessage mail = null;
            try
            {
                if (bIsSendMail)
                {
                    String strEmailUsername = !String.IsNullOrEmpty(strUsername) ? strUsername : this.strUsername;
                    ApplicationLog.Instance.WriteDebug(String.Format("Email Username - '{0}'", strEmailUsername));
                    String strEmailPassword = !String.IsNullOrEmpty(strPassword) ? strUsername : this.strPassword;
                    ApplicationLog.Instance.WriteDebug(String.Format("Email Username - '{0}'", strEmailUsername));
                    String strEmailDisplayName = !String.IsNullOrEmpty(strFromDisplayName) ? strFromDisplayName : this.strFromDisplayName;
                    ApplicationLog.Instance.WriteDebug(String.Format("Email Display name - '{0}'", strEmailDisplayName));

                    MailAddress from = new MailAddress(strEmailUsername, strEmailDisplayName);
                    MailAddress to = new MailAddress(objEmailUtil.ToEmail, !String.IsNullOrEmpty(objEmailUtil.ToName) ? objEmailUtil.ToName : "Dear");
                    ApplicationLog.Instance.WriteDebug(String.Format("Sendming Email to: '{0}'-'{1}'", objEmailUtil.ToName, objEmailUtil.ToEmail));

                    smtpClient = new SmtpClient()
                    {
                        Host = this.strHost,
                        Port = this.nPort,
                        EnableSsl = this.bEnableSSL,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(from.Address, strEmailPassword),
                    };

                    mail = new MailMessage(from, to)
                    {
                        Subject = objEmailUtil.Subject,
                        Body = objEmailUtil.Body,
                        IsBodyHtml = true,
                    };

                    //for multiple emails
                    //mail.To.Add(to);
                    //For attachments
                    if (this.bIncludeAttachment && !String.IsNullOrEmpty(strAttchmentFileName))
                        mail.Attachments.Add(new Attachment(strAttchmentFileName));

                    smtpClient.Send(mail);
                    ApplicationLog.Instance.WriteDebug(String.Format("Mail sent to: '{0}'-'{1}'", objEmailUtil.ToName, objEmailUtil.ToEmail));
                    bFlag = true;
                }
                else
                    ApplicationLog.Instance.WriteWarning("Email sending disabled from configuration. Please correct in .config file.");
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (smtpClient != null)
                    smtpClient.Dispose();
                if (mail != null)
                    mail.Dispose();
            }
            return bFlag;
        }
        #endregion
    }
}

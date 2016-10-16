using BussinessObjects;
using EmailUtils;
using HappyHutMiddleTier;
using HappyHutWinSvcAccountManager;
using LogUtils;
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

namespace HappyHutEmailWinSvc
{
    public partial class EmailService : ServiceBase
    {
        #region Objects
        long lPollingInterval = 30000;
        List<GetQuoteInfo> lstQuotes = new List<GetQuoteInfo>();
        List<String> lstCreateUser = new List<string>();
        Timer timer1 = null;
        DatabaseClass client = null;
        #endregion

        #region Constructor
        public EmailService()
        {
            InitializeComponent();
        }
        #endregion

        #region Member function
        protected override void OnStart(string[] args)
        {
            try
            {
                ApplicationLog.Instance.WriteInfo("Email Windows Service started.");
                timer1 = new Timer();
                if (ConfigurationManager.AppSettings.AllKeys.Contains("PollingInterval"))
                    long.TryParse(ConfigurationManager.AppSettings["PollingInterval"], out lPollingInterval);
                timer1.Interval = lPollingInterval;
                timer1.Enabled = true;
                timer1.Elapsed += timer1_Elapsed;
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }

        void timer1_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                PerformDataBaseOperations();
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }

        public void PerformDataBaseOperations()
        {
            GetEmailListFromDatabase();
            UpdateQuoteRequests();
            CreateHappyHutUsers();
        }

        private void UpdateQuoteRequests()
        {
            try
            {
                client = new DatabaseClass();
                client.SetCustomerRequestInfos(lstQuotes, null);
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }

        private void CreateHappyHutUsers()
        {
            try
            {
                List<HappyHutUserInfo> lstHappyHutUsers = new List<HappyHutUserInfo>();
                if (lstCreateUser != null && lstCreateUser.Count > 0)
                {
                    foreach (String strEmail in lstCreateUser)
                    {
                        if (CreateUser(strEmail))
                        {
                            GetQuoteInfo quoteInfo = lstQuotes.Where(x => x.Email.ToLower().Equals(strEmail.ToLower())).FirstOrDefault();

                            EmailUtil utilObject = new EmailUtil()
                            {
                                ToEmail = strEmail,
                                ToName = String.Format("{0} {1}", quoteInfo.FirstName, quoteInfo.LastName),
                                Subject = "Login information for HappyHut!!!",
                            };
                            //TODO:: Add Body Template
                            bool emailSent = EmailUtil.Instance.SendEmail(utilObject);
                            if (!emailSent)
                            {
                                ApplicationLog.Instance.WriteWarning(String.Format("Email not sent to {0} for service login", strEmail));
                                //TODO:: Send email to support@happyhut.in && pratik@happyhut.in
                            }

                            if (quoteInfo != null)
                            {
                                HappyHutUserInfo inf = new HappyHutUserInfo()
                                {
                                    Username = strEmail,
                                    FirstName = quoteInfo.FirstName,
                                    LastName = quoteInfo.LastName,
                                    Email = quoteInfo.Email,
                                    MobileNumber = String.Empty,
                                    AddressLine1 = String.Empty,
                                    AddressLine2 = String.Empty,
                                    CityId = quoteInfo.CityId,
                                    IsFirstLogin = true,
                                };
                                lstHappyHutUsers.Add(inf);
                            }
                        }
                    }
                }

                if (lstHappyHutUsers != null && lstHappyHutUsers.Count > 0)
                {
                    client = new DatabaseClass();
                    client.SetHappyHutUsersInfos(lstHappyHutUsers, null);
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }

        private void GetEmailListFromDatabase()
        {
            try
            {
                ApplicationLog.Instance.WriteDebug("Calling database to get list to send emails");
                client = new DatabaseClass();
                lstCreateUser = new List<string>();
                lstQuotes = client.GetRequestsToSendEmail(null);

                foreach (GetQuoteInfo obj in lstQuotes)
                {
                    if (obj.NeedToCreateUser)
                    {
                        if (!lstCreateUser.Any(x => x.Equals(obj.Email)))
                            lstCreateUser.Add(obj.Email);
                    }

                    ApplicationLog.Instance.WriteInfo(String.Format("Sending request confiramtion Email to {0}", obj.Email));
                    EmailUtil utilObject = new EmailUtil()
                    {
                        ToEmail = obj.Email,
                        ToName = String.Format("{0} {1}", obj.FirstName, obj.LastName),
                        Subject = String.Format("Request confirmation for {0} service from HappyHut!!!", obj.ServiceName),
                    };
                    //TODO:: Add Body Template
                    bool emailSent = EmailUtil.Instance.SendEmail(utilObject);
                    if (emailSent)
                    {
                        ApplicationLog.Instance.WriteInfo(String.Format("Request confirmation Email sent to {0} for service request id {1}", obj.Email, obj.Id));
                        obj.IsEmailSent = true;
                        obj.EmailSentDt = DateTime.UtcNow;
                    }
                    else
                    {
                        ApplicationLog.Instance.WriteWarning(String.Format("Request email not sent to {0} for service request id '{1}'", obj.Email, obj.Id));
                        //TODO:: Send email to support@happyhut.in && pratik@happyhut.in
                    }
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }

        protected bool CreateUser(String strEmail)
        {
            bool bFlag = false;
            try
            {
                ApplicationLog.Instance.WriteInfo(String.Format("Creating user with email {0}", strEmail));
                AccountManager acm = new AccountManager();
                String strPassword = "Password@1234";
                if (ConfigurationManager.AppSettings.AllKeys.Contains("DefaultPassword"))
                    strPassword = ConfigurationManager.AppSettings["DefaultPassword"];
                bool bUserCreated = acm.CreateUser(strEmail, strEmail, strPassword);
                if (bUserCreated)
                {
                    bool bUserAddedToRole = acm.AddUserToRole(strEmail, "SERVICEREQUESTUSER");
                    //TODO:: Send email to support@happyhut.in && pratik@happyhut.in && to user
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return bFlag;
        }

        protected override void OnStop()
        {
            try
            {
                ApplicationLog.Instance.WriteWarning("Email Windows Service stopped.");
                //TODO:: Send email to pratik@happyhut.in
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
        }
        #endregion
    }
}

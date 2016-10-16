using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailUtils
{
    class EmailUtilSection : ConfigurationSection
    {
        #region static objects
        private static ConfigurationProperty sendEmail = null;
        private static ConfigurationProperty emailFromUser = null;
        private static ConfigurationProperty gmailConfiguration = null;
        private static ConfigurationPropertyCollection s_properties = null;
        #endregion

        #region Constructor
        static EmailUtilSection()
        {
            sendEmail = new ConfigurationProperty("sendEmail", typeof(bool), true);
            emailFromUser = new ConfigurationProperty("emailFromUser", typeof(EmailFromUser), null, ConfigurationPropertyOptions.IsRequired);
            gmailConfiguration = new ConfigurationProperty("emailConfiguration", typeof(SMTPClientConfiguration));
            s_properties = new ConfigurationPropertyCollection() { sendEmail, emailFromUser, gmailConfiguration };
        }
        #endregion

        #region Properties
        [ConfigurationProperty("sendEmail")]
        public bool IsSendEmail
        {
            get { return (bool)base[sendEmail]; }
        }
        [ConfigurationProperty("emailFromUser", IsRequired = true)]
        public EmailFromUser EmailFromUserInfo
        {
            get { return (EmailFromUser)base[emailFromUser]; }
        }

        [ConfigurationProperty("emailConfiguration")]
        public SMTPClientConfiguration SMTPConfigInfo
        {
            get { return (SMTPClientConfiguration)base[gmailConfiguration]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }
        #endregion
    }

    class EmailFromUser : ConfigurationElement
    {
        #region static objects
        private static ConfigurationProperty strUserName = null;
        private static ConfigurationProperty strPassWord = null;
        private static ConfigurationProperty strFromDisplayName = null;
        private static ConfigurationPropertyCollection s_properties = null;
        #endregion

        #region Constructor
        static EmailFromUser()
        {
            strUserName = new ConfigurationProperty("userName", typeof(String), String.Empty, ConfigurationPropertyOptions.IsRequired);
            strPassWord = new ConfigurationProperty("passWord", typeof(String), String.Empty, ConfigurationPropertyOptions.IsRequired);
            strFromDisplayName = new ConfigurationProperty("fromDisplayName", typeof(String), String.Empty, ConfigurationPropertyOptions.IsRequired);
            s_properties = new ConfigurationPropertyCollection() { strUserName, strPassWord, strFromDisplayName };
        }
        #endregion

        #region Properties
        [ConfigurationProperty("userName", IsRequired = true)]
        public String Username
        {
            get { return (String)base[strUserName]; }
        }

        [ConfigurationProperty("passWord", IsRequired = true)]
        public String Password
        {
            get { return (String)base[strPassWord]; }
        }

        [ConfigurationProperty("fromDisplayName", IsRequired = true)]
        public String FromDisplayName
        {
            get { return (String)base[strFromDisplayName]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }
        #endregion
    }

    class SMTPClientConfiguration : ConfigurationElement
    {
        #region static objects
        private static ConfigurationProperty host = null;
        private static ConfigurationProperty port = null;
        private static ConfigurationProperty enableSSL = null;
        private static ConfigurationProperty includeAttachment = null;

        private static ConfigurationPropertyCollection s_properties = null;
        #endregion

        #region Constructor
        static SMTPClientConfiguration()
        {
            host = new ConfigurationProperty("host", typeof(String), "smtp.gmail.com", ConfigurationPropertyOptions.IsRequired);
            port = new ConfigurationProperty("port", typeof(int), 587, ConfigurationPropertyOptions.IsRequired);
            enableSSL = new ConfigurationProperty("enableSSL", typeof(bool), true, ConfigurationPropertyOptions.IsRequired);
            includeAttachment = new ConfigurationProperty("includeAttachment", typeof(bool), false, ConfigurationPropertyOptions.IsRequired);
            s_properties = new ConfigurationPropertyCollection() { host, port, enableSSL, includeAttachment };
        }
        #endregion

        #region Properties
        [ConfigurationProperty("host", IsRequired = true, DefaultValue = "smtp.gmail.com")]
        public String Host
        {
            get { return (String)base[host]; }
        }

        [ConfigurationProperty("port", IsRequired = true, DefaultValue = 587)]
        public int Port
        {
            get { return (int)base[port]; }
        }

        [ConfigurationProperty("enableSSL", IsRequired = true, DefaultValue = true)]
        public bool EnableSSL
        {
            get { return (bool)base[enableSSL]; }
        }

        [ConfigurationProperty("includeAttachment", IsRequired = true, DefaultValue = false)]
        public bool IncludeAttachment
        {
            get { return (bool)base[includeAttachment]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }
        #endregion
    }
}

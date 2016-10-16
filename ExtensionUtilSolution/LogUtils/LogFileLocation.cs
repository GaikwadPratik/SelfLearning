using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtils
{
    class LogFileLocation : ConfigurationElement
    {
        #region static objects
        private static ConfigurationProperty strLocation = null;
        private static ConfigurationProperty strFileName = null;
        private static ConfigurationProperty strFileExtenstion = null;
        private static ConfigurationPropertyCollection s_properties = null;
        #endregion

        #region Constructor
        static LogFileLocation()
        {
            strLocation = new ConfigurationProperty("location", typeof(String), String.Empty, ConfigurationPropertyOptions.IsRequired);
            strFileName = new ConfigurationProperty("fileName", typeof(String), String.Empty, ConfigurationPropertyOptions.IsRequired);
            strFileExtenstion = new ConfigurationProperty("extension", typeof(String), String.Empty, ConfigurationPropertyOptions.IsRequired);
            s_properties = new ConfigurationPropertyCollection() { strLocation, strFileName, strFileExtenstion };

        }
        #endregion

        #region Properties
        [ConfigurationProperty("location", IsRequired = true)]
        public String LogFilePath
        {
            get { return (String)base[strLocation]; }
        }

        [ConfigurationProperty("fileName", IsRequired = true)]
        public String FileName
        {
            get { return (String)base[strFileName]; }
        }

        [ConfigurationProperty("extension", IsRequired = true)]
        public String Extension
        {
            get { return (String)base[strFileExtenstion]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtils
{
    class ApplicationLogSection : ConfigurationSection
    {
        #region static objects
        private static ConfigurationProperty bWriteLog = null;
        private static ConfigurationProperty fileLocation = null;
        private static ConfigurationProperty appendToFile = null;
        private static ConfigurationProperty maxFileSize = null;
        private static ConfigurationProperty debugLevel = null;
        private static ConfigurationPropertyCollection s_properties;
        #endregion

        #region Constructor
        static ApplicationLogSection()
        {
            bWriteLog = new ConfigurationProperty("writeLog", typeof(Boolean), false, ConfigurationPropertyOptions.IsRequired);
            fileLocation = new ConfigurationProperty("logFileLocation", typeof(LogFileLocation), null, ConfigurationPropertyOptions.IsRequired);
            appendToFile = new ConfigurationProperty("appendToFile", typeof(AppendLogLine), null, ConfigurationPropertyOptions.IsRequired);
            maxFileSize = new ConfigurationProperty("maxFileSize", typeof(MaximumFileSize), null, ConfigurationPropertyOptions.IsRequired);
            debugLevel = new ConfigurationProperty("debugLevel", typeof(DebugLevel), null, ConfigurationPropertyOptions.IsRequired);
            s_properties = new ConfigurationPropertyCollection();// { bWriteLog, fileLocation, appendToFile, maxFileSize, debugLevel };
            s_properties.Add(bWriteLog);
            s_properties.Add(fileLocation);
            s_properties.Add(appendToFile);
            s_properties.Add(maxFileSize);
            s_properties.Add(debugLevel);
        }
        #endregion

        #region Property
        [ConfigurationProperty("writeLog", IsRequired = true)]
        public Boolean WriteLog
        {
            get { return (Boolean)base[bWriteLog]; }
        }

        [ConfigurationProperty("logFileLocation", IsRequired = true)]
        public LogFileLocation FileLocation
        {
            get { return (LogFileLocation)base[fileLocation]; }
        }

        [ConfigurationProperty("appendToFile", IsRequired = true)]
        public AppendLogLine AppendLog
        {
            get { return (AppendLogLine)base[appendToFile]; }
        }

        [ConfigurationProperty("maxFileSize")]
        public MaximumFileSize MaxFileSize
        {
            get { return (MaximumFileSize)base[maxFileSize]; }
        }

        [ConfigurationProperty("debugLevel", IsRequired = true)]
        public DebugLevel DebugLevel
        {
            get { return (DebugLevel)base[debugLevel]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get
            {
                return s_properties;
            }
        }
        #endregion
    }
}

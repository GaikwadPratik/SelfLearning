using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtils
{
    class DebugLevel : ConfigurationElement
    {
        #region static objects
        private static ConfigurationProperty strDebugLevel = null;
        private static ConfigurationPropertyCollection s_properties = null;
        #endregion

        #region Constructor
        static DebugLevel()
        {
            strDebugLevel = new ConfigurationProperty("level", typeof(String), "DEBUG", ConfigurationPropertyOptions.IsRequired);
            s_properties = new ConfigurationPropertyCollection();
            s_properties.Add(strDebugLevel);
        }
        #endregion

        #region Properties
        [ConfigurationProperty("level")]
        public String Level
        {
            get { return (String)base[strDebugLevel]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }
        #endregion
    }
}

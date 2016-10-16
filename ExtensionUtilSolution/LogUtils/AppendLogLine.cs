using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtils
{
    class AppendLogLine : ConfigurationElement
    {
        #region static objects
        private static ConfigurationProperty bAppend = null;
        private static ConfigurationPropertyCollection s_properties = null;
        #endregion

        #region Constructor
        static AppendLogLine()
        {
            bAppend = new ConfigurationProperty("appendLine", typeof(Boolean), true, ConfigurationPropertyOptions.IsRequired);
            s_properties = new ConfigurationPropertyCollection();
            s_properties.Add(bAppend);
        }
        #endregion

        #region Properties
        [ConfigurationProperty("appendLine", IsRequired = true, DefaultValue = true)]
        public Boolean AppendLines
        {
            get { return (Boolean)base[bAppend]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }
        #endregion
    }
}

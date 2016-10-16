using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogUtils
{
    class MaximumFileSize : ConfigurationElement
    {
        #region static objects
        private static ConfigurationProperty nFileSize = null;
        private static ConfigurationPropertyCollection s_properties = null;
        #endregion

        #region Constructor
        static MaximumFileSize()
        {
            nFileSize = new ConfigurationProperty("size", typeof(int), 10, ConfigurationPropertyOptions.IsRequired);
            s_properties = new ConfigurationPropertyCollection();
            s_properties.Add(nFileSize);
        }
        #endregion

        #region Properties
        [ConfigurationProperty("size")]
        public int FileSize
        {
            get { return (int)base[nFileSize]; }
        }

        protected override ConfigurationPropertyCollection Properties
        {
            get { return s_properties; }
        }
        #endregion
    }
}

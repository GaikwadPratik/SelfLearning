using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LogUtils
{
    public class ApplicationLog
    {
        #region Objects
        private static readonly ApplicationLog instance = new ApplicationLog();

        private ApplicationLogSection logSection = null;
        private bool bWriteLog = false;
        private bool bAppendLine = true;
        private String strFileName = "LogFile_0.txt";
        private String strDirectoryPath = @"c:\ApplicationLog";
        private String strCompletePath = String.Empty;
        private long lMaxFileSize = 1;
        LogFileLocation fileLocation = null;
        AppendLogLine appendToFile = null;
        MaximumFileSize maxSize = null;
        DebugLevel debugLevel = null;
        FileInfo fileInfo = null;
        DirectoryInfo directoryInfo = null;
        StackTrace stackTrace = null;

        private bool bIsDebugEnabled = false;
        private bool bIsInfoEnabled = false;
        private bool bIsWarningEnabled = false;

        #endregion

        #region Constructor
        static ApplicationLog()
        {

        }

        private ApplicationLog()
        {
            logSection = ConfigurationManager.GetSection("ApplicationCustomLog") as ApplicationLogSection;
            if (logSection != null)
            {
                bWriteLog = logSection.WriteLog;
                fileLocation = logSection.FileLocation;
                if (fileLocation != null)
                {
                    strDirectoryPath = fileLocation.LogFilePath;
                    strFileName = String.Format("{0}.{1}", fileLocation.FileName, fileLocation.Extension);
                }
                directoryInfo = new DirectoryInfo(strDirectoryPath);
                if (!IsDirectoryExists)
                    CreateDirectory(strDirectoryPath);
                strCompletePath = String.Format(@"{0}\{1}", strDirectoryPath, strFileName);
                fileInfo = new FileInfo(strCompletePath);
                appendToFile = logSection.AppendLog;
                if (appendToFile != null)
                    bAppendLine = appendToFile.AppendLines;
                maxSize = logSection.MaxFileSize;
                if (maxSize != null)
                    lMaxFileSize = (long)maxSize.FileSize;
                debugLevel = logSection.DebugLevel;
            }
        }
        #endregion

        #region Properties
        private bool IsMaxFileReached
        {
            get
            {
                bool bFlag = false;
                FileInfo f1 = new FileInfo(strCompletePath);
                bFlag = IsFileExists ? (f1.Length / 1024) / 1024 >= lMaxFileSize : false;
                return bFlag;
            }
        }

        private bool IsDirectoryExists
        {
            get
            {
                return directoryInfo.Exists;
            }
        }

        private bool IsFileExists
        {
            get
            {
                return fileInfo.Exists;
            }
        }

        public static ApplicationLog Instance
        {
            get
            {
                return instance;
            }
        }

        public bool IsDebugEnabled
        {
            get
            {
                if (debugLevel != null
                    && debugLevel.Level.Equals("DEBUG", StringComparison.OrdinalIgnoreCase))
                    bIsDebugEnabled = true;
                return bIsDebugEnabled;
            }
            set
            {
                bIsDebugEnabled = value;
            }
        }

        public bool IsInfoEnabled
        {
            get
            {
                if (debugLevel != null
                    && (debugLevel.Level.Equals("INFO", StringComparison.OrdinalIgnoreCase)
                    || IsDebugEnabled))
                    bIsInfoEnabled = true;
                return bIsInfoEnabled;
            }
            set
            {
                bIsInfoEnabled = value;
            }
        }

        public bool IsWarningEnabled
        {
            get
            {
                if (debugLevel != null
                    && (debugLevel.Level.Equals("WARNING", StringComparison.OrdinalIgnoreCase)
                    || IsDebugEnabled
                    || IsInfoEnabled))
                    bIsWarningEnabled = true;
                return bIsWarningEnabled;
            }
            set
            {
                bIsWarningEnabled = value;
            }
        }

        public bool IsErrorEnabled
        {
            get
            {
                if (debugLevel != null
                    && (debugLevel.Level.Equals("ERROR", StringComparison.OrdinalIgnoreCase)
                    || IsWarningEnabled
                    || IsDebugEnabled
                    || IsInfoEnabled))
                    bIsWarningEnabled = true;
                return bIsWarningEnabled;
            }
            set
            {
                bIsWarningEnabled = value;
            }
        }
        #endregion

        #region Member functions
        private bool CreateFile()
        {
            bool bFlag = false;
            int nCount = directoryInfo.GetFiles().Count();
            if (nCount > 0 && IsMaxFileReached)
                fileInfo.MoveTo(String.Format(@"{0}\{1}.{2}", strDirectoryPath, String.Format("{0}{1}", fileLocation.FileName, nCount), fileLocation.Extension));

            fileInfo = new FileInfo(strCompletePath);
            FileStream fStream = fileInfo.Create();
            bFlag = fStream != null;
            if (bFlag)
                fStream.SetLength(lMaxFileSize);
            fStream.Dispose();
            return bFlag;
        }

        private bool CreateDirectory(String strDirectoryPath)
        {
            bool bFlag = false;
            directoryInfo = new DirectoryInfo(strDirectoryPath);
            directoryInfo.Create();
            bFlag = IsDirectoryExists;
            return bFlag;
        }

        private String GetCallingMethodName()
        {
            String strMethodName = String.Empty;
            stackTrace = new StackTrace();
            if (stackTrace != null)
            {
                StackFrame frame = stackTrace.GetFrame(2);
                if (frame != null)
                {
                    strMethodName = string.Format("{0}.{1}", frame.GetMethod().DeclaringType, frame.GetMethod().Name);
                }
            }
            return strMethodName;
        }

        public void WriteException(Exception ex)
        {
            StreamWriter sw = null;
            try
            {
                if (!IsFileExists || IsMaxFileReached)
                    CreateFile();

                sw = fileInfo.AppendText();

                sw.WriteLine(String.Format("EXCEPTION at {0} from [{2}] : {1}", DateTime.Now, ex.ToString(), GetCallingMethodName()));
                sw.Flush();
                sw.Close();
            }
            finally
            {
                if (sw != null)
                    sw.Dispose();
            }
        }

        public void WriteInfo(String strMessage)
        {
            StreamWriter sw = null;
            try
            {
                if (IsInfoEnabled && bWriteLog)
                {
                    if (!IsFileExists || IsMaxFileReached)
                        CreateFile();

                    sw = fileInfo.AppendText();
                    sw.WriteLine(String.Format("INFO at {0} from [{2}] : {1}", DateTime.Now, strMessage, GetCallingMethodName()));
                    sw.Flush();
                    sw.Close();
                }
            }
            finally
            {
                if (sw != null)
                    sw.Dispose();
            }
        }

        public void WriteDebug(String strMessage)
        {
            StreamWriter sw = null;
            try
            {
                if (IsDebugEnabled && bWriteLog)
                {
                    if (!IsFileExists || IsMaxFileReached)
                        CreateFile();

                    sw = fileInfo.AppendText();
                    sw.WriteLine(String.Format("DEBUG at {0} from [{2}] : {1}", DateTime.Now, strMessage, GetCallingMethodName()));
                    sw.Flush();
                    sw.Close();
                }
            }
            finally
            {
                if (sw != null)
                    sw.Dispose();
            }
        }

        public void WriteWarning(String strMessage)
        {
            StreamWriter sw = null;
            try
            {
                if (IsWarningEnabled && bWriteLog)
                {
                    if (!IsFileExists || IsMaxFileReached)
                        CreateFile();

                    sw = fileInfo.AppendText();
                    sw.WriteLine(String.Format("WARNING at {0} from [{2}] : {1}", DateTime.Now, strMessage, GetCallingMethodName()));
                    sw.Flush();
                    sw.Close();
                }
            }
            finally
            {
                if (sw != null)
                    sw.Dispose();
            }
        }

        public void WriteError(String strMessage)
        {
            StreamWriter sw = null;
            try
            {
                if (IsErrorEnabled && bWriteLog)
                {
                    if (!IsFileExists || IsMaxFileReached)
                        CreateFile();

                    sw = fileInfo.AppendText();
                    sw.WriteLine(String.Format("ERROR at {0} from [{2}] : {1}", DateTime.Now, strMessage, GetCallingMethodName()));
                    sw.Flush();
                    sw.Close();
                }
            }
            finally
            {
                if (sw != null)
                    sw.Dispose();
            }
        }
        #endregion
    }
}

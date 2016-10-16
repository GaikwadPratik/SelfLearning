using LogUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SerializationExtensionUtils
{
    public class SerializationHelper
    {
        /// <summary>
        /// Serializes an object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializableObject"></param>
        /// <param name="fileName"></param>
        public static void WriteXML<T>(T serializableObject, string fileName)
        {
            if (serializableObject == null) { return; }
            StreamWriter writer = null;
            try
            {
                XmlDocument xmlDocument = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(serializableObject.GetType());
                writer = new System.IO.StreamWriter(fileName);
                serializer.Serialize(writer, serializableObject);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (writer != null)
                    writer.Dispose();
            }
        }

        public static T LoadXML<T>(String strFilePath) where T : new()
        {
            FileStream stream = null;
            try
            {
                // Deserialize
                XmlSerializer ser = new XmlSerializer(typeof(T));
                using (stream = new FileStream(strFilePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    T t = (T)ser.Deserialize(stream);
                    if (t == null) throw new NullReferenceException("Invalid xml format");
                    return t;
                }
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }

            return new T();
        }

        /// <summary>
        /// Method to convert a custom Object to XML string
        /// </summary>
        /// <param name="pObject">Object that is to be serialized to XML</param>
        /// <returns>XML string</returns>
        public static String SerializeObject(Object pObject)
        {
            String strRtnVal = String.Empty;
            try
            {
                String XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(pObject.GetType());
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = xmlTextWriter.BaseStream as MemoryStream;
                XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray());
                strRtnVal = XmlizedString;
            }
            catch (Exception e)
            {
                ApplicationLog.Instance.WriteException(e);
            }
            return strRtnVal;
        }

        /// <summary>
        /// Method to reconstruct an Object from XML string
        /// </summary>
        /// <param name="pXmlizedString"></param>
        /// <returns></returns>
        public static T DeserializeObject<T>(String pXmlizedString) where T : new()
        {
            T rtnVal = new T();
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                MemoryStream memoryStream = new MemoryStream(StringToUTF8ByteArray(pXmlizedString));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);

                rtnVal = (T)xs.Deserialize(memoryStream);
            }
            catch (Exception ex)
            {
                ApplicationLog.Instance.WriteException(ex);
            }
            return rtnVal;
        }

        /// <summary>
        /// Converts the String to UTF8 Byte array and is used in De serialization
        /// </summary>
        /// <param name="pXmlString"></param>
        /// <returns></returns>
        private static Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        /// <summary>
        /// To convert a Byte Array of Unicode values (UTF-8 encoded) to a complete String.
        /// </summary>
        /// <param name="characters">Unicode Byte Array to be converted to String</param>
        /// <returns>String converted from Unicode Byte Array</returns>
        private static String UTF8ByteArrayToString(Byte[] characters)
        {
            String strRtnVal = String.Empty;
            try
            {
                UTF8Encoding encoding = new UTF8Encoding();
                String constructedString = encoding.GetString(characters);
                strRtnVal = constructedString;
            }
            catch (Exception e)
            {
                ApplicationLog.Instance.WriteException(e);
            }
            return strRtnVal;
        }
    }
}

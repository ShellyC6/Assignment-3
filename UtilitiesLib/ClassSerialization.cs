using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;//FileStream, Reader/Writer classes
using System.Runtime.Serialization; //IFormatter
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace UtilitiesLib
{
    public class ClassSerialization
    {
        public static string BinaryFileSerialize<T>(string filePath, T obj)
        {
            FileStream fileStream = null;
            string errorMsg = null;
            try
            {
                fileStream = new FileStream(filePath, FileMode.Create);
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(fileStream, obj);

            }
            catch (Exception e)
            {
                errorMsg = e.Message;
            }
            finally
            {
                if (fileStream != null)
                    fileStream.Close();
            }
            return errorMsg;
        }

        

        /*public static T FromXML<T>(string xml)
        {
            using (StringReader stringReader = new StringReader(xml))
            {
                XmlSerializer serializer = new XmlSerializer(Typeof(T));
                return (T)serializer.Deserialize(stringReader);
            }
        }*/

        public static void ToXML<T>(T obj, string filePath)
        {
            /*
            string errorM = null;
            try
            {
                using (StringWriter stringWriter = new StringWriter(filePath))//
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(Typeof(T));
                    xmlSerializer.Serialize(stringWriter, obj);
                    stringWriter.Close();
                }
                
            }
            catch(Exception e)
            {
                //errorM = e.Message;
            }*/

        }
    }
}

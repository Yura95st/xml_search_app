using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using xml_search_app.Models;

namespace xml_search_app.ExportEngines
{
    public class XmlExportEngine : IExportEngine
    {
        private string _path = "";

        public void Export(List<BookItem> resultsList)
        {
            var xmlDoc = new XmlDocument();
            xmlDoc.AppendChild(xmlDoc.CreateXmlDeclaration("1.0", "utf-8", null));

            XmlElement root = xmlDoc.CreateElement("address_book");
            xmlDoc.AppendChild(root);

            XmlElement item;
            XmlElement element;            
            try
            {
                foreach (var bookItem in resultsList)
                {                    
                    item = xmlDoc.CreateElement("item");

                    element = xmlDoc.CreateElement("full_name");
                    element.InnerText = bookItem.Name.LastName + " " + bookItem.Name.FirstName + ". " + bookItem.Name.MiddleName + ".";
                    item.AppendChild(element);

                    element = xmlDoc.CreateElement("address");
                    element.InnerText = bookItem.Address.City + ", " + bookItem.Address.House + " " + bookItem.Address.Street + ", apt. "
                        + bookItem.Address.Apartment;
                    item.AppendChild(element);

                    element = xmlDoc.CreateElement("phone_number");
                    element.InnerText = bookItem.PhoneNumber.ToString();
                    item.AppendChild(element);

                    root.AppendChild(item);
                }

                xmlDoc.Save(_path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SetPathToExport(string path)
        {
            _path = Path.Combine(path, ProgramValues.EXPORT_ENGINE_FILE_NAME + ".xml");
        }

        //private void ToXML<T>(T obj)
        //{
        //    using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
        //    {
        //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        //        xmlSerializer.Serialize(stringWriter, obj);
        //        File.WriteAllText(_path, stringWriter.ToString());
        //    }
        //}
    }
}

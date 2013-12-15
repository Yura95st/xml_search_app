using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using xml_search_app.XmlParsers;
using xml_search_app.Models;

namespace xml_search_app.XmlParsers
{
    class DomXmlParser: IXmlParser
    {
        private string _file = "";
        private int _searchType = 0; //search by: 0 - address, 1 - city, 2 - last name, 3 - phone number

        public DomXmlParser()
        { }

        public void SetResourseFile(string file)
        {
            _file = file;
        }

        public void SetSearchType(int type)
        {
            _searchType = type;
        }

        public List<BookItem> SearchInFile(string query)
        {
            XmlDocument xmlDoc = new XmlDocument();
            try
            {
                xmlDoc.Load(_file);
            }
            catch (Exception e)
            {
                throw e;
            }

            XmlNode root = xmlDoc.DocumentElement;

            List<BookItem> bookItemList = new List<BookItem>();

            foreach (XmlNode item in root.ChildNodes)
            {
                if (item.NodeType == XmlNodeType.Element)
                {
                    try
                    {
                        BookItem bookItem = new BookItem();
                        bookItem.PhoneNumber = Convert.ToInt32(item["phone_number"].InnerText);

                        XmlNode node = item["name"];

                        Name name = new Name();
                        name.FirstName = node["first_name"].InnerText;
                        name.LastName = node["last_name"].InnerText;
                        name.MiddleName = node["middle_name"].InnerText;
                        bookItem.Name = name;

                        node = item["address"];

                        Address address = new Address();
                        address.City = node["city"].InnerText;
                        address.Street = node["street"].InnerText;
                        address.House = node["house"].InnerText;
                        address.Apartment = Convert.ToInt32(node["apartment"].InnerText);
                        bookItem.Address = address;

                        if (SearchResultContainsItem(bookItem, query))
                        {
                            bookItemList.Add(bookItem);
                        }
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }

            return bookItemList;
        }

        private bool SearchResultContainsItem(BookItem bookItem, string query)
        {
            switch (_searchType)
            {
                case 0:
                    return ((bookItem.Address.House + " " + bookItem.Address.Street + ", " + bookItem.Address.Apartment)
                        .IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);

                case 1:
                    return (bookItem.Address.City.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);

                case 2:
                    return (bookItem.Name.LastName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);

                case 3:
                    return (Convert.ToString(bookItem.PhoneNumber).IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);

                default:
                    return true;
            }
        }


        //public List<BookItem> ParseFile()
        //{
        //    XmlDocument xmlDoc = new XmlDocument();
        //    try
        //    {
        //        xmlDoc.Load(_file);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }

        //    XmlNode root = xmlDoc.DocumentElement;

        //    List<BookItem> bookItemList = new List<BookItem>();

        //    foreach (XmlNode item in root.ChildNodes)
        //    {
        //        if (item.NodeType == XmlNodeType.Element)
        //        {
        //            try
        //            {
        //                BookItem bookItem = new BookItem();
        //                bookItem.PhoneNumber = Convert.ToInt32(item.Attributes.GetNamedItem("number").Value);

        //                Name name = new Name();
        //                name.FirstName = item.Attributes.GetNamedItem("firstname").Value.ToString();
        //                name.LastName = item.Attributes.GetNamedItem("lastname").Value.ToString();
        //                name.MiddleName = item.Attributes.GetNamedItem("middlename").Value.ToString();
        //                bookItem.Name = name;

        //                Address address = new Address();
        //                address.City = "Киев";
        //                address.Street = item.Attributes.GetNamedItem("street").Value.ToString();
        //                address.House = item.Attributes.GetNamedItem("buildingn").Value.ToString();
        //                address.Apartment = Convert.ToInt32(item.Attributes.GetNamedItem("apartn").Value);
        //                bookItem.Address = address;

        //                bookItemList.Add(bookItem);
        //            }
        //            catch (Exception e)
        //            {
        //                throw e;
        //            }
        //        }
        //    }

        //    ToXML(bookItemList);

        //    return bookItemList;
        //}

        //private void ToXML<T>(T obj)
        //{
        //    using (StringWriter stringWriter = new StringWriter(new StringBuilder()))
        //    {
        //        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
        //        xmlSerializer.Serialize(stringWriter, obj);
        //        File.WriteAllText(_file, stringWriter.ToString());
        //    }
        //}
    }
}

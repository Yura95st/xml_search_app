using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

using xml_search_app.Libs;
using xml_search_app.Models;

namespace xml_search_app.XmlParsers
{
    class SaxXmlParser : IXmlParser
    {
        private string _file = "";

        public SaxXmlParser()
        { }

        public void SetResourseFile(string file)
        {
            _file = file;
        }

        public List<BookItem> ParseFile()
        {
            XmlReader xmlReader;
            try
            {
                xmlReader = new XmlTextReader(_file);
            }
            catch (Exception e)
            {
                throw e;
            }

            List<BookItem> bookItemList = new List<BookItem>();
            try
            {
                xmlReader.ReadToFollowing("address_book");
                while (xmlReader.Read())
                {
                    if (xmlReader.Name.Equals("item") && xmlReader.NodeType == XmlNodeType.Element)
                    {
                        bookItemList.Add(ParseItemNode(xmlReader.ReadSubtree()));
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                xmlReader.Close();
            }

            return bookItemList;
        }

        private BookItem ParseItemNode(XmlReader xmlReader)
        {
            BookItem bookItem = new BookItem();

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    switch (xmlReader.Name)
                    {
                        case "phone_number":
                            bookItem.PhoneNumber = xmlReader.ReadElementContentAsInt();
                            break;

                        case "name":
                            bookItem.Name = ParseNameNode(xmlReader.ReadSubtree());
                            break;

                        case "address":
                            bookItem.Address = ParseAdressNode(xmlReader.ReadSubtree());
                            break;
                    }
                }

                else if (xmlReader.Name.Equals("item") && xmlReader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }
            }

            return bookItem;
        }

        private Address ParseAdressNode(XmlReader xmlReader)
        {
            Address address = new Address();

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    switch (xmlReader.Name)
                    {
                        case "city":
                            address.City = xmlReader.ReadElementContentAsString();
                            break;

                        case "street":
                            address.Street = xmlReader.ReadElementContentAsString();
                            break;

                        case "house":
                            address.House = xmlReader.ReadElementContentAsString();
                            break;
                        case "apartment":
                            address.Apartment = xmlReader.ReadElementContentAsInt();
                            break;
                    }
                }
                else if (xmlReader.Name.Equals("address") && xmlReader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }
            }

            return address;
        }

        private Name ParseNameNode(XmlReader xmlReader)
        {
            Name name = new Name();

            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element)
                {
                    switch (xmlReader.Name)
                    {
                        case "first_name":
                            name.FirstName = xmlReader.ReadElementContentAsString();
                            break;

                        case "last_name":
                            name.LastName = xmlReader.ReadElementContentAsString();
                            break;

                        case "middle_name":
                            name.MiddleName = xmlReader.ReadElementContentAsString();
                            break;
                    }
                }
                else if (xmlReader.Name.Equals("name") && xmlReader.NodeType == XmlNodeType.EndElement)
                {
                    break;
                }
            }

            return name;
        }

        public List<BookItem> SearchInFile(string query, int type)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;

using xml_search_app.XmlParsers;
using xml_search_app.Models;

namespace xml_search_app.XmlParsers
{
    class SaxXmlParser : IXmlParser
    {
        private string _file = "";
        private int _searchType = 0; //search by: 0 - address, 1 - city, 2 - last name, 3 - phone number

        public SaxXmlParser()
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
                        BookItem bookItem = ParseItemNode(xmlReader.ReadSubtree());

                        if (SearchResultContainsItem(bookItem, query))
                        {
                            bookItemList.Add(bookItem);
                        }
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
    }
}

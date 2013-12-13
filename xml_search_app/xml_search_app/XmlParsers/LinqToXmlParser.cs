using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using xml_search_app.Libs;
using xml_search_app.Models;

namespace xml_search_app.XmlParsers
{
    class LinqToXmlParser : IXmlParser
    {
        private string _file = "";
        private int _searchType = 0; //search by: 0 - address, 1 - city, 2 - last name, 3 - phone number

        public LinqToXmlParser()
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
            XDocument xdoc = XDocument.Load(_file);
            List<BookItem> bookItemList = new List<BookItem>();

            try
            {
                var items = from item in xdoc.Descendants("item")
                            select new
                             {
                                 FirstName = item.Element("name").Element("first_name").Value,
                                 LastName = item.Element("name").Element("last_name").Value,
                                 MiddleName = item.Element("name").Element("middle_name").Value,
                                 City = item.Element("address").Element("city").Value,
                                 Street = item.Element("address").Element("street").Value,
                                 House = item.Element("address").Element("house").Value,
                                 Apartment = item.Element("address").Element("apartment").Value,
                                 PhoneNumber = item.Element("phone_number").Value,
                             };

                switch (_searchType)
                {
                    case 0:
                        items = items.Where(x =>
                            (x.House + " " + x.Street + ", " + x.Apartment).IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;

                    case 1:
                        items = items.Where(x => x.City.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;

                    case 2:
                        items = items.Where(x => 
                            x.LastName.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;

                    case 3:
                        items = items.Where(x => x.PhoneNumber.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0);
                        break;
                }

                Address address;
                Name name;
                foreach (var item in items)
                {
                    name = new Name(item.FirstName, item.LastName, item.MiddleName);
                    address = new Address(item.City, item.Street, item.House, Convert.ToInt32(item.Apartment));

                    bookItemList.Add(new BookItem(name, address, Convert.ToInt32(item.PhoneNumber)));
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return bookItemList;
        }
    }
}

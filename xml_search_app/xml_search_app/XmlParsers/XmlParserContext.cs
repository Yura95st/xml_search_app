using System;
using System.Collections.Generic;

using xml_search_app.Models;
using xml_search_app.Libs;

namespace xml_search_app.XmlParsers
{
    public class XmlParserContext : IXmlParser
    {
        private IXmlParser parser;

        public XmlParserContext()
        { }

        //parserId: 0 - Sax, 1 - Dom, 2 - LinqToXml
        public void SetParser(int parserId)
        {
            switch (parserId)
            {
                case 0:
                    {
                        parser = new SaxXmlParser();
                    }
                    break;

                case 1:
                    {
                        parser = new DomXmlParser();
                    }
                    break;

                case 2:
                    {
                        parser = new LinqToXmlParser();
                    }
                    break;

                default:
                    throw new Exception("Unknown parser's name");
            }
        }

        public void SetResourseFile(string file)
        {
            parser.SetResourseFile(file);
        }

        public List<BookItem> SearchInFile(string query)
        {
            return parser.SearchInFile(query);
        }

        public void SetSearchType(int type)
        {
            parser.SetSearchType(type);
        }
    }
}

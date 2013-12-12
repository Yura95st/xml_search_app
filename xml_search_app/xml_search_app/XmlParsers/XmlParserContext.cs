using System;

using xml_search_app.Models;
using xml_search_app.Libs;

namespace xml_search_app.XmlParsers
{
    public class XmlParserContext : IXmlParser
    {
        private IXmlParser parser;

        public XmlParserContext()
        { }

        public void SetParser(string name)
        {
            switch (name)
            {
                case "DOM":
                    {
                        parser = new DomXmlParser();
                    }
                    break;

                case "SAX":
                    {
                        parser = new SaxXmlParser();
                    }
                    break;

                case "LinqToXml":
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

        public System.Collections.Generic.List<BookItem> ParseFile()
        {
            return parser.ParseFile();
        }

        public System.Collections.Generic.List<BookItem> SearchInFile(string query, int type)
        {
            return parser.SearchInFile(query, type);
        }
    }
}

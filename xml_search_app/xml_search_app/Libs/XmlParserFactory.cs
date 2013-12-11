using System;
using xml_search_app.Models;

namespace xml_search_app.Libs
{
    public class XmlParserFactory
    {
        public static IXmlParser GetParser(string name)
        {
            IXmlParser parser;

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

            return parser;
        }
    }
}

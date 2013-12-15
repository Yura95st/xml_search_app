using System;
using System.Collections.Generic;
using System.IO;

using xml_search_app.Models;

namespace xml_search_app.XmlParsers
{
    public class XmlParserContext
    {
        private string _resourceFile = "";
        private IXmlParser _parser;

        public XmlParserContext()
        {
        }

        //parserId: 0 - Sax, 1 - Dom, 2 - LinqToXml
        public void SetParser(int parserId)
        {
            switch (parserId)
            {
                case 0:
                    {
                        _parser = new SaxXmlParser();
                    }
                    break;

                case 1:
                    {
                        _parser = new DomXmlParser();
                    }
                    break;

                case 2:
                    {
                        _parser = new LinqToXmlParser();
                    }
                    break;

                default:
                    throw new Exception("Unknown parser's name");
            }

            if (_parser != null)
            {
                _parser.SetResourseFile(_resourceFile);
            }
        }

        public string ResourseFile
        {
            set
            {
                _resourceFile = value;
            }
        }

        public List<BookItem> SearchInFile(string query)
        {
            return _parser.SearchInFile(query);
        }

        public void SetSearchType(int type)
        {
            _parser.SetSearchType(type);
        }
    }
}

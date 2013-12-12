using System;
using System.Collections.Generic;
using System.IO;
using xml_search_app.XmlParsers;

namespace xml_search_app.Models
{
    public class MainModel
    {
        private string _resourceFile = "";
        private List<BookItem> _itemsList = new List<BookItem>();

        public MainModel()
        {
        }

        public string ResourceFile
        {
            set
            {
                _resourceFile = Path.Combine(Environment.CurrentDirectory, value);
            }
        }

        public List<BookItem> ItemsList
        {
            get
            {
                return _itemsList;
            }
        }

        public void ParseFile(string parserName)
        {
            try
            {
                XmlParserContext parserContext = new XmlParserContext();
                parserContext.SetParser(parserName);
                parserContext.SetResourseFile(_resourceFile);
                _itemsList = parserContext.ParseFile();
            }
            catch (Exception e)
            {
            }
        }
    }
}

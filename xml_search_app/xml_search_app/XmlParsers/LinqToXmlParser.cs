using System;
using System.Collections.Generic;
using xml_search_app.Libs;
using xml_search_app.Models;

namespace xml_search_app.XmlParsers
{
    class LinqToXmlParser : IXmlParser
    {
        private string _file = "";

        public LinqToXmlParser()
        { }

        public void SetResourseFile(string file)
        {
            _file = file;
        }

        public List<BookItem> ParseFile()
        {
            throw new NotImplementedException();
        }

        public List<BookItem> SearchInFile(string query, int type)
        {
            throw new NotImplementedException();
        }
    }
}

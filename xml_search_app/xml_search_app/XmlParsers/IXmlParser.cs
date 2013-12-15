using System;
using System.Collections.Generic;
using xml_search_app.Models;

namespace xml_search_app.XmlParsers
{
    public interface IXmlParser
    {
        void SetResourseFile(string file);

        List<BookItem> SearchInFile(string query);

        void SetSearchType(int type);
    }
}

using System;
using System.Collections.Generic;
using xml_search_app.Models;

namespace xml_search_app.Libs
{
    public interface IXmlParser
    {
        void SetResourseFile(string file);

        List<BookItem> ParseFile();

        List<BookItem> SearchInFile(string query, int type);
    }
}

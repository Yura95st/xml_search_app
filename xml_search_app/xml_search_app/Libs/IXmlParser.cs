using System;
using System.Collections.Generic;
using xml_search_app.Models;

namespace xml_search_app.Libs
{
    public interface IXmlParser
    {
        void SetResourseFile(string file);

        List<BookItem> SearchInFile(string query);

        void SetSearchType(int type);
    }
}

using System;
using System.Collections.Generic;

using xml_search_app.Models;

namespace xml_search_app.ExportEngines
{
    public interface IExportEngine
    {
        void Export(List<BookItem> resultsList);

        void SetPathToExport(string path);
    }
}

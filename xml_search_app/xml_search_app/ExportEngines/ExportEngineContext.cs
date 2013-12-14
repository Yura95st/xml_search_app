using System;
using System.Collections.Generic;

using xml_search_app.Models;

namespace xml_search_app.ExportEngines
{
    public class ExportEngineContext
    {
        private IExportEngine _exportEngine;
        private string _pathToExport = "";

        public ExportEngineContext()
        { }

        //engineId: 0 - Txt, 1 - Xml, 2 - Html
        public void SetExportEngine (int engineId)
        {
            switch (engineId)
            {
                case 0:
                    {
                        _exportEngine = new TxtExportEngine();
                    }
                    break;

                case 1:
                    {
                        _exportEngine = new XmlExportEngine();
                    }
                    break;

                case 2:
                    {
                        _exportEngine = new HtmlExportEngine();
                    }
                    break;

                default:
                    throw new Exception("Unknown export engine's name");
            }

            if (_exportEngine != null)
            {
                _exportEngine.SetPathToExport(_pathToExport);
            }
        }

        public string PathToExport
        {
            set
            {
                _pathToExport = value;
            }
        }

        public void Export(List<BookItem> resultsList)
        {
            _exportEngine.Export(resultsList);
        }

        public void SetPathToExport(string path)
        {
            _exportEngine.SetPathToExport(path);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Xsl;
using xml_search_app.Models;

namespace xml_search_app.ExportEngines
{
    public class HtmlExportEngine : IExportEngine
    {
        private string _path = "";
        private IExportEngine _xmlExportEngine = new XmlExportEngine();
        
        private string _xmlFilePath = Path.Combine(Environment.CurrentDirectory, ProgramValues.RESOURCE_DIRECTORY_NAME, 
                ProgramValues.RESULTS_DIRECTORY_NAME, ProgramValues.EXPORT_ENGINE_FILE_NAME + ".xml");

        private string _xslTransformFilePath = Path.Combine(Environment.CurrentDirectory,
                ProgramValues.RESOURCE_DIRECTORY_NAME, ProgramValues.XLS_TRANSFORM_FILE_NAME);

        public HtmlExportEngine()
        {
            _xmlExportEngine.SetPathToExport(Path.Combine(Environment.CurrentDirectory, 
                ProgramValues.RESOURCE_DIRECTORY_NAME, ProgramValues.RESULTS_DIRECTORY_NAME));
        }

        public void Export(List<BookItem> resultsList)
        {
            try
            {
                //Generate .xml results file
                _xmlExportEngine.Export(resultsList);

                XslCompiledTransform xslt = new XslCompiledTransform();
                xslt.Load(_xslTransformFilePath);
                xslt.Transform(_xmlFilePath, _path);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void SetPathToExport(string path)
        {
            _path = Path.Combine(path, ProgramValues.EXPORT_ENGINE_FILE_NAME + ".html");
        }
    }
}

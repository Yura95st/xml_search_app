using System;
using System.Collections.Generic;
using System.IO;

using xml_search_app.XmlParsers;
using xml_search_app.ExportEngines;

namespace xml_search_app.Models
{
    public class MainModel
    {        
        private List<BookItem> _itemsList = new List<BookItem>();
        private XmlParserContext _xmlParser = new XmlParserContext();
        private int _parserId;
        private int _searchType;
        private ExportEngineContext _exportEngine = new ExportEngineContext();
        private int _exportEngineId;

        public MainModel()
        {
            _xmlParser.ResourseFile = Path.Combine(Environment.CurrentDirectory, 
                ProgramValues.RESOURCE_DIRECTORY_NAME, ProgramValues.RESOURCE_FILE_NAME);

            _exportEngine.PathToExport = Path.Combine(Environment.CurrentDirectory,
                ProgramValues.RESOURCE_DIRECTORY_NAME, ProgramValues.RESULTS_DIRECTORY_NAME);
        }

        public List<BookItem> ItemsList
        {
            get
            {
                return _itemsList;
            }
        }

        public int ParserId
        {
            get
            {
                return _parserId;
            }
            set
            {
                _parserId = value;
                _xmlParser.SetParser(_parserId);
            }
        }

        public int SearchType
        {
            get
            {
                return _searchType;
            }
            set
            {
                _searchType = value;
            }
        }

        public void Search(string query)
        {
            if (query.Equals(""))
            {
                return;
            }

            try
            {
                _xmlParser.SetSearchType(_searchType);
                _itemsList = _xmlParser.SearchInFile(query);
            }
            catch (Exception e)
            {
            }
        }

        public int ExportEngineId
        {
            get
            {
                return _exportEngineId;
            }
            set
            {
                _exportEngineId = value;
                _exportEngine.SetExportEngine(_exportEngineId);
            }
        }

        public void Export()
        {
            try
            {
                _exportEngine.Export(_itemsList);
            }
            catch (Exception e)
            {
            }
        }
    }
}

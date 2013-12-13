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
        private XmlParserContext parserContext = new XmlParserContext();
        private int _parserId;
        private int _searchType;

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

        public int ParserId
        {
            get
            {
                return _parserId;
            }
            set
            {
                _parserId = value;
                parserContext.SetParser(_parserId);
                parserContext.SetResourseFile(_resourceFile);
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
                parserContext.SetSearchType(_searchType);
                _itemsList = parserContext.SearchInFile(query);
            }
            catch (Exception e)
            {
            }
        }
    }
}

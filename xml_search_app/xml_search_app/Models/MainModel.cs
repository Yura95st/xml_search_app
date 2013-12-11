using System;
using System.Collections.Generic;
using System.IO;
using xml_search_app.Libs.ObserverPattern;
using xml_search_app.XmlParsers;

namespace xml_search_app.Models
{
    public class MainModel : ISubject
    {
        private List<IObserver> _observers = new List<IObserver>();
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
            private set
            {
                _itemsList = value;
                NotifyObservers(0);
            }
        }
 
        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }
 
        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }
 
        public void NotifyObservers(int notificationCode)
        {
            foreach (var observer in _observers)
            {
                observer.Update(notificationCode);
            }
        }

        public void ParseFile(string parserName)
        {
            try
            {
                XmlParserContext parserContext = new XmlParserContext();
                parserContext.SetParser(parserName);
                parserContext.SetResourseFile(_resourceFile);
                ItemsList = parserContext.ParseFile();
            }
            catch (Exception e)
            {
            }
        }
    }
}

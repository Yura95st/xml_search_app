using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

using xml_search_app.Models;

namespace xml_search_app.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string[] _processorTypeArray = {"SAX", "DOM", "LinqToXml"};

        private MainModel _model = new MainModel();
        private string _inputQuery;
        private int _searchByType = 0;
        private int _processorType = 0;

        public MainViewModel()
        {
            _model.ResourceFile = @"resources\source_file.xml";
        }

        public string InputQuery
        {
            get 
            {
                return _inputQuery;
            }
            set
            {
                _inputQuery = value;
                RaisePropertyChanged("InputQuery");
                RaisePropertyChanged("BookItemList");
            }
        }

        public List<AddressBookListItem> BookItemList
        {
            get
            {
                try
                {
                    _model.ParseFile(_processorTypeArray[_processorType]);
                }
                catch (Exception e)
                { }

                List<BookItem> list = _model.ItemsList;
                List<AddressBookListItem> adressBookList = new List<AddressBookListItem>();

                foreach (var bookItem in list)
                {
                    var item = new AddressBookListItem
                    {
                        FullName = bookItem.Name.LastName + " " + bookItem.Name.FirstName + ". " + bookItem.Name.MiddleName + ".",
                        City = bookItem.Address.City,
                        Address = bookItem.Address.House + " " + bookItem.Address.Street + ", apt " + bookItem.Address.Apartment,
                        PhoneNumber = bookItem.PhoneNumber
                    };

                    adressBookList.Add(item);
                }

                return adressBookList;
            }
        }

        public int SearchByType
        {
            get
            {
                return _searchByType;
            }
            set
            {
                _searchByType = value;
                RaisePropertyChanged("SearchByType");
                RaisePropertyChanged("BookItemList");
            }
        }

        public int ProcessorType
        {
            get
            {
                return _processorType;
            }
            set
            {
                _processorType = value;
                RaisePropertyChanged("ProcessorType");
                RaisePropertyChanged("BookItemList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}

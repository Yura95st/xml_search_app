using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

using xml_search_app.Models;

namespace xml_search_app.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel _model = new MainModel();
        private string _inputQuery;
        private int _processorType = 0;
        private int _searchType = 0;

        public MainViewModel()
        {
            _model.ResourceFile = ProgramValues.RESOURCE_FILE;
            ProcessorType = 0;
            SearchType = 0;
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
                    _model.Search(_inputQuery.Trim());
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

        public int SearchType
        {
            get
            {
                return _searchType;
            }
            set
            {
                _searchType = value;
                _model.SearchType = _searchType;
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
                _model.ParserId = _processorType;
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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

using xml_search_app.Models;
using xml_search_app.Commands;
using System.Windows;

namespace xml_search_app.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private MainModel _model = new MainModel();
        private string _inputQuery;
        private int _processorType = -1;
        private int _searchType = -1;
        private List<AddressBookListItem> _adressBookList = new List<AddressBookListItem>();

        private ICommand _exportCommand;
        private ICommand _copyToClipboardCommand;
        private bool _isNotListEmpty;        

        public MainViewModel()
        {
            ProcessorType = 0;
            SearchType = 0;
            _model.ExportEngineId = 0;
        }

        public string InputQuery
        {
            get 
            {
                return _inputQuery;
            }
            set
            {
                if (_inputQuery != value)
                {
                    _inputQuery = value;
                    RaisePropertyChanged("InputQuery");

                    BookItemList = Search();
                }
            }
        }

        public List<AddressBookListItem> BookItemList
        {
            get
            {
                return _adressBookList;
            }
            set
            {
                //if (value != _adressBookList)
                {
                    _adressBookList = value;
                    RaisePropertyChanged("BookItemList");
                    RaisePropertyChanged("ExportCommand");
                }
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
                if (_searchType != value)
                {
                    _searchType = value;
                    _model.SearchType = _searchType;
                    RaisePropertyChanged("SearchByType");

                    BookItemList = Search();
                }
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
                if (_processorType != value)
                {
                    _processorType = value;
                    _model.ParserId = _processorType;
                    RaisePropertyChanged("ProcessorType");

                    BookItemList = Search();
                }
            }
        }
               
        public bool IsNotListEmpty
        {
            get
            {
                return _isNotListEmpty;
            }
            set
            {
                if (value != _isNotListEmpty)
                {
                    _isNotListEmpty = value;
                    RaisePropertyChanged("IsListEmpty");
                }
            }
        }

        public ICommand ExportCommand
        {
            get
            {
                if (_exportCommand == null)
                {
                    _exportCommand = new RelayCommand(new Action<object>(Export),
                        param => IsNotListEmpty);
                }

                return _exportCommand;
            }
        }

        public ICommand CopyToClipboardCommand
        {
            get
            {
                if (_copyToClipboardCommand == null)
                {
                    _copyToClipboardCommand = new RelayCommand(new Action<object>(CopyToClipboard),
                        param => IsNotListEmpty);
                }

                return _copyToClipboardCommand;
            }
        }

        private void Export(object parameter)
        {
            _model.ExportEngineId = Convert.ToInt32(parameter);
            _model.Export();
        }

        private List<AddressBookListItem> Search()
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

            IsNotListEmpty = (adressBookList.Count > 0);

            return adressBookList;
        }

        private void CopyToClipboard(object parameter)
        {
            AddressBookListItem item = (AddressBookListItem)parameter;
            string text = item.PhoneNumber + " " + item.FullName + " " + item.City + " " + item.Address;
            Clipboard.SetData(DataFormats.UnicodeText, text);
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

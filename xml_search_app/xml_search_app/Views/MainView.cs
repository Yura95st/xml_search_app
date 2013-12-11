using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Drawing;

using xml_search_app.Libs.ObserverPattern;
using xml_search_app.Models;
using xml_search_app.Controllers;

namespace xml_search_app.Views
{
    public class MainView : IObserver
    {
        private Window _mainWindow;

        private MainController _controller;
        private MainModel _model;

        private TextBox _searchTextBox;
        private ComboBox _searchByComboBox;
        private ListView _resultsListView;

        public MainView(Window mainWindow)
        {
            _mainWindow = mainWindow;
            _searchTextBox = (TextBox)mainWindow.FindName("searchTextBox");
            _searchByComboBox = (ComboBox)mainWindow.FindName("searchByComboBox");
            _resultsListView = (ListView)mainWindow.FindName("resultsListView");
        }

        public void AddListener(MainController controller)
        {
            _controller = controller;
        }

        public void AddModel(MainModel model)
        {
            _model = model;
        }

        public void Update(int notificationCode)
        {
            switch (notificationCode)
            {
                case 0:
                    BuildResultsList();
                    break;
            }
        }

        public void BuildResultsList()
        {
            List<BookItem> list = _model.ItemsList;
            _resultsListView.Items.Clear();

            foreach (var bookItem in list)
            {
                var item = new AddressBookListItem
                {
                    FullName = bookItem.Name.LastName + " " + bookItem.Name.FirstName + ". " + bookItem.Name.MiddleName + ".",
                    City = bookItem.Address.City,
                    Address = bookItem.Address.House + " " + bookItem.Address.Street + ", apt " + bookItem.Address.Apartment,
                    PhoneNumber= bookItem.PhoneNumber
                };

                _resultsListView.Items.Add(item);
            }
        }
    }
}

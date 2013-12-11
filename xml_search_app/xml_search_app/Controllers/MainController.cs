using System;
using System.Collections.Generic;
using xml_search_app.Models;
using xml_search_app.Views;

namespace xml_search_app.Controllers
{
    public class MainController
    {
        private MainModel _model;
        private MainView _view;

        public MainController(MainModel model, MainView view)
        {
            _model = model;
            _view = view;
            _model.AddObserver(_view);
            _view.AddModel(_model);
            _view.AddListener(this);
        }

        public void Init()
        {
            _model.ParseFile("SAX");
            //_model.ParseFile("DOM");
        }
    }
}

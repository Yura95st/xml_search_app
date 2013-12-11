using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using xml_search_app.Models;
using xml_search_app.Controllers;
using xml_search_app.Views;

namespace xml_search_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            MainView view = new MainView(this);
            MainModel model = new MainModel();
            model.ResourceFile = @"resources\source_file.xml";

            MainController controller = new MainController(model, view);

            controller.Init();
        }
    }
}

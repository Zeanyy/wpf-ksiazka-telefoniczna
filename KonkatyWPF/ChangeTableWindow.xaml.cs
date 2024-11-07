using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KonkatyWPF
{
    /// <summary>
    /// Logika interakcji dla klasy ChangeTableWindow.xaml
    /// </summary>
    public partial class ChangeTableWindow : Window
    {
        public ChangeTableWindow()
        {
            InitializeComponent();
        }

        private void addTable(object sender, RoutedEventArgs e)
        {
            string name = textBoxName.Text;
            string nameRegex = "^[a-zA-Z0-9_]+$";

            if (!Regex.IsMatch(name, nameRegex))
            {
                errorLabel.Content = "* Nazwa bazy jest niepoprawna";
            }
            else
            {
                Model.addTable(name);
                this.Close();
            }
        }
    }
}

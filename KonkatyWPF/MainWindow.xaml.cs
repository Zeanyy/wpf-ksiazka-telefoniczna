using SQLite;
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

namespace KonkatyWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static int page = 0;
        static int maxPage;

        public MainWindow()
        {
            InitializeComponent();
            Model.Open();
            refresh();
        }
        public void refresh()
        {
            if (Model.GetPersons(page, searchBox.Text).Count <= 0) { page--; pageLabel.Content = page + 1; }

            var count = Model.countPersons(searchBox.Text);
            if (count == 0) { count = 1; page = 0; pageLabel.Content = page + 1; }
            maxPage = (count % Model.limit == 0) ? count / Model.limit : count / Model.limit + 1;

            progressBar.Maximum = maxPage;

            dataGrid.ItemsSource = Model.GetPersons(page, searchBox.Text);
        }
        private void searchValue(object sender, TextChangedEventArgs e)
        {
            dataGrid.ItemsSource = Model.GetPersons(page, searchBox.Text);
        }

        private void addPerson(object sender, RoutedEventArgs e)
        {
            AddWindow window = new AddWindow();
            window.ShowDialog();
            refresh();
        }

        private void deletePerson(object sender, RoutedEventArgs e)
        {
            var item = dataGrid.SelectedItem;
            if (item == null) { return; }
            Model.deletePerson(((Person)item).id);
            refresh();
        }

        private void editPerson(object sender, RoutedEventArgs e)
        {
            var item = dataGrid.SelectedItem;
            if (item == null) { return; }
            EditWindow window = new EditWindow((Person)item);
            window.ShowDialog();
            refresh();
        }

        private void closeTable(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void changeTable(object sender, RoutedEventArgs e)
        {
            ChangeTableWindow window = new ChangeTableWindow();
            window.ShowDialog();
            page = 0;
            pageLabel.Content = page + 1;
            refresh();
        }

        private void previewPage(object sender, RoutedEventArgs e)
        {
            if(page <= 0) { return; }
            page--;
            pageLabel.Content = page + 1;
            progressBar.Value = page + 1;
            dataGrid.ItemsSource = Model.GetPersons(page, searchBox.Text);
        }

        private void nextPage(object sender, RoutedEventArgs e)
        {
            if(page >= maxPage-1) { return; }
            page++;
            pageLabel.Content = page + 1;
            progressBar.Value = page + 1;
            dataGrid.ItemsSource = Model.GetPersons(page, searchBox.Text);
        }
    }
}

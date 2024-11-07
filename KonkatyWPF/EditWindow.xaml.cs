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
    /// Logika interakcji dla klasy EditWindow.xaml
    /// </summary>
    public partial class EditWindow : Window
    {
        static int selectedId;
        public EditWindow(Person person)
        {
            InitializeComponent();
            selectedId = person.id;
            textBoxName.Text = person.name;
            textBoxSurname.Text = person.surname;
            radioMale.IsChecked = true;
            if(person.gender == 'k') { radioFemale.IsChecked = true; }
            textBoxEmail.Text = person.email;
            comboBoxState.SelectedValue = person.state;
            textBoxNumber.Text = person.number.ToString();
        }

        private void editPerson(object sender, RoutedEventArgs e)
        {
            string alert = "";
            errorLabel.Text = alert;
            bool check = true;

            string name = textBoxName.Text;
            string surname = textBoxSurname.Text;
            string gender = "m";
            if (radioFemale.IsChecked == true)
            {
                gender = "k";
            }
            string email = textBoxEmail.Text;
            string state = (comboBoxState.SelectedValue != null) ? comboBoxState.SelectedValue.ToString() : null;
            string number = textBoxNumber.Text;

            string nameRegex = "^[A-Z]{1}[a-ząćęłńóśźżĄĆĘŁŃÓŚŹŻ]+$";
            if (!Regex.IsMatch(name, nameRegex))
            {
                check = false;
                alert += "* Imie jest niepoprawne\n";
            }
            string surnameRegex = "^[A-Z]{1}[a-ząćęłńóśźżĄĆĘŁŃÓŚŹŻ]+$";
            if (!Regex.IsMatch(surname, surnameRegex))
            {
                check = false;
                alert += "* Nazwisko jest niepoprawne\n";
            }
            string emailRegex = "^[a-zA-Z0-9_.+-]+@(?:[a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailRegex))
            {
                check = false;
                alert += "* Email jest niepoprawny\n";
            }
            if (state == null)
            {
                check = false;
                alert += "* Wojewodztwo nie jest wybrane\n";
            }
            string numberRegex = "^[0-9]{9}$";
            if (!Regex.IsMatch(number, numberRegex))
            {
                check = false;
                alert += "* Numer jest niepoprawny\n";
            }

            if (!check)
            {
                errorLabel.Text = alert;
            }
            else
            {
                Model.editPerson(new Person(selectedId, name, surname, gender, email, state, Convert.ToInt32(number)));
                this.Close();
            }
        }
    }
}

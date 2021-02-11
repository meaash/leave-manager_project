using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ISDhhMuszakBeosztasDataAccess;
using ISDhhMuszakBeosztasDataAccess.Model;
using ISDhhMuszakBeosztasDataAccess.UserSettings;

namespace ISDhhMuszakBeosztasUI.View
{
    /// <summary>
    /// Interaction logic for EmployeeView.xaml
    /// </summary>
    public partial class EmployeeView : UserControl
    {
        private EmpData _empdata;
        private MySettings _mySettings;
        private string myMuszak;
        private List<EmployeeModel> empadatokList = new List<EmployeeModel>();

        public EmployeeView()
        {
            InitializeComponent();
            _empdata = new EmpData(); 
            _mySettings = new MySettings();
            myMuszak = _mySettings.MyMuszak;
            this.Loaded += EmployeeView_Loaded;

        }

        private void EmployeeView_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewLoad();
        }
        private void ListViewLoad()
        {
            hiba.Content = "";
            //itt megtörténik a lista kiíratása egyből, hogy kik szerepelnek az adatbázisban
            peopleListView.Items.Clear();
 
            if (myMuszak == "Minden Műszak" || myMuszak == null || myMuszak == "")
            { empadatokList = _empdata.GetEmpData(); }
            else
            {
                empadatokList = _empdata.GetEmpData().Where(item => item.Muszak == myMuszak).ToList();
            }
            empadatokList = empadatokList.OrderBy(item => item.LastName).ToList();
            foreach (var item in empadatokList)
            {
                peopleListView.Items.Add(item);
            }
            
        }



        private void ButtonAddEmp_Click(object sender, RoutedEventArgs e)
        {
            var people = new EmployeeModel { FirstName = "New", SzuletesiDatum = DateTime.Now };
            if (myMuszak != "Minden Műszak") { people.Muszak = myMuszak; };
            peopleListView.Items.Add(people);
            peopleListView.SelectedItem = people;
        }

        private void ButtonDeleteEmp_Click(object sender, RoutedEventArgs e)
        {
            //itt lehet majd törölni
            var people = peopleListView.SelectedItem as EmployeeModel;
            if (people != null)
            { peopleListView.Items.Remove(people); }

        }

        private void PeopleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //ez a metódus arra van, hogy amit kiválasztunk annak az adatai jelenjenek meg
            //A kiválasztott itemet Modellé kasztoljuk
            //itt csekkoljuk, hogy nulla, ha igen akkor megadunk értéket
            var people = peopleListView.SelectedItem as EmployeeModel;
            txtFirstName.Text = people?.FirstName ?? "";
            txtLastName.Text = people?.LastName ?? "";
            txtEmail.Text = people?.EMail ?? "";
            txtTelefon.Text = people?.Tel ?? "";
            Szuletesidp.SelectedDate = people?.SzuletesiDatum; //nem lehet üres a mező
            txtMunkakor.Text = people?.Munkakor ?? "";
            Muszakcb.Text = people?.Muszak ?? "";
            txtExtraSzab.Text = Convert.ToString(people?.ExtraSzabad) ?? "0"; //nem lehet üres a mező         
        }


        private void ButtonDeleteDB_Click(object sender, RoutedEventArgs e)
        {
            var people = peopleListView.SelectedItem as EmployeeModel;

            if (people != null)
            {
                _empdata.EmpAdatokDataAccesDelete(people);
                MessageBox.Show("Sikeresen törölve az adatbázisból!");
            }
            else
            { MessageBox.Show("Nincs kiválasztva listaelem!"); }
            //újra betöltjük a listát

            ListViewLoad();
        }
        private bool Validate()
        {
            var isValid = true;

            if (string.IsNullOrWhiteSpace(txtFirstName.Text)) isValid = false;
            if (string.IsNullOrWhiteSpace(txtLastName.Text)) isValid = false;
            if (string.IsNullOrWhiteSpace(Muszakcb.Text)) isValid = false;
            return isValid;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            bool fieldnotempty = Validate();


            var people = peopleListView.SelectedItem as EmployeeModel;
            //Ennek a peoplenek még nincsenek adatai azert kell megadni neki!!
            //adatbázishoz majd így adjuk hozzá:!!
            if (people != null)
            {
                if (fieldnotempty == true)
                {
                    people.FirstName = txtFirstName.Text;
                    people.LastName = txtLastName.Text;
                    people.SzuletesiDatum = Convert.ToDateTime(Szuletesidp.Text);
                    people.EMail = txtEmail.Text;
                    people.Tel = txtTelefon.Text;
                    people.Munkakor = txtMunkakor.Text;
                    people.Muszak = Muszakcb.Text;
                    //az extraszabadnapok száma vagy 0 vagy megadott szám lehet
                    if (txtExtraSzab.Text != "")
                    { people.ExtraSzabad = Convert.ToInt32(txtExtraSzab.Text); }
                    else { people.ExtraSzabad = 0; }

                    if (people.ID > 0)
                    {
                        //ha már meglévő ember csak modositani akarod:
                        _empdata.EmpAdatokDataAccessUpdate(people);
                        MessageBox.Show("A változtatások sikeresen feltöltve az adatbázisba!");
                    }
                    else
                    {
                        //egyébként újat hozzáad:
                        _empdata.EmpAdatokDataAccesSave(people);
                        MessageBox.Show("Sikeresen feltöltve az adatbázisba!");
                    }
                    ListViewLoad();
                    
                }
                else
                { hiba.Content = "Az üresen hagyott mezők kitöltése kötelező!"; }
            }
            else
            { MessageBox.Show("Nincs kiválasztva listaelem!"); }
            //újra betöltjük a listát

        }

        //txt textchanged valamiért nem működik egy eventhandlerrel
        //mindegyiknek külön lett létrehozva

        private void txtLastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var people = peopleListView.SelectedItem as EmployeeModel;
            if (people != null)
            {
                people.LastName = txtLastName.Text;
            }
        }

        private void txtFirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var people = peopleListView.SelectedItem as EmployeeModel;
            if (people != null)
            {
                people.FirstName = txtFirstName.Text;
            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            var people = peopleListView.SelectedItem as EmployeeModel;
            if (people != null)
            {
                people.EMail = txtEmail.Text;
            }
        }

        private void txtTel_TextChanged(object sender, TextChangedEventArgs e)
        {
            var people = peopleListView.SelectedItem as EmployeeModel;
            if (people != null)
            {
                people.Tel = txtTelefon.Text;
            }
        }

        private void Szuletesidp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            var people = peopleListView.SelectedItem as EmployeeModel;
            if (people != null && Szuletesidp.Text != "")
            {
                people.SzuletesiDatum = Convert.ToDateTime(Szuletesidp.Text);
            }
        }

        private void Muszak_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var people = peopleListView.SelectedItem as EmployeeModel;
            if (people != null && Muszakcb.SelectedItem != null)
            {
                people.Muszak = (e.AddedItems[0] as ComboBoxItem).Content as string;
            }
        }
        private void txtMunkakor_TextChanged(object sender, TextChangedEventArgs e)
        {
            var people = peopleListView.SelectedItem as EmployeeModel;
            if (people != null)
            {
                people.Munkakor = txtMunkakor.Text;
            }

        }

        private void txtExtraSzab_TextChanged(object sender, TextChangedEventArgs e)
        {
            var people = peopleListView.SelectedItem as EmployeeModel;
            if (people != null && txtExtraSzab.Text != "")
            {
                people.ExtraSzabad = Convert.ToInt32(txtExtraSzab.Text);
            }
        }

        //A textboxba csak és kizárólag számot lehet megadni
        private void txtprev_numeric(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }


    }
}

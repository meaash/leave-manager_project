using ISDhhMuszakBeosztasDataAccess;
using ISDhhMuszakBeosztasDataAccess.Model;
using ISDhhMuszakBeosztasDataAccess.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ISDhhMuszakBeosztasUI.View
{
    /// <summary>
    /// Interaction logic for OverTimeView.xaml
    /// </summary>
    public partial class OverTimeView : UserControl
    {
        private OverTimeData _overtimedata;
        private EmpData _empdata;
        private MySettings _mySettings;
        private string myMuszak;
        private List<EmployeeModel> empadatokList = new List<EmployeeModel>();
        public OverTimeView()
        {
            InitializeComponent();
            _overtimedata = new OverTimeData();
            _empdata = new EmpData();
            _mySettings = new MySettings();
            myMuszak = _mySettings.MyMuszak;
            this.Loaded += OverTimeView_Loaded;
        }
        private void OverTimeView_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridLoad();
            PeopleListViewLoad();
        }
        private void DataGridLoad()
        {
            hiba.Content = "";
            //DataGrid feltöltése
            var tuloralist = _overtimedata.GetOverTimeData();

            if (myMuszak == "Minden Műszak" || myMuszak == null || myMuszak == "")
            { tuloralist = _overtimedata.GetOverTimeData(); }
            else
            {
                tuloralist = _overtimedata.GetOverTimeData().Where(item => item.sajatMuszak == myMuszak).ToList();
            }
            tuloralist = tuloralist.OrderBy(item => item.Datum).ToList();
            TuloraDataGrid.ItemsSource = tuloralist;

        }
        private void PeopleListViewLoad()
        {

            PeopleComboBox.Items.Clear();
            if (myMuszak == "Minden Műszak" || myMuszak == null || myMuszak == "")
            { empadatokList = _empdata.GetEmpData(); }
            else
            {
                empadatokList = _empdata.GetEmpData().Where(item => item.Muszak == myMuszak).ToList();
            }
            empadatokList = empadatokList.OrderBy(item => item.LastName).ToList();
            PeopleComboBox.ItemsSource = empadatokList;

        }
        private void PeopleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PickerDatum.IsEnabled = true;
            txt_Munkakor.IsEnabled = true;
            CBtuloraMuszak.IsEnabled = true;

            var people = PeopleComboBox.SelectedItem as EmployeeModel;
            txtName.Text = people.FullName;
            txtMuszak.Text = people.Muszak;
      
        }

        private void BTSaveTulora(object sender, RoutedEventArgs e)
        {
            var people = PeopleComboBox.SelectedItem as EmployeeModel;

            if (people != null)
            {
                if (txt_Munkakor.Text != "" && CBtuloraMuszak.Text != "" && PickerDatum.Text != "")
                {
                    var tulora = new OverTimeModel();
                    tulora.Name = txtName.Text;
                    tulora.sajatMuszak = txtMuszak.Text;
                    tulora.Munkakor = txt_Munkakor.Text;
                    tulora.tuloraMuszak = CBtuloraMuszak.Text;
                    tulora.Datum = Convert.ToDateTime(PickerDatum.Text);

                    _overtimedata.TuloraAdatokDataAccesSave(tulora);
                    MessageBox.Show("Sikeresen Mentve!");
                    DataGridLoad();
                }
                else { hiba.Content =  "A mezők kitöltése kötelező!"; }
            }
            else
            { MessageBox.Show("Nincs kiválasztva listaelem!"); }
        }

        private void ButtonDeleteRow_Click(object sender, RoutedEventArgs e)
        {

            var datarow = TuloraDataGrid.SelectedItem as OverTimeModel;
            if (datarow != null)
            {
                _overtimedata.TuloraAdatokDataAccesDelete(datarow);
                MessageBox.Show("Sikeresen Törölve!");
                DataGridLoad();
            }
            else
            { MessageBox.Show("Nincs kiválasztva listaelem!"); }

        }
    }
}

using ISDhhMuszakBeosztasDataAccess;
using ISDhhMuszakBeosztasDataAccess.Model;
using ISDhhMuszakBeosztasDataAccess.UserSettings;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
namespace ISDhhMuszakBeosztasUI.View
{
    /// <summary>
    /// Interaction logic for SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        private MuszakBeosztasData _muszakbeoasztasdata;
        private EmpData _empdata;
        private MySettings _mysettings;
        private HolidayData _holidaydata;
        public SettingsView()
        {
            InitializeComponent();
            _muszakbeoasztasdata = new MuszakBeosztasData();
            _empdata = new EmpData();
            _mysettings = new MySettings();
            _holidaydata = new HolidayData();
            this.Loaded += SettingsView_Loaded;
        }
        private void SettingsView_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewLoad();
            HolidayListViewLoad();
        }
        private void ListViewLoad()
        {
            //C napok kiíratása

            var cnapok = _empdata.GetCNapokData();

            foreach (var item in cnapok)
            {
                if (item.Muszak == "A")
                { txtA.Text = item.CNapokSzama.ToString(); }
                else if (item.Muszak == "B")
                { txtB.Text = item.CNapokSzama.ToString(); }
                else if (item.Muszak == "C")
                { txtC.Text = item.CNapokSzama.ToString(); }
                else if (item.Muszak == "D")
                { txtD.Text = item.CNapokSzama.ToString(); }
            }

            //Műszak megejelenítéshez szükséges beállítás
            if (_mysettings.MyMuszak == "Minden Műszak")
            { CBmyMuszak.SelectedIndex = 0; }
            else if (_mysettings.MyMuszak == "A")
            { CBmyMuszak.SelectedIndex = 1; }
            else if (_mysettings.MyMuszak == "B")
            { CBmyMuszak.SelectedIndex = 2; }
            else if (_mysettings.MyMuszak == "C")
            { CBmyMuszak.SelectedIndex = 3; }
            else if (_mysettings.MyMuszak == "D")
            { CBmyMuszak.SelectedIndex = 4; }

        }

        private void HolidayListViewLoad()
        {
            var holidaylist = _holidaydata.GetHolidayData().OrderBy(item => item.UnnepNap).ToList();
            HolidayListView.ItemsSource = holidaylist;
            var munkanaplist = _holidaydata.GetMunkaNapData().OrderBy(item => item.MunkaNap).ToList();
            MunkanapLisView.ItemsSource = munkanaplist;
            var pihenonaplist = _holidaydata.GetPihenoNapData().OrderBy(item => item.PihenoNap).ToList();
            PihenonapListView.ItemsSource = pihenonaplist;

        }

        private void txtprev_numeric(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private List<MuszakBeosztasModel> mbList = new List<MuszakBeosztasModel>();
        private void bt_talloz(object sender, RoutedEventArgs e)
        {

            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                try { 

                StreamReader sr = new StreamReader(ofd.FileName, Encoding.Default);
                string fejlec = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    string sor = sr.ReadLine();
                    string[] sorelemek = sor.Split(';');
                    MuszakBeosztasModel mb = new MuszakBeosztasModel();
                    mb.Ev = sorelemek[0];
                    mb.Honap = sorelemek[1];
                    mb.Nap = sorelemek[2];
                    mb.HetNap = sorelemek[3];
                    mb.Delelott = sorelemek[4];
                    mb.Delutan = sorelemek[5];
                    mb.Ejszaka = sorelemek[6];
                    mb.Szabad = sorelemek[7];
                    mbList.Add(mb);

                }
                sr.Close();
                if (mbList.Count < 366)
                {
                    MessageBox.Show("A beosztás nincs kitöltve egész évre");
                }
                else
                { 
                MessageBox.Show("A file beolvasása sikeres");
                tb_fname.Text = ofd.FileName;
                }
                }
                catch (Exception)
                {
                    MessageBox.Show("A file tartalmi követlményei nem megfelelőek!");
                }

            }
            else
            {
                MessageBox.Show("Nem választott ki filet!");
            }
        }

        private void bt_Muszakbeosztas_feltolt(object sender, RoutedEventArgs e)
        {
            if (tb_fname.Text.Length > 0)
            {
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += worker_DoWork;
                worker.ProgressChanged += worker_ProgressChanged;
                worker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("Nem választott ki filet!");
            }

        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            try
            {
                _muszakbeoasztasdata.MuszakBeosztasDataAccesDelete();

                for (int i = 0; i < mbList.Count; i++)
                {
                    _muszakbeoasztasdata.MuszakBeosztasDataAccesSave(mbList[i]);
                    (sender as BackgroundWorker).ReportProgress(i);

                }
                MessageBox.Show("Az Adatbázis feltöltése sikeres volt");
            }
            catch (Exception)
            {
                MessageBox.Show("A fájl csatolása nem sikerült");
            }

        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            PgBar.Visibility = Visibility.Visible;
            PgBar.Minimum = 0;
            PgBar.Maximum = mbList.Count;
            PgBar.Value = e.ProgressPercentage;
        }

        private void btnCNapokSave_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtA.Text) && !string.IsNullOrWhiteSpace(txtB.Text) &&
                !string.IsNullOrWhiteSpace(txtC.Text) &&
                !string.IsNullOrWhiteSpace(txtD.Text))
            {
                List<int> cnapok = new List<int>();
                cnapok.Clear();
                cnapok.Add(Convert.ToInt32(txtA.Text));
                cnapok.Add(Convert.ToInt32(txtB.Text));
                cnapok.Add(Convert.ToInt32(txtC.Text));
                cnapok.Add(Convert.ToInt32(txtD.Text));

                _empdata.UpdateCnapok(cnapok);
            }
            else
            {
                MessageBox.Show("Nem megfelelő érték lett megadva!");
            }
        }

        private void btnMuszakShowSave_Click(object sender, RoutedEventArgs e)
        {
            _mysettings.UpdatemyMuszak(CBmyMuszak.Text);
        }

        private void btnHolidaySave_Click(object sender, RoutedEventArgs e)
        {
            if (dp_unnepnap.Text != "")
            {
                DateTime Holiday = Convert.ToDateTime(dp_unnepnap.Text);
                _holidaydata.HolidaySave(Holiday);
                HolidayListViewLoad();

            }
            else
            {
                MessageBox.Show("Nincs kiválasztva Dátum!");
            }

        }
        private void btnHolidayDelete_Click(object sender, RoutedEventArgs e)
        {
            var holidaymodel = HolidayListView.SelectedItem as UnnepnapModel;

            if (holidaymodel != null)
            {
                _holidaydata.HolidayDelete(holidaymodel);
                HolidayListViewLoad();
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva Dátum!");
            }

        }

        private void btn_LedolgozandoSave_Click(object sender, RoutedEventArgs e)
        {

            if (dp_munkanap.Text != "" && dp_pihenonap.Text == "")
            {
                DateTime Munkanap = Convert.ToDateTime(dp_munkanap.Text);
                _holidaydata.MunkanapSave(Munkanap);
                HolidayListViewLoad();
            }
            else if (dp_pihenonap.Text != "" && dp_munkanap.Text == "")
            {
                DateTime Pihenonap = Convert.ToDateTime(dp_pihenonap.Text);
                _holidaydata.PihenonapSave(Pihenonap);
                HolidayListViewLoad();
            }
            else if (dp_munkanap.Text != "" && dp_pihenonap.Text != "")
            {
                DateTime Munkanap = Convert.ToDateTime(dp_munkanap.Text);
                _holidaydata.MunkanapSave(Munkanap);
                HolidayListViewLoad();
                DateTime Pihenonap = Convert.ToDateTime(dp_pihenonap.Text);
                _holidaydata.PihenonapSave(Pihenonap);
                HolidayListViewLoad();
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva Dátum!");
            }
        }

        private void btn_LedolgozandoDelete_Click(object sender, RoutedEventArgs e)
        {
            var munkanapmodel = MunkanapLisView.SelectedItem as MunkanapModel;
            var pihenonapmodel = PihenonapListView.SelectedItem as PihenonapModel;

            if (munkanapmodel != null)
            {
                _holidaydata.MunkanapDelete(munkanapmodel);
                HolidayListViewLoad();
            }
            else if (pihenonapmodel != null)
            {
                _holidaydata.PihenonapDelete(pihenonapmodel);
                HolidayListViewLoad();
            }
            else
            {
                MessageBox.Show("Nincs kiválasztva Dátum!");
            }

        }
    }
}

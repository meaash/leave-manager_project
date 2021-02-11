using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ISDhhMuszakBeosztasDataAccess;
using ISDhhMuszakBeosztasDataAccess.Model;
using ISDhhMuszakBeosztasDataAccess.UserSettings;

namespace ISDhhMuszakBeosztasUI.View
{
    /// <summary>
    /// Interaction logic for LeaveView.xaml
    /// </summary>
    public partial class LeaveView : UserControl
    {
        private EmpData _empdata;
        private LeaveData _leavedata;
        private MySettings _mySettings;
        private string myMuszak;
        private List<EmployeeModel> empadatokList = new List<EmployeeModel>();

        public LeaveView()
        {
            InitializeComponent();
            _empdata = new EmpData(); 
            _leavedata = new LeaveData();
            _mySettings = new MySettings();
            myMuszak = _mySettings.MyMuszak;
            this.Loaded += LeaveView_Loaded;
        }

        private void LeaveView_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewLoad();
        }
        private void ListViewLoad()
        {
            //itt megtörténik a lista kiíratása egyből, hogy kik szerepelnek az adatbázisban
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
            ReasonCB.IsEnabled = true;
            var people = PeopleComboBox.SelectedItem as EmployeeModel;

            txt_összkivehetö.Text = Convert.ToString(people.Összkievehetö);
            txt_canapokszáma.Text = Convert.ToString(people.CnapokSzama);
            txt_szabadnapokszama.Text = Convert.ToString(people.SzabadnapokSzama);


            Show_LboxDaysonLeave(); //wrap panel elemeit listázza a név változásakor
            Show_LboxDaystoAdd(); //a szabadnapok kezeléséhez a listát listázza
        }



        private void ReasonCombobox_SelectionChanged(object sender, EventArgs e)
        {
            Show_LboxDaystoAdd();
        } //a szabadnapok kezeléséhez a listát listázza


        private void Show_LboxDaysonLeave() //ha név változik kiirja a wrap listákban a távolléteket
        {
            var people = PeopleComboBox.SelectedItem as EmployeeModel;
            var tavpeoplelist = _leavedata.GetLeaveData();
            WrapSzabadnapok.Items.Clear();
            WrapCnapok.Items.Clear();
            WrapBetegszabadsag.Items.Clear();
            WrapIgazolatan.Items.Clear();
            foreach (var item in tavpeoplelist)
            {
                if (item.Leave_ID == people.ID)
                {
                    if (item.Szabadnap != null && item.Szabadnap != "")
                    {
                        if (item.Szabadnap.Contains(','))
                        {
                            var datumok = item.Szabadnap.Split(',').ToList();
                            datumok.Sort();
                            foreach (var datum in datumok)
                            { WrapSzabadnapok.Items.Add(datum); }
                        }
                        else { WrapSzabadnapok.Items.Add(item.Szabadnap); };
                    }

                    if (item.Cnap != null && item.Cnap != "")
                    {
                        if (item.Cnap.Contains(','))
                        {
                            var datumok = item.Cnap.Split(',').ToList();
                            datumok.Sort();
                            foreach (var datum in datumok)
                            { WrapCnapok.Items.Add(datum); }
                        }
                        else { WrapCnapok.Items.Add(item.Cnap); };
                    }

                    if (item.BetegSzabadsag != null && item.BetegSzabadsag != "")
                    {
                        if (item.BetegSzabadsag.Contains(','))
                        {
                            var datumok = item.BetegSzabadsag.Split(',').ToList();
                            datumok.Sort();
                            foreach (var datum in datumok)
                            { WrapBetegszabadsag.Items.Add(datum); }
                        }
                        else { WrapBetegszabadsag.Items.Add(item.BetegSzabadsag); };
                    }

                    if (item.Igazolatlan != null && item.Igazolatlan != "")
                    {
                        if (item.Igazolatlan.Contains(','))
                        {
                            var datumok = item.Igazolatlan.Split(',').ToList();
                            datumok.Sort();
                            foreach (var datum in datumok)
                            { WrapIgazolatan.Items.Add(datum); }
                        }
                        else { WrapIgazolatan.Items.Add(item.Igazolatlan); };
                    }
                }
            }

        }

        private void Show_LboxDaystoAdd()
        {
            TavolletiNapokLbox.Items.Clear();
            var people = PeopleComboBox.SelectedItem as EmployeeModel;
            var tavpeoplelist = _leavedata.GetLeaveData();

            if (people != null)
            {
                foreach (var item in tavpeoplelist)
                {

                    if (item.Leave_ID == people.ID && ReasonCB.Text == "Fizetett szabadság")
                    {
                        if (item.Szabadnap != null && item.Szabadnap != "")
                        {
                            if (item.Szabadnap.Contains(','))
                            {
                                var datumok = item.Szabadnap.Split(',').ToList();
                                datumok.Sort();
                                foreach (var datum in datumok)
                                { TavolletiNapokLbox.Items.Add(datum); }
                            }
                            else { TavolletiNapokLbox.Items.Add(item.Szabadnap); };
                        }
                    }
                    else if (item.Leave_ID == people.ID && ReasonCB.Text == "C nap")
                    {

                        if (item.Cnap != null && item.Cnap != "")
                        {
                            if (item.Cnap.Contains(','))
                            {
                                var datumok = item.Cnap.Split(',').ToList();
                                datumok.Sort();
                                foreach (var datum in datumok)
                                { TavolletiNapokLbox.Items.Add(datum); }
                            }
                            else { TavolletiNapokLbox.Items.Add(item.Cnap); };
                        }
                    }
                    else if (item.Leave_ID == people.ID && ReasonCB.Text == "Betegszabadság")
                    {

                        if (item.BetegSzabadsag != null && item.BetegSzabadsag != "")
                        {
                            if (item.BetegSzabadsag.Contains(','))
                            {
                                var datumok = item.BetegSzabadsag.Split(',').ToList();
                                datumok.Sort();
                                foreach (var datum in datumok)
                                { TavolletiNapokLbox.Items.Add(datum); }
                            }
                            else { TavolletiNapokLbox.Items.Add(item.BetegSzabadsag); };
                        }
                    }
                    else if (item.Leave_ID == people.ID && ReasonCB.Text == "Fizetés nélküli szabadság")
                    {

                        if (item.Igazolatlan != null && item.Igazolatlan != "")
                        {
                            if (item.Igazolatlan.Contains(','))
                            {
                                var datumok = item.Igazolatlan.Split(',').ToList();
                                datumok.Sort();
                                foreach (var datum in datumok)
                                { TavolletiNapokLbox.Items.Add(datum); }
                            }
                            else { TavolletiNapokLbox.Items.Add(item.Igazolatlan); };
                        }
                    }
                }
            }
            else { MessageBox.Show("Nincs kiválasztva hozzáadott személy!"); }

        }   // a szerkeszthető listához adja hozzá ha a név vagy az ok változik

        private bool DateExist(string myDatum) // leellenőrzi, hogy a dátum már ki lette e adva (WrapListákat használa)
        {
            if (WrapBetegszabadsag.Items.Contains(myDatum) || WrapCnapok.Items.Contains(myDatum)
                || WrapSzabadnapok.Items.Contains(myDatum) || WrapIgazolatan.Items.Contains(myDatum))
            { return true; }
            else { return false; }

        }

        private void ButtonAddLeave_Click(object sender, RoutedEventArgs e)
        {
            var people = PeopleComboBox.SelectedItem as EmployeeModel;

            //hozzáadjuk a naptári napokat a listboxhoz
            //ha már tartalmazza nem lehet hozzáadni
            // ha az ok c nap vagy fizetett szabadság, nem lehet nagyobb a szám, mint ezek maximális összege
            //else max 365 lehet a szám
            if (people != null && Calendar.SelectedDates.Count > 0 && ReasonCB.SelectedItem != null)
            {

                if (ReasonCB.Text == "Fizetett Szabadság")
                {
                    foreach (var item in Calendar.SelectedDates)
                    {
                        string datum = item.ToString("M. dd. yyyy");
                        bool myDateExists = DateExist(datum);

                        if (myDateExists == false && TavolletiNapokLbox.Items.Count < people.SzabadnapokSzama)
                        {
                            TavolletiNapokLbox.Items.Add(datum);
                        }

                    }
                }

                else if (ReasonCB.Text == "C nap")
                {
                    foreach (var item in Calendar.SelectedDates)
                    {
                        string datum = item.ToString("M. dd. yyyy");
                        bool myDateExists = DateExist(datum);

                        if (myDateExists == false && TavolletiNapokLbox.Items.Count < people.CnapokSzama)
                        {
                            TavolletiNapokLbox.Items.Add(datum);
                        }

                    }
                }
                else
                {
                    foreach (var item in Calendar.SelectedDates)
                    {
                        string datum = item.ToString("M. dd. yyyy");
                        bool myDateExists = DateExist(datum);

                        if (myDateExists == false && TavolletiNapokLbox.Items.Count < 365)
                        {
                            TavolletiNapokLbox.Items.Add(datum);
                        }
                    }
                }

                //miután rendeztük a listboxban hozzáadjuk az adatbázishoz
                //listából stringet csinálunk és updatelve feltöltjük az adatbázisba
                var tavpeople = new LeaveModel();
                List<string> datumoklista = new List<string>();
                foreach (string item in TavolletiNapokLbox.Items)
                {
                    if (item != "")
                    { datumoklista.Add(item); }
                }

                string delimiter = ",";
                string datumok = string.Join(delimiter, datumoklista); //listából stringet


                if (ReasonCB.Text == "Fizetett szabadság")
                {
                    tavpeople.Szabadnap = datumok;
                    tavpeople.Leave_ID = people.ID;
                    _leavedata.UpdateLeaveDataSzabadnap(tavpeople);
                }
                else if (ReasonCB.Text == "C nap")
                {
                    tavpeople.Cnap = datumok;
                    tavpeople.Leave_ID = people.ID;
                    _leavedata.UpdateLeaveDataCnap(tavpeople);
                }
                else if (ReasonCB.Text == "Betegszabadság")
                {
                    tavpeople.BetegSzabadsag = datumok;
                    tavpeople.Leave_ID = people.ID;
                    _leavedata.UpdateLeaveDataBetegszabadsag(tavpeople);
                }
                else if (ReasonCB.Text == "Fizetés nélküli szabadság")
                {
                    tavpeople.Igazolatlan = datumok;
                    tavpeople.Leave_ID = people.ID;
                    _leavedata.UpdateLeaveDataIgazolatlan(tavpeople);
                }
                Show_LboxDaysonLeave();             //frissítjük a wrappanelt
            }
            else
            { MessageBox.Show("Nincs kiválasztva hozzáadott személy, dátum, vagy távollét oka!"); }

        }

        private void ButtonDeleteLeaveDays_Click(object sender, RoutedEventArgs e)
        {
            var people = PeopleComboBox.SelectedItem as EmployeeModel;


            var tavpeople = new LeaveModel();
            if (people != null && TavolletiNapokLbox.SelectedItem != null && ReasonCB.SelectedItem != null)
            {

                TavolletiNapokLbox.Items.RemoveAt(TavolletiNapokLbox.SelectedIndex);

                //miután rendeztük a listboxban hozzáadjuk az adatbázishoz
                //listából stringet csinálunk és updatelve feltöltjük az adatbázisba
                List<string> datumoklista = new List<string>();
                foreach (string item in TavolletiNapokLbox.Items)
                {
                    datumoklista.Add(item);
                }

                string delimiter = ",";
                string datumok = string.Join(delimiter, datumoklista); //listából stringet


                if (ReasonCB.Text == "Fizetett szabadság")
                {
                    tavpeople.Szabadnap = datumok;
                    tavpeople.Leave_ID = people.ID;
                    _leavedata.UpdateLeaveDataSzabadnap(tavpeople);

                }
                else if (ReasonCB.Text == "C nap")
                {
                    tavpeople.Cnap = datumok;
                    tavpeople.Leave_ID = people.ID;
                    _leavedata.UpdateLeaveDataCnap(tavpeople);
                }
                else if (ReasonCB.Text == "Betegszabadság")
                {
                    tavpeople.BetegSzabadsag = datumok;
                    tavpeople.Leave_ID = people.ID;
                    _leavedata.UpdateLeaveDataBetegszabadsag(tavpeople);
                }
                else if (ReasonCB.Text == "Fizetés nélküli szabadság")
                {
                    tavpeople.Igazolatlan = datumok;
                    tavpeople.Leave_ID = people.ID;
                    _leavedata.UpdateLeaveDataIgazolatlan(tavpeople);

                }
                Show_LboxDaysonLeave();      //frissítjük a wrappanelt
            }
            else
            { MessageBox.Show("Nincs kiválasztva hozzáadott személy, dátum, vagy távollét oka!"); }

        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //--------------------------------------
            Mouse.Capture(null); // Calendar focus
            //--------------------------------------


            Lbox_PeopleonLeave.Items.Clear();
            //masik listába csak a mymuszak kerül igy abbol az id alapján lehet;
            var leavelist = _leavedata.GetLeaveData().Where(item => empadatokList.Any(em => em.ID == item.Leave_ID)).ToList();

            List<string> peopleonleavelist = new List<string>(); //segédlista a kiíratáshoz

            for (int j = 0; j < Calendar.SelectedDates.Count; j++)
            {
                string sdatum = Calendar.SelectedDates[j].ToString("M. dd. yyyy");
                for (int i = 0; i < leavelist.Count; i++)
                {

                    if ((leavelist[i].Szabadnap != null && leavelist[i].Szabadnap.Contains(sdatum))
                        || (leavelist[i].Cnap != null && leavelist[i].Cnap.Contains(sdatum))
                        || (leavelist[i].BetegSzabadsag != null && leavelist[i].BetegSzabadsag.Contains(sdatum))
                        || (leavelist[i].Igazolatlan != null && leavelist[i].Igazolatlan.Contains(sdatum)))
                    {
                        if (!peopleonleavelist.Contains(sdatum))
                        { peopleonleavelist.Add(sdatum); }
                        peopleonleavelist.Add(leavelist[i].Name);
                    }
                }
            }

            foreach (string item in peopleonleavelist)
            {
                Lbox_PeopleonLeave.Items.Add(item);

            }
            peopleonleavelist.Clear();

        } //kilistázza kik vannak szabadságon


    }
}


using ISDhhMuszakBeosztasDataAccess;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace ISDhhMuszakBeosztasUI.View
{
    /// <summary>
    /// Interaction logic for MuszakBeosztasView.xaml
    /// </summary>
    public partial class MuszakBeosztasView : UserControl
    {
        private MuszakBeosztasData _muszakbeosztas;
        private EmpData _empdata;
        private HolidayData _holidaydata;

        public MuszakBeosztasView()
        {
            InitializeComponent();
            _muszakbeosztas = new MuszakBeosztasData();
            _empdata = new EmpData();
            _holidaydata = new HolidayData();
            Calendar.SelectedDate = DateTime.Now.AddDays(1);
            this.Loaded += MuszakBeosztasView_Loaded;
        }
        private void MuszakBeosztasView_Loaded(object sender, RoutedEventArgs e)
        {
            ListViewLoad();
            HolidayListViewLoad();
        }

        private void ListViewLoad()
        {
            //műszakbeosztás betöltése
            muszakListView.Items.Clear();
            var muszaklist = _muszakbeosztas.GetMuszakBeosztasData();

            for (int i = 0; i < muszaklist.Count; i++)
            {
                for (int j = 0; j < Calendar.SelectedDates.Count; j++)
                {
                    string SDatesmd = Calendar.SelectedDates[j].ToString("MM. dd. (dddd)");
                    if (Convert.ToString(Calendar.SelectedDates[j].Year) == muszaklist[i].Ev &&
                     Convert.ToString(Calendar.SelectedDates[j].Month) == muszaklist[i].Honap &&
                     Convert.ToString(Calendar.SelectedDates[j].Day) == muszaklist[i].Nap)
                    {
                        muszakListView.Items.Add(SDatesmd + "\nDélelőttös: \t" + muszaklist[i].Delelott + "\nDélutános: \t" + muszaklist[i].Delutan + "\nÉjszakás: \t" + muszaklist[i].Ejszaka + "\nSzabadnapos: \t" + muszaklist[i].Szabad);
                    }
                }
            }

            var cnapok = _empdata.GetCNapokData();

            foreach (var item in cnapok)
            {
                if (item.Muszak == "A")
                { txtA.Text = item.CNapokSzama.ToString(); }
                if (item.Muszak == "B")
                { txtB.Text = item.CNapokSzama.ToString(); }
                if (item.Muszak == "C")
                { txtC.Text = item.CNapokSzama.ToString(); }
                if (item.Muszak == "D")
                { txtD.Text = item.CNapokSzama.ToString(); }
            }
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
        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //--------------------------------------
            Mouse.Capture(null); // calendar focus 
           //--------------------------------------

            ListViewLoad();
        }


    }

}

using ISDhhMuszakBeosztasDataAccess;
using ISDhhMuszakBeosztasDataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;


namespace ISDhhMuszakBeosztasUI.View
{
    /// <summary>
    /// Interaction logic for InformationView.xaml
    /// </summary>
    public partial class InformationView : UserControl
    {

        private EmpData _empdata;
        private LeaveData _leavedata;
        private OverTimeData _overtimedata;


        public InformationView()
        {
            InitializeComponent();
            _empdata = new EmpData();
            _leavedata = new LeaveData();
            _overtimedata = new OverTimeData();
            this.Loaded += InformationView_Loaded;
        }

        private void InformationView_Loaded(object sender, RoutedEventArgs e)
        {
            EmpDataLoad();
            LeaveDataLoad();
            OverTimeDataLoad();
        }
        private void EmpDataLoad()
        {

            var empadatokList = _empdata.GetEmpData();
            int Amuszakregszam = empadatokList.Where(item => item.Muszak == "A").ToList().Count;
            int Bmuszakregszam = empadatokList.Where(item => item.Muszak == "B").ToList().Count;
            int Cmuszakregszam = empadatokList.Where(item => item.Muszak == "C").ToList().Count;
            int Dmuszakregszam = empadatokList.Where(item => item.Muszak == "D").ToList().Count;

            Areg.Text = " - A - műszak munkavállalók száma: " + Amuszakregszam;
            Breg.Text = " - B - műszak munkavállalók száma: " + Bmuszakregszam;
            Creg.Text = " - C - műszak munkavállalók száma: " + Cmuszakregszam;
            Dreg.Text = " - D - műszak munkavállalók száma: " + Dmuszakregszam;


        }

        private void LeaveDataLoad()
        {
            string sdatum = DateTime.Now.ToString("M. dd. yyyy");
            var leaveList = _leavedata.GetLeaveData();
            var Amuszaklist = _empdata.GetEmpData().Where(item => item.Muszak == "A").ToList();
            var Amuszakleavelist = leaveList.Where(item => Amuszaklist.Any(em => em.ID == item.Leave_ID)).ToList();

            int aonleave = 0;

            for (int i = 0; i < Amuszakleavelist.Count; i++)
            {

                if ((Amuszakleavelist[i].Szabadnap != null && Amuszakleavelist[i].Szabadnap.Contains(sdatum))
                                    || (Amuszakleavelist[i].Cnap != null && Amuszakleavelist[i].Cnap.Contains(sdatum))
                                    || (Amuszakleavelist[i].BetegSzabadsag != null && Amuszakleavelist[i].BetegSzabadsag.Contains(sdatum))
                                    || (Amuszakleavelist[i].Igazolatlan != null && Amuszakleavelist[i].Igazolatlan.Contains(sdatum)))
                { aonleave++; }
            }
            Aleave.Text = " - A - műszak hiányzók száma: " + aonleave;

            var Bmuszaklist = _empdata.GetEmpData().Where(item => item.Muszak == "B").ToList();
            var Bmuszakleavelist = leaveList.Where(item => Bmuszaklist.Any(em => em.ID == item.Leave_ID)).ToList();

            int bonleave = 0;

            for (int i = 0; i < Bmuszakleavelist.Count; i++)
            {
                if ((Bmuszakleavelist[i].Szabadnap != null && Bmuszakleavelist[i].Szabadnap.Contains(sdatum))
                                    || (Bmuszakleavelist[i].Cnap != null && Bmuszakleavelist[i].Cnap.Contains(sdatum))
                                    || (Bmuszakleavelist[i].BetegSzabadsag != null && Bmuszakleavelist[i].BetegSzabadsag.Contains(sdatum))
                                    || (Bmuszakleavelist[i].Igazolatlan != null && Bmuszakleavelist[i].Igazolatlan.Contains(sdatum)))
                { bonleave++; }
            }
            Bleave.Text = " - B - műszak hiányzók száma: " + bonleave;


            var Cmuszaklist = _empdata.GetEmpData().Where(item => item.Muszak == "C").ToList();
            var Cmuszakleavelist = leaveList.Where(item => Cmuszaklist.Any(em => em.ID == item.Leave_ID)).ToList();

            int conleave = 0;

            for (int i = 0; i < Cmuszakleavelist.Count; i++)
            {
                if ((Cmuszakleavelist[i].Szabadnap != null && Cmuszakleavelist[i].Szabadnap.Contains(sdatum))
                                    || (Cmuszakleavelist[i].Cnap != null && Cmuszakleavelist[i].Cnap.Contains(sdatum))
                                    || (Cmuszakleavelist[i].BetegSzabadsag != null && Cmuszakleavelist[i].BetegSzabadsag.Contains(sdatum))
                                    || (Cmuszakleavelist[i].Igazolatlan != null && Cmuszakleavelist[i].Igazolatlan.Contains(sdatum)))
                { conleave++; }
            }
            Cleave.Text = " - C - műszak hiányzók száma: " + conleave;

            var Dmuszaklist = _empdata.GetEmpData().Where(item => item.Muszak == "D").ToList();
            var Dmuszakleavelist = leaveList.Where(item => Dmuszaklist.Any(em => em.ID == item.Leave_ID)).ToList();

            int donleave = 0;

            for (int i = 0; i < Dmuszakleavelist.Count; i++)
            {
                if ((Dmuszakleavelist[i].Szabadnap != null && Dmuszakleavelist[i].Szabadnap.Contains(sdatum))
                                    || (Dmuszakleavelist[i].Cnap != null && Dmuszakleavelist[i].Cnap.Contains(sdatum))
                                    || (Dmuszakleavelist[i].BetegSzabadsag != null && Dmuszakleavelist[i].BetegSzabadsag.Contains(sdatum))
                                    || (Dmuszakleavelist[i].Igazolatlan != null && Dmuszakleavelist[i].Igazolatlan.Contains(sdatum)))
                { donleave++; }
            }
            Dleave.Text = " - D - műszak hiányzók száma: " + donleave;

        }

        private void OverTimeDataLoad() 
        { 
            var overtimeList = _overtimedata.GetOverTimeData();
            int Amuszakot = overtimeList.Where(item => item.sajatMuszak == "A").ToList().Count;
            int Bmuszakot = overtimeList.Where(item => item.sajatMuszak == "B").ToList().Count;
            int Cmuszakot = overtimeList.Where(item => item.sajatMuszak == "C").ToList().Count;
            int Dmuszakot = overtimeList.Where(item => item.sajatMuszak == "D").ToList().Count;

            Aot.Text = " - A - műszak munkavállalóinak túlóra száma: " + Amuszakot;
            Bot.Text = " - B - műszak munkavállalóinak túlóra száma: " + Bmuszakot;
            Cot.Text = " - C - műszak munkavállalóinak túlóra száma: " + Cmuszakot;
            Dot.Text = " - D - műszak munkavállalóinak túlóra száma: " + Dmuszakot;


        }

    }
}

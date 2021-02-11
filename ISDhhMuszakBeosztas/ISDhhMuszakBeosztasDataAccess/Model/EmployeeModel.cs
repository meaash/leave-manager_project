using ISDhhMuszakBeosztasDataAccess.Base;
using System;


namespace ISDhhMuszakBeosztasDataAccess.Model
{
    public class EmployeeModel : Observable
    {

        public int ID { get; set; }

        private string _firstName;
        private string _lastName;
        private string _muszak;
        private DateTime _szuletesidatum;
        private string _email;
        private string _tel;
        private string _munkakor;
        private int _szabadnapokszama;
        private int _extraszabad;
        private int _összkivehetö;
        private string _fullName;


        public string FullName
        {
            get
            {
                _fullName = LastName;
                if (!string.IsNullOrWhiteSpace(FirstName))
                {
                    if (!string.IsNullOrWhiteSpace(_fullName))
                    {
                        _fullName += " ";
                    }
                    _fullName += FirstName;
                }

                return _fullName;

            }
            set
            {
                _fullName = value;
                OnPropertyChanged();
            }
        }
        public int Összkievehetö
        {

            get
            {
                int _összkivehetö = SzabadnapokSzama + CnapokSzama;
                return _összkivehetö;
            }
            set
            {
                _összkivehetö = value;
                OnPropertyChanged();
            }
        }
        public int CnapokSzama
        {
            get
            {
                int cnapok = 0;
                EmpData ea = new EmpData();
                var cnapoklist = ea.GetCNapokData();

                foreach (var item in cnapoklist)
                {
                    if (item.Muszak == Muszak)
                    { cnapok = item.CNapokSzama; }
                    else if (item.Muszak == Muszak)
                    { cnapok = item.CNapokSzama; }
                    else if (item.Muszak == Muszak)
                    { cnapok = item.CNapokSzama; }
                    else if (item.Muszak == Muszak)
                    { cnapok = item.CNapokSzama; }
                }
                return cnapok;
            }
        }
        private int SzabadsagSzamitas()
        {

            int eletkor = DateTime.Now.Year - SzuletesiDatum.Year;
            if (DateTime.Now.Month < SzuletesiDatum.Month || 
               (DateTime.Now.Month == SzuletesiDatum.Month && DateTime.Now.Day < SzuletesiDatum.Day))
                eletkor--;

            int szabadsagokszama = 0;
            if (eletkor < 25)
            { szabadsagokszama = 20; }
            else if (eletkor >= 25 && eletkor < 28)
            { szabadsagokszama = 21; }
            else if (eletkor >= 28 && eletkor < 31)
            { szabadsagokszama = 22; }
            else if (eletkor >= 31 && eletkor < 33)
            { szabadsagokszama = 23; }
            else if (eletkor >= 33 && eletkor < 35)
            { szabadsagokszama = 24; }
            else if (eletkor >= 35 && eletkor < 37)
            { szabadsagokszama = 25; }
            else if (eletkor >= 37 && eletkor < 39)
            { szabadsagokszama = 26; }
            else if (eletkor >= 39 && eletkor < 41)
            { szabadsagokszama = 27; }
            else if (eletkor >= 41 && eletkor < 43)
            { szabadsagokszama = 28; }
            else if (eletkor >= 43 && eletkor < 45)
            { szabadsagokszama = 29; }
            else if (eletkor >= 45)
            { szabadsagokszama = 30; }

            return szabadsagokszama;
        }


        public int SzabadnapokSzama
        {
            get
            {
                _szabadnapokszama = SzabadsagSzamitas();
                return _szabadnapokszama + ExtraSzabad;
            }

        }

        public int ExtraSzabad
        {
            get => _extraszabad;
            set
            {
                _extraszabad = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged();
            }
        }

        public string Muszak
        {
            get => _muszak;
            set
            {
                _muszak = value;
                OnPropertyChanged();
            }
        }

        public DateTime SzuletesiDatum
        {
            get => _szuletesidatum;
            set
            {
                _szuletesidatum = value;
                OnPropertyChanged();
            }
        }

        public string EMail
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Tel
        {
            get => _tel;
            set
            {
                _tel = value;
                OnPropertyChanged();
            }
        }

        public string Munkakor
        {
            get => _munkakor;
            set
            {
                _munkakor = value;
                OnPropertyChanged();
            }
        }

    }
}


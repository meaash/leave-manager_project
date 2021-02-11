using System;


namespace ISDhhMuszakBeosztasDataAccess.Model
{
    public class OverTimeModel
    {
        public int ID { get; set; }
        private string _name;
        private string _sajatMuszak;
        private string _tuloraMuszak;
        private string _munkakor;
        private DateTime _datum;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
            }
        }

        public string sajatMuszak
        {
            get => _sajatMuszak;
            set
            {
                _sajatMuszak = value;
            }
        }
        public string tuloraMuszak
        {
            get => _tuloraMuszak;
            set
            {
                _tuloraMuszak = value;
            }
        }
        public string Munkakor
        {
            get => _munkakor;
            set
            {
                _munkakor = value;
            }
        }
        public DateTime Datum
        {
            get => _datum;
            set
            {
                _datum = value;
            }
        }
    }
}

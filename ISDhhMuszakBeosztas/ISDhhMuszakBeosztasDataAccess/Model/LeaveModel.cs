using System;


namespace ISDhhMuszakBeosztasDataAccess.Model
{
    public class LeaveModel 
    {

        private string _szabadnap;
        private string _cnap;
        private string _betegszabadsag;
        private string _igazolatlan;
        private int _leave_id;
        private string _name;


        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                
            }

        }

        public int Leave_ID
        {
            get => _leave_id;
            set
            {
                _leave_id = value;
                
            }
        }

        public string Szabadnap
        {
            get => _szabadnap;

            set
            {
                _szabadnap = value;
                
            }
        }
        public string Cnap
        {
            get => _cnap;

            set
            {
                _cnap = value;
                
            }
        }
        public string BetegSzabadsag
        {
            get => _betegszabadsag;

            set
            {
                _betegszabadsag = value;
                
            }
        }

        public string Igazolatlan
        {
            get => _igazolatlan;

            set
            {
                _igazolatlan = value;
                
            }
        }
    }
}

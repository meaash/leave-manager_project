using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ISDhhMuszakBeosztasDataAccess.Base
{
    //ez az osztály arra kell, hogyha megváltoztatjuk a propertyt, átírjuk a nevet akkor
    //a listviewban is változtassa meg
    //azért van kiszervezve namsepacbe, hogy máshoz is lehessen használni 
    //ebből származtatom a Modelt
    public class Observable : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

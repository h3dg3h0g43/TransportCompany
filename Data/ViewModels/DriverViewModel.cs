using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TransportComp.Data.Context;
using TransportComp.Data.Models;

namespace TransportComp.Data.ViewModels
{

    public class DriverViewModel : INotifyPropertyChanged
    {
        private List<Driver> drivers = ConnectToDb.db.Drivers.ToList();

        public List<Driver> Drivers
        {
            get => drivers;
            set
            {
                drivers = value;
                OnPropertyChanged();
            }
        }

        private string txtSearch;

        /// <summary>
        /// Конструктор для поиска водителей
        /// </summary>
        public string TxtSearch
        {
            get { return txtSearch; }
            set
            {
                txtSearch = value;
                OnPropertyChanged();
                Drivers = ConnectToDb.db.Drivers.Where(dr =>
                    dr.Name.ToLower().StartsWith(TxtSearch.ToLower())).ToList();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
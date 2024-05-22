using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using TransportComp.Data.Context;
using TransportComp.Data.Models;

namespace TransportComp.Data.ViewModels
{
    public class TruckViewModel : INotifyPropertyChanged
    {
        private List<Truck> trucks = ConnectToDb.db.Trucks.ToList();

        public List<Truck> Trucks
        {
            get => trucks;
            set
            {
                trucks = value;
                OnPropertyChanged();
            }
        }

        private string txtSearch;

        /// <summary>
        /// Конструктор для поиска грузовиков
        /// </summary>
        public string TxtSearch
        {
            get { return txtSearch; }
            set
            {
                txtSearch = value;
                OnPropertyChanged();
                Trucks = ConnectToDb.db.Trucks.Where(tr =>
                    tr.Name.ToLower().StartsWith(TxtSearch.ToLower()) ||
                    tr.Chassis.ToLower().StartsWith(TxtSearch.ToLower())).ToList();
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
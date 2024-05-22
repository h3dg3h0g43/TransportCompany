using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TransportComp.Data;
using TransportComp.Data.Context;
using TransportComp.Data.Core;

namespace TransComp.Data.ViewModels
{

    public class DeleteTruckViewModel : INotifyPropertyChanged
    {
        private readonly int TruckId;

        public DeleteTruckViewModel(int truckId)
        {
            TruckId = truckId;
            var currentDriver = ConnectToDb.db.Drivers.Find(truckId);
        }

        private RelayCommand deleteTruck;

        /// <summary>
        /// Метод удаляет грузовик из списка других водителей
        /// </summary>
        public RelayCommand DeleteTruck
        {
            get
            {
                return deleteTruck ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var truck = ConnectToDb.db.Trucks.Find(TruckId);
                        ConnectToDb.db.Trucks.Remove(truck);

                        ConnectToDb.db.SaveChanges();
                        SupplyMethods.SetMessageToStatusBar($"Грузовик {truck.Name} удалён!");
                    }
                    catch (Exception ex)
                    {
                        SupplyMethods.SetMessageToStatusBar($"Ошибка! {ex.Message}");
                    }
                }));
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

        private void ValidateProperty<T>(T value, string name)
        {
            if (NeedToCheck)
                Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                {
                    MemberName = name
                });
        }

        protected virtual bool OnPropertyChanged<T>(ref T backingField, T value,
            [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<T>.Default.Equals(backingField, value))
                return false;

            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        private bool NeedToCheck = true;
    }
}
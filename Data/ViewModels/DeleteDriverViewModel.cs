using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using TransportComp.Data.Context;
using TransportComp.Data.Core;

namespace TransportComp.Data.ViewModels
{

    public class DeleteDriverViewModel : INotifyPropertyChanged
    {
        private readonly int DriverId;

        public DeleteDriverViewModel(int driverId)
        {
            DriverId = driverId;
            var currentDriver = ConnectToDb.db.Drivers.Find(driverId);
        }

        private RelayCommand deleteDriver;

        /// <summary>
        /// Метод удаляет водителя из списка других водителей
        /// </summary>
        public RelayCommand DeleteDriver
        {
            get
            {
                return deleteDriver ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var driver = ConnectToDb.db.Drivers.Find(DriverId);
                        ConnectToDb.db.Drivers.Remove(driver);
                        ConnectToDb.db.SaveChanges();
                        SupplyMethods.SetMessageToStatusBar($"Водитель {driver.Name} удалён!");
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
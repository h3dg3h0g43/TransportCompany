using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using TransportComp.Data.Context;
using TransportComp.Data.Core;
using TransportComp.Data.Models;
using TransportComp.Services;

namespace TransportComp.Data.ViewModels
{

    public class EditTruckViewModel : INotifyPropertyChanged
    {
        private readonly int TruckId;

        public EditTruckViewModel(int truckId)
        {
            TruckId = truckId;
            var currentTruck = ConnectToDb.db.Trucks.Find(truckId);
            name = currentTruck.Name;
            power = currentTruck.Power;
            chassis = currentTruck.Chassis;
            mileage = currentTruck.Mileage;
            truckImagePath = currentTruck.TruckImagePath;
            depotId = currentTruck.DepotId;
        }

        public IEnumerable<Depot> Depots => ConnectToDb.db.Depots.ToList();

        private string name;

        [Required(ErrorMessage = "Не может быть пустым.")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Должно быть не менее 10 символов.")]
        public string Name
        {
            get { return name; }
            set
            {
                ValidateProperty(value, "Name");
                OnPropertyChanged(ref name, value);
            }
        }

        private int power;

        [Required(ErrorMessage = "Не может быть пустым.")]
        public int Power
        {
            get { return power; }
            set
            {
                ValidateProperty(value, "Power");
                OnPropertyChanged(ref power, value);
            }
        }

        private string chassis;

        [Required(ErrorMessage = "Не может быть пустым.")]
        [StringLength(11, MinimumLength = 3, ErrorMessage = "Должно быть не менее 3 символов.")]
        public string Chassis
        {
            get { return chassis; }
            set
            {
                ValidateProperty(value, "Chassis");
                OnPropertyChanged(ref chassis, value);
            }
        }

        private int mileage;

        [Required(ErrorMessage = "Не может быть пустым.")]
        public int Mileage
        {
            get { return mileage; }
            set
            {
                ValidateProperty(value, "Mileage");
                OnPropertyChanged(ref mileage, value);
            }
        }

        private string truckImagePath;

        [Required(ErrorMessage = "Не может быть пустым.")]
        public string TruckImagePath
        {
            get { return truckImagePath; }
            set
            {
                ValidateProperty(value, "TruckImagePath");
                OnPropertyChanged(ref truckImagePath, value);
            }
        }

        private int depotId;

        [Required(ErrorMessage = "Не может быть пустым.")]
        public int DepotId
        {
            get { return depotId; }
            set
            {
                ValidateProperty(value, "DepotId");
                OnPropertyChanged(ref depotId, value);
            }
        }

        private RelayCommand editTruck;

        /// <summary>
        /// Метод вносит изменения грузовика
        /// </summary>
        public RelayCommand EditTruck
        {
            get
            {
                return editTruck ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var truck = ConnectToDb.db.Trucks.Find(TruckId);
                        truck.Name = name;
                        truck.Power = power;
                        truck.Chassis = chassis;
                        truck.Mileage = mileage;
                        truck.TruckImagePath = truckImagePath;
                        truck.DepotId = depotId;

                        ConnectToDb.db.SaveChanges();
                        UploadPhotos.CopyImage();
                        SupplyMethods.SetMessageToStatusBar($"Грузовик {truck.Name} обновлён!");
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
        public RelayCommand AddImage => new RelayCommand(obj => TruckImagePath = UploadPhotos.GetImagePath());
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using TransportComp.Data.Models;
using TransportComp.Data.Context;
using TransportComp.Data.Core;
using TransportComp.Services;

namespace TransportComp.Data.ViewModels
{

    public class AddTruckViewModel : INotifyPropertyChanged
    {
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

        private RelayCommand addTruck;

        /// <summary>
        /// Метод добавляет грузовик в список других грузовиков
        /// </summary>s
        public RelayCommand AddTruck
        {
            get
            {
                return addTruck ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var truck = new Truck()
                        {
                            Name = name,
                            Power = power,
                            Chassis = chassis,
                            Mileage = mileage,
                            TruckImagePath = truckImagePath,
                            DepotId = depotId
                        };
                        if (truck != null)
                        {
                            ConnectToDb.db.Trucks.Add(truck);
                            ConnectToDb.db.SaveChanges();
                            UploadPhotos.CopyImage();
                            SupplyMethods.SetMessageToStatusBar($"Грузовик {truck.Name} добавлен!");

                            NeedToCheck = false;
                            this.Name = string.Empty;
                            this.Power = 0;
                            this.Chassis = string.Empty;
                            this.Mileage = 0;
                            this.TruckImagePath = string.Empty;
                            this.DepotId = default;
                            NeedToCheck = true;
                        }
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
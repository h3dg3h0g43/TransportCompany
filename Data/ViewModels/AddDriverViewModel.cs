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

    public class AddDriverViewModel : INotifyPropertyChanged
    {
        public IEnumerable<Truck> Trucks => ConnectToDb.db.Trucks.ToList();

        private string name;

        [Required(ErrorMessage = "Не может быть пустым.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Должно быть не менее 5 символов.")]
        public string Name
        {
            get { return name; }
            set
            {
                ValidateProperty(value, "Name");
                OnPropertyChanged(ref name, value);
            }
        }

        private int age;

        [Required(ErrorMessage = "Не может быть пустым.")]
        public int Age
        {
            get { return age; }
            set
            {
                ValidateProperty(value, "Age");
                OnPropertyChanged(ref age, value);
            }
        }

        private string phone;

        [Required(ErrorMessage = "Не может быть пустым.")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Должно быть не менее 11 символов.")]
        public string Phone
        {
            get { return phone; }
            set
            {
                ValidateProperty(value, "Phone");
                OnPropertyChanged(ref phone, value);
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

        private string driverImagePath;

        [Required(ErrorMessage = "Не может быть пустым.")]
        public string DriverImagePath
        {
            get { return driverImagePath; }
            set
            {
                ValidateProperty(value, "DriverImagePath");
                OnPropertyChanged(ref driverImagePath, value);
            }
        }

        private int truckId;

        [Required(ErrorMessage = "Не может быть пустым.")]
        public int TruckId
        {
            get { return truckId; }
            set
            {
                ValidateProperty(value, "TruckId");
                OnPropertyChanged(ref truckId, value);
            }
        }

        private RelayCommand addDriver;

        /// <summary>
        /// Метод добавляет водителя в список других водителей
        /// </summary>
        public RelayCommand AddDriver
        {
            get
            {
                return addDriver ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var driver = new Driver()
                        {
                            Name = name,
                            Age = age,
                            Phone = phone,
                            Mileage = mileage,
                            DriverImagePath = driverImagePath,
                            TruckId = truckId
                        };
                        if (driver != null)
                        {
                            ConnectToDb.db.Drivers.Add(driver);
                            ConnectToDb.db.SaveChanges();
                            UploadPhotos.CopyImage();
                            SupplyMethods.SetMessageToStatusBar($"Водитель {driver.Name} добавлен!");

                            NeedToCheck = false;
                            this.Name = string.Empty;
                            this.Age = 0;
                            this.Phone = string.Empty;
                            this.Mileage = 0;
                            this.DriverImagePath = string.Empty;
                            this.TruckId = default;
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
        public RelayCommand AddImage => new RelayCommand(obj => DriverImagePath = UploadPhotos.GetImagePath());
    }
}
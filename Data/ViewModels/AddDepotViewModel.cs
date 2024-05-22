using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TransportComp.Data.Context;
using TransportComp.Data.Core;
using TransportComp.Data.Models;

namespace TransportComp.Data.ViewModels
{
    internal class AddDepotViewModel : INotifyPropertyChanged
    {
        private string city;

        [Required(ErrorMessage = "Не может быть пустым.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Должно быть не менее 5 символов.")]
        public string City
        {
            get { return city; }
            set
            {
                ValidateProperty(value, "City");
                OnPropertyChanged(ref city, value);
            }
        }

        private string address;

        [Required(ErrorMessage = "Не может быть пустым.")]
        public string Address
        {
            get { return address; }
            set
            {
                ValidateProperty(value, "Address");
                OnPropertyChanged(ref address, value);
            }
        }

        private RelayCommand addDepot;

        /// <summary>
        /// Метод добавляет депо в список других депо
        /// </summary>
        public RelayCommand AddDepot
        {
            get
            {
                return addDepot ?? (new RelayCommand(obj =>
                {
                    try
                    {
                        var depot = new Depot()
                        {
                            City = city,
                            Address = address
                        };
                        if (depot != null)
                        {
                            ConnectToDb.db.Depots.Add(depot);
                            ConnectToDb.db.SaveChanges();
                            SupplyMethods.SetMessageToStatusBar($"Депо {depot.City} добавлено!");

                            NeedToCheck = false;
                            this.City = string.Empty;
                            this.Address = string.Empty;
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
    }
}
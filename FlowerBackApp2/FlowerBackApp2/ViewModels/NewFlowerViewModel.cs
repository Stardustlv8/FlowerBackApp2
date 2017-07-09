using FlowerBackApp2.Models;
using FlowerBackApp2.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FlowerBackApp2.ViewModels
{
    public class NewFlowerViewModel:INotifyPropertyChanged
    {
        #region Attributes
        private ApiService apiService;
        private NavigationService navigationService;
        private DialogService dialogService;
        private string description;
        private decimal price;
        private bool isRunning;
        private bool isEnable;
        #endregion

        #region Properties
        public string Description
        {
            set
            {
                if(description != value)
                {
                    description= value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Description"));
                }
            }
            get
            {
                return description;
            }
        }

        public decimal Price
        {
            set
            {
                if (price != value)
                {
                    price = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
                }
            }
            get
            {
                return price;
            }
        }

        public bool IsRunning
        {
            set
            {
                if(isRunning != value)
                {
                    isRunning = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRunning"));
                }
            }
            get
            {
                return isRunning;
            }
        }

        public bool IsEnable
        {
            set
            {
                if (isEnable != value)
                {
                    isEnable = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsEnable"));
                }
            }
            get
            {
                return isEnable;
            }
        }
        #endregion

        #region Constructors
        public NewFlowerViewModel()
        {
            IsEnable = true;
            apiService = new ApiService();
            navigationService = new NavigationService();
            dialogService = new DialogService();
        }
        #endregion

        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands
        public ICommand NewFlowerCommand { get { return new RelayCommand(NewFlower); } }

        private async void NewFlower()
        {
            if (string.IsNullOrEmpty(Description))
            {
                await dialogService.ShowMessage("Error", "You must enter a description");
                return;
            }
            if (Price <= 0)
            {
                await dialogService.ShowMessage("Error", "You must enter a number grather than zero in price...");
            }

            var flower = new Flower
            {
                Description = Description,
                Price = Price
            };

            IsRunning = true;
            IsEnable = false;

            var response = await apiService.Post("http://flowerback220170702014717.azurewebsites.net/",
                "/api", "/Flowers", flower);

            IsRunning = false;
            IsEnable = true;

            if (!response.IsSuccess)
            {
                await dialogService.ShowMessage("Error", response.Message);
                return;
            }
            await navigationService.Back();
        }
        #endregion

    }
}

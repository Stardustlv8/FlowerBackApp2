using FlowerBackApp2.Services;
using System.Collections.ObjectModel;
using System;
using FlowerBackApp2.Models;
using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace FlowerBackApp2.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        private ApiService apiService;
        private NavigationService navigationService;
        private DialogService dialogService;
        #endregion

        #region Properties
        public ObservableCollection<FlowerItemViewModel> Flowers { get; set; }
        public NewFlowerViewModel NewFlower { get; set; }
        
        #endregion

        #region Constructors
        public MainViewModel()
        {
            //Singleton
            instance = this;

            //Service
            apiService = new ApiService();
            navigationService = new NavigationService();

            //ViewModels
            Flowers = new ObservableCollection<FlowerItemViewModel>();

            //Load Data
            LoadFlowers();
        }
        #endregion

        #region Methods

        private async void LoadFlowers()
        {
            var respose = await apiService.Get<Flower>("http://flowerback220170702014717.azurewebsites.net/", 
                "/api","/Flowers");
            if (!respose.IsSuccess)
            {
                await dialogService.ShowMessage("Error", respose.Message);
                return;
            }
            ReloadFlowers((List<Flower>)respose.Result);
        }

        private void ReloadFlowers(List<Flower> flowers)
        {
            Flowers.Clear();
            foreach (var flower in flowers)
            {
                Flowers.Add(new FlowerItemViewModel
                {
                    Description = flower.Description,
                    FlowerId = flower.FlowerId,
                    Price = flower.Price,
                });

            }
        }
        #endregion

        #region Commands
        public ICommand AddFlowerCommand { get { return new RelayCommand(AddFlower); } }

        private async void AddFlower()
        {
            await navigationService.Navigate("NewFlowerPage");
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if(instance == null)
            {
                instance = new MainViewModel();
            }
            return instance;
        }

        #endregion

    }
}

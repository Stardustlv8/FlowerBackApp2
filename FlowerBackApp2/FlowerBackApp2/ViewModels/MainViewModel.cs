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
        private NavigationService navigationService { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<FlowerItemViewModel> Flowers { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
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
            var flowers = await apiService.Get<Flower>("http://flowerback220170702014717.azurewebsites.net/", 
                "/api","/Flowers");

            ReloadFlowers(flowers);
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

    }
}

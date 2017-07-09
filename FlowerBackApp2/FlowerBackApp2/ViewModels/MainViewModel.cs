using FlowerBackApp2.Services;
using System.Collections.ObjectModel;
using System;
using FlowerBackApp2.Models;
using System.Collections.Generic;

namespace FlowerBackApp2.ViewModels
{
    public class MainViewModel
    {
        #region Attributes
        private ApiService apiService;
        #endregion

        #region Properties
        public ObservableCollection<FlowerItemViewModel> Flowers { get; set; }
        #endregion

        #region Constructors
        public MainViewModel()
        {
            apiService = new ApiService();
            Flowers = new ObservableCollection<FlowerItemViewModel>();
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
        #endregion

    }
}

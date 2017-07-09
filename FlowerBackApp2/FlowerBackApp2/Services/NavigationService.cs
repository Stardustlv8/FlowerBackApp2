using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlowerBackApp2.Pages;

namespace FlowerBackApp2.Services
{
    public class NavigationService
    {
        public async Task Navigate(string pageName)
        {
            switch (pageName)
            {
                case "NewFlowerPage":
                    await App.Current.MainPage.Navigation.PushAsync(new NewFlowerPage());
                    break;
                default:
                    break;
            }

        }
        public async Task Back()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}

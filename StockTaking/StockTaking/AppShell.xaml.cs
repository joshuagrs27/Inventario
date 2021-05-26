using StockTaking.ViewModels;
using StockTaking.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace StockTaking
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            //Registering Routes for Special Pages
            Routing.RegisterRoute(nameof(NewCompanyPage), typeof(NewCompanyPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}

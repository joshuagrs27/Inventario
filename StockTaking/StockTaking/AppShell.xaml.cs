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
     

            //Registering Routes for Special Pages
            Routing.RegisterRoute(nameof(NewCompanyPage), typeof(NewCompanyPage));
            Routing.RegisterRoute(nameof(ValidationPage), typeof(ValidationPage));
            Routing.RegisterRoute(nameof(NewProductPage), typeof(NewProductPage));
            Routing.RegisterRoute(nameof(ProductDetailsPage), typeof(ProductDetailsPage));
            Routing.RegisterRoute(nameof(NewTransactionPage), typeof(NewTransactionPage));
            Routing.RegisterRoute(nameof(EditTransactionPage), typeof(EditTransactionPage));
            Routing.RegisterRoute(nameof(NewUserPage), typeof(NewUserPage));
            Routing.RegisterRoute(nameof(UserDetailsPage), typeof(UserDetailsPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//StartPage");
        }
    }
}

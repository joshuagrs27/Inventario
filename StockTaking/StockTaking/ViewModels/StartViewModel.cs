using StockTaking.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class StartViewModel:BindableObject
    {
        //Constructor
        public StartViewModel()
        {
            //NEWCOMPANY = new Command(GotoNewCompanyPage_F);
            CHOOSECOMPANY = new Command(GotoChooseCompanyPage_F);
        }

        //New Company Command(Triggered by Pressing the Button)
        //public Command NEWCOMPANY { get; }
        //Choose Company Command
        public Command CHOOSECOMPANY { get; }

        //Go to New Company Function
        public void GotoNewCompanyPage_F()
        {


        }
        // Go to Choose Company Page
        public async void GotoChooseCompanyPage_F()
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync($"//{nameof(CompaniesPage)}");
        }
    }
}

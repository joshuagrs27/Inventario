using StockTaking.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class CompaniesViewModel : BindableObject
    {
        //Constructor
        public CompaniesViewModel()
        {
            ADDNEWCOMPANY = new Command(GotoNewCompanyPage_F);
        }

        //Command for Adding A new Company
        public Command ADDNEWCOMPANY {get;}
        //

        //Go to New Company Function
        public async void GotoNewCompanyPage_F()
        {
            //Go to new Company Page
            await Shell.Current.GoToAsync(nameof(NewCompanyPage));

        }
    }
}

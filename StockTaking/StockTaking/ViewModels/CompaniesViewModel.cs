using StockTaking.Models;
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
            ItemTapped = new Command<Company>(OnCompanySelected_F);
        }

        public async void OnAppearing()
        {
            //Get A List of All the Companies
            CompanyCollection = await App.Database.GetCompaniesAsync();
        }

        //--------------Commands---------------\\
        //Command for Adding A new Company
        public Command ADDNEWCOMPANY {get;}
        //Command for Chosen Company
        public Command<Company> ItemTapped { get; }
        //--------------Getters and Setters---------------\\
        private List<Company> companyCollection = new List<Company>();
        public List<Company> CompanyCollection
        {
            get => companyCollection;
            set
            {
                companyCollection = value;
                OnPropertyChanged();
            }
        }


        //Go to New Company Function
        public async void GotoNewCompanyPage_F()
        {
            //Go to new Company Page
            await Shell.Current.GoToAsync(nameof(NewCompanyPage));

        }
        //
        private async void OnCompanySelected_F(Company company)
        {
            App.CurrentCompany = company;
            //Go to new Validation Page
            await Shell.Current.GoToAsync(nameof(ValidationPage));
        }
        //
    }
}

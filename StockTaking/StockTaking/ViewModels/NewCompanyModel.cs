using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class NewCompanyModel:BindableObject
    {
        //Conscrutor
        public NewCompanyModel()
        {
            SAVE_COMPANY_COMMAND = new Command(SaveCompany_F);
            CANCEL_COMMAND = new Command(Cancel_F);
        }

        //---------------------------Commands-----------------------\\
        //Save Company Command
        public Command SAVE_COMPANY_COMMAND { get; }
        //Cancel Command
        public Command CANCEL_COMMAND { get; }

        //---------------------------Getters and Setters-----------------------\\
        //Company Name Field
        private string companyName;
        public string CompanyName
        {
            get => companyName;
            set
            {
                companyName = value;
                OnPropertyChanged();
            }
        }
        //Company Description Field
        private string companyDescription;
        public string CompanyDescription
        {
            get => companyDescription;
            set
            {
                companyDescription = value;
                OnPropertyChanged();
            }
        }
        //Company Adminstrator Username Field
        private string companyAdminUsername;
        public string CompanyAdminUsername
        {
            get => companyAdminUsername;
            set
            {
                companyAdminUsername = value;
                OnPropertyChanged();
            }
        }
        //Company Administrator Password Field
        private string companyAdminUserpassword;
        public string CompanyAdminUserpassword
        {
            get => companyAdminUserpassword;
            set
            {
                companyAdminUserpassword = value;
                OnPropertyChanged();
            }
        }
        //Confirm Password Field
        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword = value;
                OnPropertyChanged();
            }
        }


        //---------------------------CUSTOM FUNCTIONS-----------------------\\
        //Save Company Function
        public async void SaveCompany_F()
        {
            //Validate Fields
            bool answer = await Validate_F();
            if (answer)
            {
                //Create a new Company Object
                Company newCompanyObj = new Company();
                //Copy local properties to the company object
                newCompanyObj.Company_Name = CompanyName;
                newCompanyObj.Company_Description = CompanyDescription;
                newCompanyObj.Company_Admin_Username = CompanyAdminUsername;
                newCompanyObj.Company_Admin_Password = CompanyAdminUserpassword;
                //Check if Company Already Exists
                bool checker =  await CheckDuplicate_F();
                //
                if (checker)
                {
                    //Save To Database
                    await App.Database.SaveCompanyAsync(newCompanyObj);
                    //
                    await App.Current.MainPage.DisplayAlert("Success", "Company Successfully Saved", "Ok");
                    //
                    // This will pop the current page off the navigation stack
                    await Shell.Current.GoToAsync("..");
                }

            }
      
        }

        //Cancel Function
        public async void Cancel_F()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        //Check for DUPLICATE companies
        public async Task<bool> CheckDuplicate_F()
        {
            bool pass = true;
            //Create a list that will store all the Company objects from the Database
            List<Company> collection = await App.Database.GetCompaniesAsync();
            foreach(var company in collection)
            {
                //Compare All Companies in the Database to the Current CompanyName
                if(company.Company_Name == CompanyName)
                {
                    pass = false;
                    await App.Current.MainPage.DisplayAlert("Alert", "Company Already Exists", "Ok");
                    return pass;
                }
            }
            //
            return pass;
        }


        public async Task<bool> Validate_F()
        {
            bool pass = true;
            //
            if(CompanyName == null)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Company Name Needed", "Ok");
                return pass;
            }
            if(CompanyDescription == null)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Company Description Name Needed", "Ok");
                return pass;
            }
            if(CompanyAdminUsername == null)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Administrator Username Needed", "Ok");
                return pass;
            }
            if(CompanyAdminUserpassword == null)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Administrator password Needed", "Ok");
                return pass;
            }
            if(ConfirmPassword == null)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Confirm Password", "Ok");
                return pass;
            }
            if(CompanyAdminUserpassword != ConfirmPassword)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Passwords don't Match!", "Ok");
                return pass;
            }
            //
            return pass;
        }

    }
}

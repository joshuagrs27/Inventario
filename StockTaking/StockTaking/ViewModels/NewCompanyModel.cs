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
        //Constructor
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

                if (checker)
                {
                    //Save To Database
                    await App.Database.SaveCompanyAsync(newCompanyObj);
                    //Get Back the Company From The Database Since it will have a valid ID
                    newCompanyObj = await App.Database.SearchCompanyAsync(newCompanyObj.Company_Name);
                    //Create a User Object to put the Admin In
                    User newUserObj = new User();
                    newUserObj.User_Company_ID = newCompanyObj.Company_Id;
                    newUserObj.User_Name = newCompanyObj.Company_Admin_Username;
                    newUserObj.User_Password = newCompanyObj.Company_Admin_Password;
                    newUserObj.User_Permission = "Administrador";
                    //Save the Administrator to the Database
                    await App.Database.SaveUserAsync(newUserObj);
                    //
                    await App.Current.MainPage.DisplayAlert("Exito", "Empresa guardada", "Ok");
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
                    await App.Current.MainPage.DisplayAlert("Alerta", "La empresa ya existe", "Ok");
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
                await App.Current.MainPage.DisplayAlert("Alerta", "La empresa necesita nombre", "Ok");
                return pass;
            }
            if(CompanyDescription == null)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "La empresa necesita descripcion", "Ok");
                return pass;
            }
            if(CompanyAdminUsername == null)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Necesitas Username de Administrador", "Ok");
                return pass;
            }
            if(CompanyAdminUserpassword == null)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Necesitas un password para Administrador", "Ok");
                return pass;
            }
            if(ConfirmPassword == null)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Confirmar Password", "Ok");
                return pass;
            }
            if(CompanyAdminUserpassword != ConfirmPassword)
            {
                pass = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Passwords no coinciden", "Ok");
                return pass;
            }
            //
            return pass;
        }

    }
}

﻿using StockTaking.Models;
using StockTaking.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class ValidationViewModel:BindableObject
    {
        //Constructor
        public ValidationViewModel()
        {
            LOGIN_COMMAND = new Command(Trylogin_F);
            CANCEL_COMMAND = new Command(Cancel_F);
            DELETE_COMPANY_COMMAND = new Command(Delete_F);
        }
        //
        public void OnAppearing()
        {
            ValidCompany = App.CurrentCompany;
        }

        //--Commands--\\
        public Command LOGIN_COMMAND { get; }
        public Command CANCEL_COMMAND { get; }
        public Command DELETE_COMPANY_COMMAND { get; }
        //------------------Getters and Setters--------------\\
        public Company validCompany = new Company();
        public Company ValidCompany
        {
            get => validCompany;
            set
            {
                validCompany = value;
                OnPropertyChanged();
            }
        }
        //Username field
        private string username;
        public string Username
        {
            get => username;
            set
            {
                username = value;
                OnPropertyChanged();
            }
        }
        //Password field
        private string userpassword;
        public string Userpassword
        {
            get => userpassword;
            set
            {
                userpassword = value;
                OnPropertyChanged();
            }
        }
        //--------Custom Function---------\\
        public async void Trylogin_F()
        {
            bool ans = await Valid_F();
            if (ans)
            {
                await App.Current.MainPage.DisplayAlert("Exito", "Logueado", "Ok");
                //
                await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
            }
            else{
                await App.Current.MainPage.DisplayAlert("Alert", "User no existe", "Intentalo de nuevo");
            }
        }
        public async void Cancel_F()
        {

            //ValidCompany = await App.Database.SearchCompanyAsync("Jokes LTD");
            
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
            
        }

        public async Task<bool> Valid_F()
        {
            bool pass = false;
            //Check for non-admin Users
            List<User> usersCollection = await App.Database.GetUsersAsync();
            //
            foreach (var user in usersCollection)
            {
                if (Username == user.User_Name)
                {
                    if (Userpassword == user.User_Password)
                    {
                        if (user.User_Company_ID == ValidCompany.Company_Id)
                        {
                           
                            App.CurrentUser = user;
                            pass = true;
                            return pass;
                           
                           
                        }
                    }
                }
            }

            return pass;
        }
        public async Task<bool> Valid_F2()
        {
            bool pass = false;
            //Check for non-admin Users
            List<User> usersCollection = await App.Database.GetUsersAsync();
            //
            foreach (var user in usersCollection)
            {
                if (Username == user.User_Name)
                {
                    if (Userpassword == user.User_Password)
                    {
                        if (user.User_Company_ID == ValidCompany.Company_Id)
                        {
                            if (user.User_Permission == "ADMIN-RIGHTS")
                            {
                                App.CurrentUser = user;
                                pass = true;
                                return pass;
                            }

                        }
                    }
                }
            }

            return pass;
        }
        //
        public async void Delete_F()
        {
            bool ans = await Valid_F2();
            if (ans)
            {
                
                //Telling the database to delete the product
                await App.Database.DeleteCompanyAsync(ValidCompany);
                //
                App.CurrentCompany = null;
                //
                await App.Current.MainPage.DisplayAlert("Exito", ValidCompany.Company_Name + " Borrado", "Atras");
            }
            else{
                await App.Current.MainPage.DisplayAlert("Alerta", "Tu no tienes permiso", "Atras");
            }
           
            //
            Back_F();

        }
        //
        //Function when the back button is clicked
        public async void Back_F()
        {
            await Shell.Current.GoToAsync("..");
        }


    }
}

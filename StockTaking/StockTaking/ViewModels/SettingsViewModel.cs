using StockTaking.Models;
using StockTaking.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class SettingsViewModel:BindableObject
    {
        //Only an Administrator can create Users
        //Constructor
        public SettingsViewModel()
        {
            ADD_NEW_USER_COMMAND = new Command(AddUser_F);
            ItemTapped = new Command<User>(OnUserSelected_F);
        }
        //
        public Command<User> ItemTapped { get; }
        //
        public async void OnAppearing()
        {
            UserA = App.CurrentUser;
            if (App.CurrentUser.User_Permission == "ADMIN-RIGHTS")
            {
                //oPEN ALL RIGHTS
            }
            if(App.CurrentUser.User_Permission == "PRODUCTS-RIGHTS")
            {

            }
            if(App.CurrentUser.User_Permission == "TRANSACTION-RIGHTS")
            {

            }
            var tempCollection = await App.Database.GetUsersAsync();
            var otherCollection = new List<User>();
            foreach(var user in tempCollection)
            {
                if(user.User_Company_ID == App.CurrentCompany.Company_Id)
                {
                    otherCollection.Add(user);
                }
            }
            UserList = otherCollection;
        }
        /// <summary>
        private User userA;
        public User UserA
        {
            get => userA;
            set
            {
                userA = value;
                OnPropertyChanged();
            }
        }
        private List<User> usersList;
        public List<User> UserList
        {
            get => usersList;
            set
            {
                usersList = value;
                OnPropertyChanged();
            }
        }
        /// </summary>
        //commadns
        public Command ADD_NEW_USER_COMMAND { get; }
        //ADMIN-FUNCTIONS
        public async void AddUser_F()
        {
            //
            if (App.CurrentUser.User_Permission == "ADMIN-RIGHTS")
            {
                //Go to new Company Page
                await Shell.Current.GoToAsync(nameof(NewUserPage));
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "El administrado añade los usuarios", "Ok");
            }
        }
        //
        public async void OnUserSelected_F(User user)
        {
            if (App.CurrentUser.User_Permission == "ADMIN-RIGHTS")
            {
                //Set the Global Current Product
                App.SelectedUser = user;
                //Go to Product Details Page
                await Shell.Current.GoToAsync(nameof(UserDetailsPage));
                //
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Tu no tienes permiso", "Atras");
            }

        }
    }
}

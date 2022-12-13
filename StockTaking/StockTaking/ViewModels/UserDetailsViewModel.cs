using StockTaking.Models;
using StockTaking.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class UserDetailsViewModel:BindableObject
    {
        //Constructor
        public UserDetailsViewModel()
        {
            BACK_COMMAND = new Command(Back_F);
            //
            DELETE_PRODUCT_COMMAND = new Command(DeleteProduct_F);
        }
        //
        public Command BACK_COMMAND { get; }
        public Command DELETE_PRODUCT_COMMAND { get; }
        //
        public  void OnAppearing()
        {
            User_A = App.SelectedUser;
            //
            UserName = User_A.User_Name;
            UserPassword = User_A.User_Password;
            UserPermission = User_A.User_Permission;

        }
        //
        public User user_A = new User();
        public User User_A
        {
            get => user_A;
            set
            {
               user_A = value;
               OnPropertyChanged();
            }
        }
        //
        public string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        //
        public string userPassword;
        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = value;
                OnPropertyChanged();
            }
        }
        //
        public string userPermission;
        public string UserPermission
        {
            get => userPermission;
            set
            {
                userPermission = value;
                OnPropertyChanged();
            }
        }
        //
        public async void DeleteProduct_F()
        {
            if(App.CurrentUser.User_Name == User_A.User_Name)
            {
                await App.Current.MainPage.DisplayAlert("Alerta","Nopuedes borrar usuario", "Atras");
            }
            else
            {
                //Telling the database to delete the product
                await App.Database.DeleteUserAsync(User_A);
                //
                //App.CurrentUser = null;
                //
                await App.Current.MainPage.DisplayAlert("Alerta", User_A.User_Name + " Borrado", "Atras");
                //
            }

            Back_F();

        }
        //Function when the back button is clicked
        public async void Back_F()
        {
            await Shell.Current.GoToAsync("..");
        }


    }
}

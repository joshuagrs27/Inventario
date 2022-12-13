using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class NewUserViewModel:BindableObject
    {
        //
        public NewUserViewModel()
        {
            SAVE_USER_COMMAND = new Command(SaveUser_F);
            CANCEL_COMMAND = new Command(Cancel_F);
        }
        public void OnAppearing()
        {
            MakePickerData_F();
        }
        ////////////////////////////////
        public Command SAVE_USER_COMMAND { get; }
        public Command CANCEL_COMMAND { get; }

        ////////////////////////////////
        public IList<UPermissions> permissionTypes;
        public IList<UPermissions> PermissionTypes
        {
            get => permissionTypes;
            set
            {
                permissionTypes = value;
                OnPropertyChanged();
            }
        }
        //
        private UPermissions chosenPermission;
        public UPermissions ChosenPermission
        {
            get => chosenPermission;
            set
            {
                chosenPermission = value;
                OnPropertyChanged();
            }
        }
        //
        private string userName;
        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                OnPropertyChanged();
            }
        }
        private string userPassword;
        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = value;
                OnPropertyChanged();
            }
        }
        ////////////////////////////////////////
        public void MakePickerData_F()
        {
            IList<UPermissions> tempValues = new List<UPermissions>();
            tempValues.Add(new UPermissions { P_Value = "PRODUCTS-RIGHTS" });
            tempValues.Add(new UPermissions { P_Value = "TRANSACTION-RIGHTS" });
            PermissionTypes = new ObservableCollection<UPermissions>();
            PermissionTypes = tempValues;
        }
        //
        public async void SaveUser_F()
        {
            bool ans = await Validate_F();
            //
            if (ans)
            {
                User userObj = new User();
                //
                userObj.User_Company_ID = App.CurrentCompany.Company_Id;
                userObj.User_Name = UserName;
                userObj.User_Password = UserPassword;
                userObj.User_Permission = ChosenPermission.P_Value;
                //
                await App.Database.SaveUserAsync(userObj);
                //
                await App.Current.MainPage.DisplayAlert("Exito", "User guardado", "ok");
                //
                Cancel_F();
            }

        }
        //
        public async Task<bool> Validate_F()
        {
            bool ans2 = true;
            //
            if (ChosenPermission == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Elige permiso", "Atras");
                return ans2;
            }
            if (UserName == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Elige nombre de username", "Atras");
                return ans2;
            }
            if (UserPassword == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Elige User Password", "Atras");
                return ans2;
            }
            //
            return ans2;
        }
        //Custom Function
        public async void Cancel_F()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

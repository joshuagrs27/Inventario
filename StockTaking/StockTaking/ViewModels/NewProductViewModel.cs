using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class NewProductViewModel:BindableObject
    {
        //Constructor
        public NewProductViewModel()
        {
            SAVE_PRODUCT_COMMAND = new Command(SaveProduct_F);
            CANCEL_COMMAND = new Command(Cancel_F);
        }

        //------COMMANDS-----\\
        public Command SAVE_PRODUCT_COMMAND { get; }
        public Command CANCEL_COMMAND { get; }

        //------Getters and Setters-----\\
        public string productName;
        public string ProductName
        {
            get => productName;
            set
            {
                productName = value;
                OnPropertyChanged();
            }
        }
        //
        public string productDescription;
        public string ProductDescription
        {
            get => productDescription;
            set
            {
                productDescription = value;
                OnPropertyChanged();
            }
        }
        //------Custom Functions-----\\
        public async void Cancel_F()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        //Function for Saving Products

        public async void SaveProduct_F()
        {
            bool ans = await Validate_F();
            if (ans)
            {
                Product productObj = new Product();
                //
                productObj.Product_Name = ProductName;
                productObj.Product_Description = ProductDescription;
                productObj.Product_CompanyID = App.CurrentCompany.Company_Id;
                productObj.Product_Current_Stock = 0;
                productObj.Product_Level = "EMPTY";
                //Save the Administrator to the Database
                await App.Database.SaveProductAsync(productObj);
                //
                await App.Current.MainPage.DisplayAlert("Success", "Product Successfully Saved", "Ok");
                //
                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");

            }

        }

        public async Task<bool> Validate_F()
        {
            bool ans2 = true;
            if(ProductName == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Company Name Needed", "Ok");
                return ans2;
            }
            if(ProductDescription == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Company Description Needed", "Ok");
            }
            return ans2;
        }


    }
}

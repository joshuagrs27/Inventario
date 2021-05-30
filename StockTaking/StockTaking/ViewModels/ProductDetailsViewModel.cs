using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class ProductDetailsViewModel:BindableObject
    {
        //Constructor
        public ProductDetailsViewModel()
        {
            //
            BACK_COMMAND = new Command(Back_F);
            //
            SAVE_PRODUCT_COMMAND = new Command(SaveProduct_F);
            //
            EDIT_PRODUCT_COMMAND = new Command(EditProduct_F);
            //
            DELETE_PRODUCT_COMMAND = new Command(DeleteProduct_F);
        }

        public async void OnAppearing()
        {
            Product_A = App.CurrentProduct;
            //
            ProductName = Product_A.Product_Name;
            ProductDescription = Product_A.Product_Description;

        }


        //---------------------\\
        public Command BACK_COMMAND { get; }
        public Command SAVE_PRODUCT_COMMAND { get; }
        public Command EDIT_PRODUCT_COMMAND { get; }
        public Command DELETE_PRODUCT_COMMAND { get; }
        //------Gettters nad Setters-----\\
        //
        public Product product_A = new Product();
        public Product Product_A
        {
            get => product_A;
            set
            {
                product_A = value;
                OnPropertyChanged();
            }
        }


        //
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
        public string productCurrentLevel;
        public string ProductCurrentLevel
        {
            get => productCurrentLevel;
            set
            {
                productCurrentLevel = value;
                OnPropertyChanged();
            }
        }
        public int productCurrentStock;
        public int ProductCurrentStock
        {
            get => productCurrentStock;
            set
            {
                productCurrentStock = value;
                OnPropertyChanged();
            }
        }
        public int productCompanyID;
        public int ProductCompanyID
        {
            get => productCompanyID;
            set
            {
                productCompanyID = value;
                OnPropertyChanged();
            }
        }
        public bool edit_Enable = false;
        public bool Edit_Enable
        {
            get => edit_Enable;
            set
            {
                edit_Enable = value;
                OnPropertyChanged();
            }
        }

        //////
        //Function when we delete the Product
        public async void DeleteProduct_F()
        {
            await App.Database.DeleteProductAsync(Product_A);
            //
            App.CurrentProduct = null;
            //
            await App.Current.MainPage.DisplayAlert("Alert", Product_A.Product_Name + " Deleted", "Back");
            //
            Back_F();

        }
        //Function when the back button is clicked
        public async void Back_F()
        {
            await Shell.Current.GoToAsync("..");
        }
        //Function when Edit button is clicked
        public async void EditProduct_F()
        {
            Edit_Enable = !Edit_Enable;
        }
        //Function when Save Button is clicked
        public async void SaveProduct_F()
        {
            //
            bool ans = await Validate_F();
            if (ans)
            {
                Product productObj = new Product();
                //
                productObj = Product_A;
                productObj.Product_Name = ProductName;
                productObj.Product_Description = ProductDescription;
                //Save the Administrator to the Database
                await App.Database.EditProductAsync(productObj);
                //
                await App.Current.MainPage.DisplayAlert("Success", "Product Successfully Saved", "Ok");
                //
                // This will pop the current page off the navigation stack
                await Shell.Current.GoToAsync("..");

            }
            //

        }
        public async Task<bool> Validate_F()
        {
            bool ans2 = true;
            if (ProductName == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Company Name Needed", "Ok");
                return ans2;
            }
            if (ProductDescription == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Company Description Needed", "Ok");
            }
            return ans2;
        }


    }
}

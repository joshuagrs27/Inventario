using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class ProductDetailsViewModel:BindableObject
    {
        //Constructor
        public ProductDetailsViewModel()
        {

        }

        public async void OnAppearing()
        {
            Product_A = App.CurrentProduct;
        }


        //---------------------\\
        public Command BACK_COMMAND { get; }
        public Command SAVE_PRODUCT_COMMAND { get; }
        public Command EDIT_PRODUCT_COMMAND { get; }
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

        //////
        //Function when we delete the Product
        public async void DeleteProduct_F()
        {
            await App.Database.DeleteProductAsync(Product_A);
        }
        //Function when the back button is clicked
        public async void Back_F()
        {
            await Shell.Current.GoToAsync("..");
        }


    }
}

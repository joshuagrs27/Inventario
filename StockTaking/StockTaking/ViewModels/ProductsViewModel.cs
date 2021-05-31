using StockTaking.Models;
using StockTaking.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class ProductsViewModel:BindableObject
    {
        //Constructor
        public ProductsViewModel()
        {
            ADD_NEW_PRODUCT_COMMAND = new Command(GotoNewProductPage_F);
            ItemTapped = new Command<Product>(OnProductSelected_F);
        }



        //
        public async void OnAppearing()
        {
            //Get Products for Current Company
            List<Product> tempProducts = new List<Product>();
            var collection = await App.Database.GetProductsAsync();
            foreach(var product in collection)
            {
                if(product.Product_CompanyID == App.CurrentCompany.Company_Id)
                {
                    tempProducts.Add(product);
                }
            }
            ProductCollection = tempProducts;
        }
        //-----------------COMMANDS------------------\\
        public Command ADD_NEW_PRODUCT_COMMAND { get; }
        public Command<Product> ItemTapped { get; }
 
        //-----------------Getters and Setters------------------\\
        private List<Product> productCollection = new List<Product>();
        public List<Product> ProductCollection
        {
            get => productCollection;
            set
            {
                productCollection = value;
                OnPropertyChanged();
            }
        }
        //-----------------CUSTOM FUNCTIONS------------------\\

        //Function for Going to Add Product Page
        public async void GotoNewProductPage_F()
        {
            if (App.CurrentUser.User_Permission == "TRANSACTION-RIGHTS")
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You Dont Have Permission", "back");
                return;
            }
            //Go to new Company Page
            await Shell.Current.GoToAsync(nameof(NewProductPage));
        }

        //Function when Product is Selected
        public async void OnProductSelected_F(Product product)
        {
            if (App.CurrentUser.User_Permission == "TRANSACTION-RIGHTS")
            {
                await App.Current.MainPage.DisplayAlert("Alert", "You Dont Have Permission", "back");
                return;
            }
            //Set the Global Current Product
            App.CurrentProduct = product;
            //Go to Product Details Page
            await Shell.Current.GoToAsync(nameof(ProductDetailsPage));
        }
        
    }

}


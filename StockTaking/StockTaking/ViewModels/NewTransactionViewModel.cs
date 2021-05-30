using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class NewTransactionViewModel:BindableObject
    {
        //Consructor
        public NewTransactionViewModel()
        {
            CANCEL_COMMAND = new Command(Cancel_F);
        }
        //
        public async void OnAppearing()
        {

            //Get Products for Current Company
            List<Product> tempProducts = new List<Product>();
            var collection = await App.Database.GetProductsAsync();
            foreach (var product in collection)
            {
                if (product.Product_CompanyID == App.CurrentCompany.Company_Id)
                {
                    tempProducts.Add(product);
                }
            }
            ProductList = new ObservableCollection<Product>();
            ProductList = tempProducts;

            ////Get Transaction Type for Current Company
        }
        //------COMMANDS-----\\
        public Command SAVE_TRANSACTION_COMMAND { get; }
        public Command CANCEL_COMMAND { get; }

        //----getters and setters---\\
        //
        private DateTime newDate;
        public DateTime NewDate
        {
            get => newDate;
            set
            {
                if (value == newDate) { return; }
                newDate = value;
                OnPropertyChanged();
            }
        }
        //
        private IList<Product> productList;
        public IList<Product> ProductList
        {
            get => productList;
            set
            {
                productList = value;
                OnPropertyChanged();
            }
        }
        //
        private Product productSelected;
        public Product ProductSelected
        {
            get => productSelected;
            set
            {
                productSelected = value;
                OnPropertyChanged();
            }
        }
        
        //Custom Function
        public async void Cancel_F()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
    }
}

using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class EditTransactionViewModel:BindableObject
    {
        //Constructor
        public EditTransactionViewModel()
        {
            CANCEL_COMMAND = new Command(Cancel_F);
            //
            SAVE_TRANSACTION_COMMAND = new Command(SaveTransaction_F);
        }

        public async void OnAppearing()
        {
            TheTransaction = App.CurrentTransaction;
            //
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
            //
            IList<Models.Picker> tempValues = new List<Models.Picker>();
            tempValues.Add(new Models.Picker { Value_Data = "IN" });
            tempValues.Add(new Models.Picker { Value_Data = "OUT" });
            TransactionsTypes = new ObservableCollection<Models.Picker>();
            TransactionsTypes = tempValues;
        }

        //---   COMMANDS ---\\
        public Command SAVE_TRANSACTION_COMMAND { get; }
        public Command CANCEL_COMMAND { get; }
        //--Getters and Setters--\\
        public Transaction theTransaction;
        public Transaction TheTransaction
        {
            get => theTransaction;
            set
            {
                theTransaction = value;
                OnPropertyChanged();
            }
        }
        //
        private int amount;
        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged();
            }
        }
        private string notes;
        public string Notes
        {
            get => notes;
            set
            {
                notes = value;
                OnPropertyChanged();
            }
        }
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
        private IList<Product> productList = new List<Product>();
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
        private Product productSelected = new Product();
        public Product ProductSelected
        {
            get => productSelected;
            set
            {
                productSelected = value;
                OnPropertyChanged();
            }
        }
        //
        public IList<Models.Picker> transactionTypes = new List<Models.Picker>();
        public IList<Models.Picker> TransactionsTypes
        {
            get => transactionTypes;
            set
            {
                transactionTypes = value;
                OnPropertyChanged();
            }
        }
        //
        private Models.Picker chosenTransaction = new Models.Picker();
        public Models.Picker ChosenTransaction
        {
            get => chosenTransaction;
            set
            {
                chosenTransaction = value;
                OnPropertyChanged();
            }
        }

        ////////////////
        public async void SaveTransaction_F()
        {

            bool ans = await Validate_F();
            //
            if (ans)
            {
                Transaction transObj = new Transaction();
                //
                transObj = TheTransaction;
                transObj.Transaction_Amount = Convert.ToInt32(Amount);
                transObj.Transaction_Date = NewDate;
                transObj.Transaction_Notes = Notes;
                transObj.Transaction_Type = ChosenTransaction.Value_Data;
                transObj.Transaction_Product_Name = ProductSelected.Product_Name;
                transObj.Transaction_Product_ID = ProductSelected.Product_Id;
                //
                await App.Database.EditTransactionAsync(transObj);
                //
                UpdateProduct_F(ProductSelected);
                //
                await App.Current.MainPage.DisplayAlert("Success", "Transaction Saved", "ok");
                //
                Cancel_F();
            }

        }
        public async Task<bool> Validate_F()
        {
            bool ans2 = true;
            //
            if (ChosenTransaction == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Choose Transaction Type", "back");
                return ans2;
            }
            if (NewDate == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Choose Date", "back");
                return ans2;
            }
            if (ProductSelected == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Choose Product", "back");
                return ans2;
            }
            if (Amount <= 0)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alert", "Enter amount", "back");
                return ans2;
            }
            //await App.Current.MainPage.DisplayAlert("Success", "Product Successfully Saved", "Ok");
            //
            return ans2;
        }
        public async void Cancel_F()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }
        public async void UpdateProduct_F(Product product)
        {
            Product affectedProduct = new Product();
            affectedProduct = await App.Database.SearchProductAsync(product.Product_Name);
            affectedProduct.Product_Current_Stock = affectedProduct.Product_Current_Stock + Convert.ToInt32(Amount);
            if (affectedProduct.Product_Current_Stock <= 50)
            {
                affectedProduct.Product_Level = "LOW";
            }
            if(affectedProduct.Product_Current_Stock > 50)
            {
                affectedProduct.Product_Level = "HIGH";
            }
            //
            await App.Database.EditProductAsync(affectedProduct);
            //
        }

    }
}

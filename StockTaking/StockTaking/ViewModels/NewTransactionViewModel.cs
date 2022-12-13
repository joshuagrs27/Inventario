using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class NewTransactionViewModel:BindableObject
    {
        //Consructor
        public NewTransactionViewModel()
        {
            CANCEL_COMMAND = new Command(Cancel_F);
            //
            SAVE_TRANSACTION_COMMAND = new Command(SaveTransaction_F);
            
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
            ////Get Transaction Type 
            MakePickerData_F();

        }
        //------COMMANDS-----\\
        public Command SAVE_TRANSACTION_COMMAND { get; }
        public Command CANCEL_COMMAND { get; }

        //----getters and setters---\\
        //
        private string amount;
        public string Amount
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
        //
        public IList<Models.Picker> transactionTypes;
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
        private Models.Picker chosenTransaction;
        public Models.Picker ChosenTransaction
        {
            get => chosenTransaction;
            set
            {
                chosenTransaction = value;
                OnPropertyChanged();
            }
        }

        //Custom Function
        public async void Cancel_F()
        {
            // This will pop the current page off the navigation stack
            await Shell.Current.GoToAsync("..");
        }

        public void MakePickerData_F()
        {
            IList<Models.Picker> tempValues = new List<Models.Picker>();
            tempValues.Add(new Models.Picker { Value_Data = "Entrada" });
            tempValues.Add(new Models.Picker { Value_Data = "Salida" });
            TransactionsTypes = new ObservableCollection<Models.Picker>();
            TransactionsTypes = tempValues;
        }

        public async void SaveTransaction_F()
        {
            bool ans = await Validate_F();
            //
            if (ans)
            {
                Transaction transObj = new Transaction();
                //
                transObj.Transaction_Amount = Convert.ToInt32(Amount);
                transObj.Transaction_Date = NewDate;
                transObj.Transaction_Company_ID = App.CurrentCompany.Company_Id;
                transObj.Transaction_Notes = Notes;
                transObj.Transaction_Type = ChosenTransaction.Value_Data;
                transObj.Transaction_Product_Name = ProductSelected.Product_Name;
                transObj.Transaction_Product_ID = ProductSelected.Product_Id;
                //
                if(transObj.Transaction_Type == "OUT")
                {
                    if (ProductSelected.Product_Current_Stock > transObj.Transaction_Amount)
                    {

                        await App.Database.SaveTransactionAsync(transObj);

                        UpdateProduct_F(ProductSelected, transObj);

                        await App.Current.MainPage.DisplayAlert("Exito", "Transaccion guardada", "ok");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "La transaccion fallo", "ok");
                    }
                }
                //
                if(transObj.Transaction_Type == "IN")
                {
            
                    await App.Database.SaveTransactionAsync(transObj);

                    UpdateProduct_F(ProductSelected, transObj);

                    await App.Current.MainPage.DisplayAlert("Exito", "Transaccion guardada", "ok");
                    
                }
                //
                Cancel_F();
            }
            
        }
        //
        public void CheckTransactionType()
        {

        }
        //
        public async void UpdateProduct_F(Product product, Transaction transaction)
        {
            Product affectedProduct = new Product();
            affectedProduct = await App.Database.SearchProductAsync(product.Product_Name);
            //Check Transaction Type
            if (transaction.Transaction_Type == "IN")
            {
                affectedProduct.Product_Current_Stock = affectedProduct.Product_Current_Stock + Convert.ToInt32(Amount);
            }
            if (transaction.Transaction_Type == "OUT")
            {
                affectedProduct.Product_Current_Stock = affectedProduct.Product_Current_Stock - Convert.ToInt32(Amount);
            }
            //
            if (affectedProduct.Product_Current_Stock <= 50)
            {
                affectedProduct.Product_Level = "LOW";
            }
            if (affectedProduct.Product_Current_Stock > 50)
            {
                affectedProduct.Product_Level = "HIGH";
            }
       
            await App.Database.EditProductAsync(affectedProduct);
            //
        }
        //
        public async Task<bool> Validate_F()
        {
            bool ans2 = true;
            //
            if(ChosenTransaction == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Elige tipo transaccion", "Atras");
                return ans2;
            }
            if(NewDate == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Elige fecha", "Atras");
                return ans2;
            }
            if (ProductSelected == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Elige producto", "Atras");
                return ans2;
            }
            if (Amount == null)
            {
                ans2 = false;
                await App.Current.MainPage.DisplayAlert("Alerta", "Ingresa cantidad", "Atras");
                return ans2;
            }
            //await App.Current.MainPage.DisplayAlert("Success", "Product Successfully Saved", "Ok");
            //
            return ans2;
        }
    }
}

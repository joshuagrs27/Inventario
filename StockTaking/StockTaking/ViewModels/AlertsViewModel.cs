using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class AlertsViewModel:BindableObject
    {
        //Constructor
        public AlertsViewModel()
        {

        }
        public async void OnAppearing()
        {
            var tempCollection = await App.Database.GetProductsAsync();
            var otherCollection = new List<Product>();
            foreach(var product in tempCollection)
            {
                if(product.Product_Current_Stock <= product.Product_Low_Level)
                {
                    otherCollection.Add(product);
                }
            }
            AlertsList = otherCollection;
        }
        /////////////////////////
        private List<Product> alertsList;
        public List<Product> AlertsList
        {
            get => alertsList;
            set
            {
                alertsList = value;
                OnPropertyChanged();
            }
        }
        ///////////////////////////
    }
}

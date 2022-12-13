﻿using StockTaking.Models;
using StockTaking.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public class ActivityViewModel:BindableObject
    {
        //Constructor
        public ActivityViewModel()
        {
            //
            ItemTapped = new Command<Transaction>(OnTransactionSelected_F);
            ADD_NEW_TRANSACTION = new Command(NewTransaction_F);
        }

        //Function called everytime the Screen Appears
        public async void OnAppearing()
        {
            List<Transaction> tempCollection = new List<Transaction>();
            var allTransaction = await App.Database.GetTransactionsAsync();
            foreach(var transaction in allTransaction)
            {
                if(transaction.Transaction_Company_ID == App.CurrentCompany.Company_Id)
                {
                    
                    tempCollection.Add(transaction);
                }
            }

            TransactionCollection = tempCollection;
        }
        //----COMMANDS---\\
        public Command ADD_NEW_TRANSACTION { get; }
        public Command<Transaction> ItemTapped { get; }
        //----GETTERS & SETTERS----\\
        private List<Transaction> transactionCollection = new List<Transaction>();
        public List<Transaction> TransactionCollection
        {
            get => transactionCollection;
            set
            {
                transactionCollection = value;
                OnPropertyChanged();
            }
        }
        //----CUSTOM FUNCTIONS---\\
        public async void OnTransactionSelected_F(Transaction transaction)
        {
            if (App.CurrentUser.User_Permission == "PRODUCTS-RIGHTS")
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Tu no tienes permiso", "Atras");
                return;
            }
            //
            App.CurrentTransaction = transaction;
            //Go to nEdit Transaction Page
            await Shell.Current.GoToAsync(nameof(EditTransactionPage));
        }
        //
        public async void NewTransaction_F()
        {
            if(App.CurrentUser.User_Permission == "PRODUCTS-RIGHTS")
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Tu no tienes permiso", "Atras");
                return;
            }

            //Go to new Company Page
            await Shell.Current.GoToAsync(nameof(NewTransactionPage));
        }
    }
}

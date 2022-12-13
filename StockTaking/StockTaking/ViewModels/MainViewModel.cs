using Microcharts;
using SkiaSharp;
using StockTaking.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace StockTaking.ViewModels
{
    public  class MainViewModel:BindableObject
    {
        //Constructor
        public MainViewModel()
        {
            //
            StartAlerts_F();
        }
        //
        public async void OnAppearing()
        {
           
            List<Product> tempCollection = await App.Database.GetProductsAsync();
            List<Product> otherCollection = new List<Product>();
            foreach(var product in tempCollection)
            {
                if(product.Product_CompanyID == App.CurrentCompany.Company_Id)
                {
                    otherCollection.Add(product);
                }
            }
            Products = otherCollection;
            MakeChartData_F();

        }
        //---Getterrs and Setters ---\\
        private List<Product> products = new List<Product>();
        public List<Product> Products
        {
            get => products;
            set
            {
                products = value;
                OnPropertyChanged();
            }
        }

        private List<ChartEntry> entries1;
        public List<ChartEntry> Entries1
        {
            get => entries1;
            set
            {
                entries1 = value;
                OnPropertyChanged();
            }
        }

        //---Getters & Setters---\\
        private PieChart chartObj; 
        public PieChart ChartObj
        {
            get => chartObj;
            set
            {
                chartObj = value;
                OnPropertyChanged();
            }
        }

        public void MakeChart_F()
        {
            TimeSpan newTime = new TimeSpan(0, 0, 8);
            ChartObj = new PieChart {
                Entries = Entries1,
                //ValueLabelOrientation = Orientation.Horizontal,
                LabelTextSize = 30,
                IsAnimated = true,
                AnimationProgress = 3f,
                AnimationDuration = newTime
            };
        }
        //
        public void MakeChartData_F()
        {

            Entries1 = null;
            ChartObj = null;
            var source = new List<ChartEntry>();
            int i = 10;

            //
            if(Products.Count <= 0)
            {
                source.Add(new ChartEntry(100)
                {
                    Label = "Vacio",
                    ValueLabelColor = SKColor.Parse("#b3b3b3"),
                    ValueLabel = "0",
                    Color = SKColor.Parse("#b3b3b3")
                });
                Entries1 = source;
                MakeChart_F();
                return;
            }
            foreach (var product in Products)
            {
                //

                var random = new Random();
                var color = String.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
                source.Add(new ChartEntry(product.Product_Current_Stock)
                {
                    Label = product.Product_Name,
                    ValueLabelColor =SKColor.Parse(color),
                    ValueLabel = product.Product_Current_Stock.ToString(),
                    Color = SKColor.Parse(color)
                });
                i = i + 10;
            }

            Entries1 = source;
            MakeChart_F();

        }

        public async void StartAlerts_F()
        {
            List<Product> tempCollection = await App.Database.GetProductsAsync();
            var tempStrings = new List<string>();
            foreach(var product in tempCollection)
            {
                if(product.Product_Current_Stock <= product.Product_Low_Level)
                {
                    tempStrings.Add(product.Product_Name);
                }
            }
            string temp = String.Join(String.Empty, tempStrings.ToArray());
            if (tempStrings.Count > 0)
            {
                await App.Current.MainPage.DisplayAlert("Peligro", 
                    "El siguiente producto es poco: "+ " " + temp +" ," +" :ve a la pagina de alerta para mas", 
                    "ok");
            }
        }
    }

}


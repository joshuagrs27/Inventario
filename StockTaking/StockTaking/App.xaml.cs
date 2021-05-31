using StockTaking.Models;
using StockTaking.Services;
using StockTaking.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockTaking
{
    public partial class App : Application
    {
        //
        static Database database;
        //
        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "stock.db3"));
                }
                return database;
            }
        }

        //Global Variable for CurrentCompany
        public static Company currentCompany = new Company();
        public static Company CurrentCompany
        {
            get => currentCompany;
            set
            {
                currentCompany = value;
            }
        }
        //Global Variable for CurrentUser
        public static User currentUser = new User();
        public static User CurrentUser
        {
            get => currentUser;
            set
            {
                currentUser = value;
            }
        }
        //
        public static Product currentProduct = new Product();
        public static Product CurrentProduct
        {
            get => currentProduct;
            set
            {
                currentProduct = value;
            }
        }
        //
        public static Transaction currentTransaction = new Transaction();
        public static Transaction CurrentTransaction
        {
            get => currentTransaction;
            set
            {
                currentTransaction = value;
            }
        }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

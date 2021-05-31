using StockTaking.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StockTaking.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditTransactionPage : ContentPage
    {
        public EditTransactionPage()
        {
            InitializeComponent();
        }
        //
        //
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var vm = BindingContext as EditTransactionViewModel;
            //
            vm.OnAppearing();

        }
    }
}
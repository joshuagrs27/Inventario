using StockTaking.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace StockTaking.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
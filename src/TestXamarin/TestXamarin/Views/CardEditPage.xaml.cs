using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXamarin.Models;
using TestXamarin.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardEditPage : ContentPage
    {
        private CardEditViewModel _viewModel;

        public CardEditPage(ListItemModel item = null)
        {
            InitializeComponent();

            Title = item != null ? item.Name : "New Card";
            BindingContext = _viewModel = new CardEditViewModel(item ?? new ListItemModel(), Navigation);
        }

        protected override async void OnAppearing()
        {
            await _viewModel.Load();
             
            base.OnAppearing();            
        }

        private async void Button_Attach_Clicked(object sender, EventArgs e)
        {
            try
            {
                var result = await FilePicker.PickAsync();
                _viewModel.File = result;                               
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }
        }
    }
}
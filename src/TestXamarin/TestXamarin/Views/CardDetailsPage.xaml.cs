using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXamarin.Models;
using TestXamarin.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarin.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CardDetailsPage : ContentPage
    {
        private CardDetailsViewModel _viewModel;

        public CardDetailsPage(ListItemModel card)
        {
            InitializeComponent();

            Title = card.Name;
            BindingContext = _viewModel = new CardDetailsViewModel(card ?? new ListItemModel());
        }

        private async void ToolbarItem_Edit_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CardEditPage(_viewModel.Item));
        }

        private void ToolbarItem_Delete_Clicked(object sender, EventArgs e)
        {

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _viewModel.Load();
        }
    }
}
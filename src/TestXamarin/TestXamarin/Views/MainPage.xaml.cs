using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXamarin.Models;
using TestXamarin.ViewModels;
using TestXamarin.Views;
using Xamarin.Forms;

namespace TestXamarin
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new MainPageViewModel();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new ListDetailsPage(e.CurrentSelection.First() as ListModel));
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new InviteMemberPage());
        }
    }
}

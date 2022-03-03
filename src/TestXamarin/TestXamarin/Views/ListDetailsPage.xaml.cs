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
    public partial class ListDetailsPage : ContentPage
    {
        private ListDetailsViewModel _viewModel;

        public ListDetailsPage(ListModel list)
        {
            InitializeComponent();

            Title = list.Name;
            BindingContext = _viewModel = new ListDetailsViewModel(list);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CardEditPage(new ListItemModel
            {
                 IdList = _viewModel.List.Id,                  
            }));
        }

        protected override void OnAppearing()
        {           
            _viewModel.OnAppearing();

            base.OnAppearing();
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await Navigation.PushAsync(new CardDetailsPage(e.CurrentSelection.First() as ListItemModel));
        }
    }
}
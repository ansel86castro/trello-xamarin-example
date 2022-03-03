using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TestXamarin.Models;
using TestXamarin.Services;
using Xamarin.Forms;

namespace TestXamarin.ViewModels
{
    public class MainPageViewModel: BaseViewModel
    {
        public Command LoadItemsCommand { get; }

        private TrelloBoardService _service;

        public Command<ListModel> ItemTapped { get; }
        public ListModel SelectedItem { get; private set; }
        public ObservableCollection<ListModel> Items { get; }

        public MainPageViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            _service =  DependencyService.Get<TrelloBoardService>();    
            Items = new ObservableCollection<ListModel>();
        }


        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;
        }

        protected async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                var result = await _service.GetLists();
                Items.Clear();
                foreach (var item in result)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TestXamarin.ViewModels
{
    public abstract class BaseListViewModel<T> : BaseViewModel
        where T : class
    {
        public Command LoadItemsCommand { get; }
        public Command<T> ItemTapped { get; }
        public T SelectedItem { get; private set; }
        public ObservableCollection<T> Items { get; }

        public BaseListViewModel()
        {
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            Items = new ObservableCollection<T>();
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
                await OnLoad();
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

        protected abstract Task OnLoad();
    }
}


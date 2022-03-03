using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TestXamarin.Models;
using TestXamarin.Services;
using Xamarin.Forms;

namespace TestXamarin.ViewModels
{
    public class ListDetailsViewModel : BaseListViewModel<ListItemModel>
    {
        public ListModel List { get; }

        private TrelloCardService _service;

        public ListDetailsViewModel(ListModel list)
        {
            List = list;
            _service = DependencyService.Get<TrelloCardService>();
        }

        protected override async Task OnLoad()
        {
            Items.Clear();
            
            foreach (var item in await  _service.GetCards(List.Id))
            {
                Items.Add(item);
            }
        }
    }
}

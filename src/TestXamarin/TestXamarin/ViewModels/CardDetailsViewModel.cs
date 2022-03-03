using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXamarin.Models;
using TestXamarin.Services;
using Xamarin.Forms;

namespace TestXamarin.ViewModels
{
    public class CardDetailsViewModel :BaseViewModel
    {

        private TrelloBoardService _listService;
        private TrelloCardService _cardService;

        public CardDetailsViewModel(ListItemModel card)
        {
            this.Item = card;
            _listService = DependencyService.Get<TrelloBoardService>();
            _cardService = DependencyService.Get<TrelloCardService>();
        }

        public ListModel List { get; set; }
        public ListItemModel Item { get; set; }
        public List<ListItemAttachementModel> Attachements { get; private set; }

        public async Task Load()
        {
          
            var lists = await _listService.GetLists();            

            List = lists.FirstOrDefault(x => x.Id == Item.IdList);
            OnPropertyChanged(nameof(List));

            if (Item.Id != null)
            {
                Attachements = await _cardService.GetCardAttachements(Item.Id);
            }
            OnPropertyChanged(nameof(Attachements));
        }

    }
}

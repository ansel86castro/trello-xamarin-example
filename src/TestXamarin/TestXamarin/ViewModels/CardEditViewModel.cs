using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestXamarin.Models;
using TestXamarin.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TestXamarin.ViewModels
{
    public class CardEditViewModel : BaseViewModel
    {
        private readonly INavigation _navigation;        

        public CardEditViewModel(ListItemModel item, INavigation navigation)
        {           
            AddCard = new Command(OnAddCard, CanAddCard);
            _navigation = navigation;            
            _item = item;
            _service = DependencyService.Get<TrelloCardService>();
        }

        public string Name
        {
            get => _item.Name;
            set
            {
               _item.Name = value;
                OnPropertyChanged();

                AddCard.ChangeCanExecute();
            }
        }

        private ListItemModel _item;

        private TrelloCardService _service;
        private FileResult _file;


        public bool IsLoaded => !IsBusy;

        public Command AddCard { get; }

        public List<ListModel> Lists { get; set; }

        public List<ListItemAttachementModel> Attachements { get; set; } = new List<ListItemAttachementModel>();

        public ListModel List { get; set; }

        public string Filename { get => File.FileName; }

        public FileResult File { get => _file; set
            {
                SetProperty(ref _file, value);
                OnPropertyChanged(nameof(Filename));
            } 
        }

        public async Task Load()
        {            
            var listService = DependencyService.Get<TrelloBoardService>();
            Lists = await listService.GetLists();          
            OnPropertyChanged(nameof(Lists));

            List = Lists.FirstOrDefault(x => x.Id == _item.IdList);            
            OnPropertyChanged(nameof(List));

            if (_item.Id != null)
            {
                Attachements = await _service.GetCardAttachements(_item.Id);
            }
            OnPropertyChanged(nameof(Attachements));
        }

        private bool CanAddCard(object arg)
        {
            bool isValid = !string.IsNullOrEmpty(Name);
            return isValid;
        }

        private async void OnAddCard(object obj)
        {
            _item.IdList = List.Id;
            if (string.IsNullOrEmpty(_item.Id))
            {
                _item = await _service.CreateCard(_item);              
            }
            else
            {
                _item = await _service.UpdateCard(_item);
            }

            if (File != null && Attachements.All(x => x.Name != File.FileName))
            {
                using (var stream = await File.OpenReadAsync())
                {                    
                    await _service.CreateCardAttachement(File.FileName, File.ContentType, _item.Id, stream, File.FileName);
                }
            }

            await _navigation.PopAsync();
        }
    }
}

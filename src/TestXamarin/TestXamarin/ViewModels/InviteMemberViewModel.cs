using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using TestXamarin.Services;
using Xamarin.Forms;

namespace TestXamarin.ViewModels
{
    public class InviteMemberViewModel :BaseViewModel
    {
        private readonly INavigation _navigation;
        private readonly TrelloBoardService _service;
        private string _email;

        public InviteMemberViewModel(INavigation navigation)
        {
            Invite = new Command(OnInvite, CanInvite);
            _navigation = navigation;
            _service = DependencyService.Get<TrelloBoardService>();

        }

        public string Email
        {
            get => _email;
            set
            {
                SetProperty(ref _email, value);
                Invite.ChangeCanExecute();
            }
        }

        public bool IsLoaded => !IsBusy;

        public Command Invite { get; }    

        private bool CanInvite(object arg)
        {
            bool isValid = Email != null && Regex.IsMatch(Email, @"(\w+)((\.|-)\w+)*@(\w+)(\.\w+)*");
            return isValid;
        }

        private async void OnInvite(object obj)
        {
            await _service.InviteMember(TrelloApi.BoardId, Email);

            await _navigation.PopModalAsync();
        }
    }
}

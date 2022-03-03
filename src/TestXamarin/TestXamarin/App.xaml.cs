using System;
using TestXamarin.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            DependencyService.Register<TrelloBoardService>();
            DependencyService.Register<TrelloCardService>();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

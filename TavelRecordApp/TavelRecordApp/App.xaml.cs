using Microsoft.WindowsAzure.MobileServices;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;

namespace TavelRecordApp {
    public partial class App : Application {
        public static string DatabaseLocation = string.Empty;

        public static MobileServiceClient client = new MobileServiceClient("https://travelrecordappxamtlj.azurewebsites.net", new HttpClientHandler());

        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        public App(string databaseLocation) {
            InitializeComponent();
            MainPage = new NavigationPage(new MainPage());
            DatabaseLocation = databaseLocation;
        }
        protected override void OnStart() {
        }

        protected override void OnSleep() {
        }

        protected override void OnResume() {
        }
    }
}

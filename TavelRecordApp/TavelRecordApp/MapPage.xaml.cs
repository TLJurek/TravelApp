using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using SQLite;
using TavelRecordApp.Model;

namespace TavelRecordApp {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage {

        private bool hasLocationPermission = false;
        public MapPage() {
            InitializeComponent();
            GetPermissions();
        }

        private async void GetPermissions() {
            try {


                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
                if (status != PermissionStatus.Granted) {

                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse)) {
                        await DisplayAlert("Need your location", "give access bro pls", "k");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                    if (results.ContainsKey(Permission.LocationWhenInUse)) {
                        status = results[Permission.LocationWhenInUse];
                    }
                }
                if (status == PermissionStatus.Granted) {
                    hasLocationPermission = true;
                    locationMap.IsShowingUser = true;
                    GetLocation();
                }
            } catch (Exception e) {
                await DisplayAlert("Bruh", e.Message, "ait fam");
            }
        }
        protected override async void OnAppearing() {
            base.OnAppearing();
            if (hasLocationPermission) {
                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                await locator.StartListeningAsync(TimeSpan.Zero, 100);
            }
            GetLocation();

            /*
            using (var conn = new SQLiteConnection(App.DatabaseLocation)) {
                conn.CreateTable<Post>();
                var posts = conn.Table<Post>().ToList();
                DisplayInMap(posts);
            }
            */
            var posts = await App.client.GetTable<Post>().Where(p => p.UserId == App.user.Id).ToListAsync();
            DisplayInMap(posts);
        }

            

        private void DisplayInMap(List<Post> posts) {
            try {
                foreach (var post in posts) {
                    var position = new Xamarin.Forms.Maps.Position(post.Latitude, post.Longitude);
                    var pin = new Xamarin.Forms.Maps.Pin() {
                        Type = Xamarin.Forms.Maps.PinType.SavedPin,
                        Position = position,
                        Label = (post.VenueName ?? "No Name"),
                        Address = post.Address
                    };

                    locationMap.Pins.Add(pin);
                }
            }
            catch(NullReferenceException nre) {
                Console.WriteLine(nre);
            }
        }

        protected override void OnDisappearing() {
            base.OnDisappearing();
            CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= Locator_PositionChanged;
        }

        void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e) {
            MoveMap(e.Position);
        }

        private async void GetLocation() {
            if (hasLocationPermission) {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                MoveMap(position);
            }
        }

        private void MoveMap(Position position) {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new Xamarin.Forms.Maps.MapSpan(center, 1, 1);
            locationMap.MoveToRegion(span);
        }
    }
}
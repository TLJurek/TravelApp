using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavelRecordApp.Logic;
using TavelRecordApp.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.WindowsAzure.MobileServices;

namespace TavelRecordApp {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage {
        public NewTravelPage() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {

            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e) {
            try {
                var selectedVenue = venueListView.SelectedItem as Venue;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                var post = new Post() {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name,
                    UserId = App.user.Id
                };


                /*
                using (var connection = new SQLiteConnection(App.DatabaseLocation)) {
                    connection.CreateTable<Post>();
                    var rows = connection.Insert(post);

                    if (rows > 0) {
                        DisplayAlert("Added", "Your gibberish has been added", "Epic dab");
                    }
                    else {
                        DisplayAlert("Mf failed", "You ting failed fam", "Sad dev noises");
                    }
                }
                */
                await App.client.GetTable<Post>().InsertAsync(post);
                await DisplayAlert("Success", "Experience successfully entered", "Ok");
            }
            catch(NullReferenceException nre) {
                Console.WriteLine(nre);
            }
            catch(Exception ex) {
                Console.WriteLine(ex);
            }
        }
    }
}
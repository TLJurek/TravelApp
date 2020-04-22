using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TavelRecordApp.Model;
using Xamarin.Forms;

namespace TavelRecordApp {
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage {
        public MainPage() {
            InitializeComponent();

            var assembly = typeof(MainPage);

            //planeIconImage.Source = ImageSource.FromResource("TavelRecordApp.Assets.Images.plane.png", assembly);
        }

        private async void LoginButton_Clicked(object sender, EventArgs e) {

            bool isEmailEmpty = string.IsNullOrEmpty(emailEntry.Text);
            bool isPasswordEmpty = string.IsNullOrEmpty(passwordEntry.Text);

            if (isEmailEmpty || isPasswordEmpty) {
                await DisplayAlert("Error", "Missing fields", "Ok");
            }
            else {

                var user = (await App.client.GetTable<Users>().Where(u => u.Email == emailEntry.Text).ToListAsync()).FirstOrDefault();
                
                if (user != null) {

                    App.user = user;

                    if(user.Password == passwordEntry.Text) {
                        await Navigation.PushAsync(new HomePage());
                    }
                    else {
                        await DisplayAlert("Error", "Password incorrect", "Bruh");
                    }
                }
                else {
                    await DisplayAlert("Error", "Login Error", "Perfect");
                }
                
            }
        }

        private void RegisterUserButton_Clicked(object sender, EventArgs e) {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}
